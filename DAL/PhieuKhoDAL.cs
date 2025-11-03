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

        // ========== Lấy tất cả ==========
        public List<PhieuKho> GetAll()
        {
            var list = new List<PhieuKho>();
            string sql = @"
                SELECT pk.*, nv.HoTen 
                FROM PhieuKho pk
                LEFT JOIN NhanVien nv ON pk.MaNV = nv.MaNV
                ORDER BY pk.MaPhieuKho DESC";

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

        // ========== Lấy theo ID ==========
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

        // ========== Thêm ==========
        public bool Insert(PhieuKho pk)
        {
            string sql = @"INSERT INTO PhieuKho (Loai, NgayLap, MaNV, TenNhanVien, GhiChu)
                           VALUES (@Loai, @NgayLap, @MaNV, @TenNV, @GhiChu)";
            using (var conn = new SqlConnection(_connStr))
            {
                conn.Open();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Loai", pk.Loai);
                    cmd.Parameters.AddWithValue("@NgayLap", pk.NgayLap);
                    cmd.Parameters.AddWithValue("@MaNV", (object?)pk.MaNV ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@TenNV", (object?)pk.TenNhanVien ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@GhiChu", (object?)pk.GhiChu ?? DBNull.Value);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // ========== Cập nhật ==========
        public bool Update(PhieuKho pk)
        {
            string sql = @"UPDATE PhieuKho
                           SET Loai=@Loai, NgayLap=@NgayLap, MaNV=@MaNV, TenNhanVien=@TenNV, GhiChu=@GhiChu
                           WHERE MaPhieuKho=@ID";
            using (var conn = new SqlConnection(_connStr))
            {
                conn.Open();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", pk.MaPhieuKho);
                    cmd.Parameters.AddWithValue("@Loai", pk.Loai);
                    cmd.Parameters.AddWithValue("@NgayLap", pk.NgayLap);
                    cmd.Parameters.AddWithValue("@MaNV", (object?)pk.MaNV ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@TenNV", (object?)pk.TenNhanVien ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@GhiChu", (object?)pk.GhiChu ?? DBNull.Value);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // ========== Xóa ==========
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
