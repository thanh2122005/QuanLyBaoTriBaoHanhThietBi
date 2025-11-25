using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.DAL
{
    public class PhieuKhoDAL
    {
        private readonly string _connStr;

        public PhieuKhoDAL(string connStr)
        {
            _connStr = connStr;
        }

        // =========================================================
        // 🟩 LẤY TẤT CẢ PHIẾU KHO
        // =========================================================
        public List<PhieuKho> GetAll()
        {
            var list = new List<PhieuKho>();
            string sql = @"
                SELECT pk.*, nv.HoTen 
                FROM PhieuKho pk
                LEFT JOIN NhanVien nv ON pk.MaNV = nv.MaNV
                ORDER BY pk.MaPhieuKho ASC";

            using (var conn = new SqlConnection(_connStr))
            {
                conn.Open();
                using (var cmd = new SqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new PhieuKho
                        {
                            MaPhieuKho = (int)reader["MaPhieuKho"],
                            Loai = reader["Loai"].ToString(),
                            NgayLap = (DateTime)reader["NgayLap"],
                            MaNV = reader["MaNV"] == DBNull.Value ? null : (int?)reader["MaNV"],
                            TenNhanVien = reader["TenNhanVien"]?.ToString(),
                            HoTen = reader["HoTen"]?.ToString(),
                            GhiChu = reader["GhiChu"]?.ToString()
                        });
                    }
                }
            }
            return list;
        }

        // =========================================================
        // 🟩 LẤY THEO ID
        // =========================================================
        public PhieuKho? GetById(int id)
        {
            PhieuKho? pk = null;
            string sql = @"
                SELECT pk.*, nv.HoTen 
                FROM PhieuKho pk
                LEFT JOIN NhanVien nv ON pk.MaNV = nv.MaNV
                WHERE pk.MaPhieuKho = @id";

            using (var conn = new SqlConnection(_connStr))
            {
                conn.Open();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            pk = new PhieuKho
                            {
                                MaPhieuKho = (int)reader["MaPhieuKho"],
                                Loai = reader["Loai"].ToString(),
                                NgayLap = (DateTime)reader["NgayLap"],
                                MaNV = reader["MaNV"] == DBNull.Value ? null : (int?)reader["MaNV"],
                                TenNhanVien = reader["TenNhanVien"]?.ToString(),
                                HoTen = reader["HoTen"]?.ToString(),
                                GhiChu = reader["GhiChu"]?.ToString()
                            };
                        }
                    }
                }
            }
            return pk;
        }

        // =========================================================
        // 🟩 THÊM MỚI
        // =========================================================
        public bool Insert(PhieuKho pk)
        {
            string sql = @"
                INSERT INTO PhieuKho (Loai, NgayLap, MaNV, TenNhanVien, GhiChu)
                VALUES (@Loai, @NgayLap, @MaNV, @TenNhanVien, @GhiChu)";
            try
            {
                using (var conn = new SqlConnection(_connStr))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        // === CHUẨN HÓA LOẠI PHIẾU ===
                        string loai = string.IsNullOrWhiteSpace(pk.Loai) ? "Nhap" : pk.Loai.Trim();

                        // Tự động chuyển về dạng hợp lệ (theo CHECK constraint)
                        if (loai.Equals("Nhập", StringComparison.OrdinalIgnoreCase)) loai = "Nhap";
                        else if (loai.Equals("Xuất", StringComparison.OrdinalIgnoreCase)) loai = "Xuat";
                        else if (loai != "Nhap" && loai != "Xuat") loai = "Nhap";

                        cmd.Parameters.AddWithValue("@Loai", loai);

                        // === XỬ LÝ NGÀY ===
                        DateTime ngayLapValue;
                        if (pk.NgayLap == default || pk.NgayLap.Year < 1900)
                            ngayLapValue = DateTime.Now;
                        else
                            ngayLapValue = pk.NgayLap;

                        cmd.Parameters.AddWithValue("@NgayLap", ngayLapValue);

                        // === CÁC TRƯỜNG KHÁC ===
                        cmd.Parameters.AddWithValue("@MaNV", (object?)pk.MaNV ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@TenNhanVien", (object?)pk.TenNhanVien ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@GhiChu", (object?)pk.GhiChu ?? DBNull.Value);

                        // === THỰC THI ===
                        int rows = cmd.ExecuteNonQuery();
                        Console.WriteLine($"✅ Đã thêm {rows} dòng mới vào bảng PhieuKho.");
                        return rows > 0;
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine("❌ Lỗi SQL khi thêm phiếu kho: " + sqlEx.Message);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Lỗi khác khi thêm phiếu kho: " + ex.Message);
                throw;
            }
        }

        // =========================================================
        // 🟩 CẬP NHẬT
        // =========================================================
        public bool Update(PhieuKho pk)
        {
            string sql = @"
                UPDATE PhieuKho
                SET Loai=@Loai, NgayLap=@NgayLap, MaNV=@MaNV, TenNhanVien=@TenNhanVien, GhiChu=@GhiChu
                WHERE MaPhieuKho=@ID";

            using (var conn = new SqlConnection(_connStr))
            {
                conn.Open();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", pk.MaPhieuKho);

                    // Chuẩn hóa loại phiếu
                    string loai = string.IsNullOrWhiteSpace(pk.Loai) ? "Nhap" : pk.Loai.Trim();
                    if (loai.Equals("Nhập", StringComparison.OrdinalIgnoreCase)) loai = "Nhap";
                    else if (loai.Equals("Xuất", StringComparison.OrdinalIgnoreCase)) loai = "Xuat";
                    else if (loai != "Nhap" && loai != "Xuat") loai = "Nhap";
                    cmd.Parameters.AddWithValue("@Loai", loai);

                    cmd.Parameters.AddWithValue("@NgayLap", pk.NgayLap);
                    cmd.Parameters.AddWithValue("@MaNV", (object?)pk.MaNV ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@TenNhanVien", (object?)pk.TenNhanVien ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@GhiChu", (object?)pk.GhiChu ?? DBNull.Value);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // =========================================================
        // 🟩 XÓA
        // =========================================================
        public bool Delete(int id)
        {
            string sql = "DELETE FROM PhieuKho WHERE MaPhieuKho=@id";
            using (var conn = new SqlConnection(_connStr))
            {
                conn.Open();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}
