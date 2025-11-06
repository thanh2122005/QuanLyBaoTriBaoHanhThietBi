using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.DAL
{
    public class NhanVienDAL
    {
        private readonly string _conn;

        public NhanVienDAL(IConfiguration config)
        {
            _conn = config.GetConnectionString("DefaultConnection");
        }
        // ===== Hàm dùng chung để map dữ liệu từ DataReader =====
        private NhanVien MapReader(SqlDataReader dr)
        {
            return new NhanVien
            {
                MaNV = Convert.ToInt32(dr["MaNV"]),
                HoTen = dr["HoTen"].ToString(),
                SoDienThoai = dr["SoDienThoai"] == DBNull.Value ? null : dr["SoDienThoai"].ToString(),
                Email = dr["Email"] == DBNull.Value ? null : dr["Email"].ToString(),
                TrangThai = dr["TrangThai"].ToString()
            };
        }

        // ==================== GET ALL ====================
        public List<NhanVien> GetAll()
        {
            var list = new List<NhanVien>();
            try
            {
                using var conn = new SqlConnection(_conn);
                using var cmd = new SqlCommand("SELECT * FROM NhanVien ORDER BY MaNV DESC", conn);
                conn.Open();

                using var dr = cmd.ExecuteReader();
                while (dr.Read())
                    list.Add(MapReader(dr));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] GetAll NhanVien: {ex.Message}");
            }
            return list;
        }

        // ==================== GET BY ID ====================
        public NhanVien? GetById(int id)
        {
            try
            {
                using var conn = new SqlConnection(_conn);
                using var cmd = new SqlCommand("SELECT * FROM NhanVien WHERE MaNV=@id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();

                using var dr = cmd.ExecuteReader();
                return dr.Read() ? MapReader(dr) : null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] GetById NhanVien: {ex.Message}");
                return null;
            }
        }

        // ==================== ADD ====================
        public bool Add(NhanVien nv)
        {
            try
            {
                using var conn = new SqlConnection(_conn);
                using var cmd = new SqlCommand(@"
                    INSERT INTO NhanVien (HoTen, SoDienThoai, Email, TrangThai)
                    VALUES (@HoTen, @SoDienThoai, @Email, @TrangThai)", conn);

                cmd.Parameters.AddWithValue("@HoTen", nv.HoTen);
                cmd.Parameters.AddWithValue("@SoDienThoai", (object?)nv.SoDienThoai ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Email", (object?)nv.Email ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@TrangThai", nv.TrangThai);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Add NhanVien: {ex.Message}");
                return false;
            }
        }

        // ==================== UPDATE ====================
        public bool Update(NhanVien nv)
        {
            try
            {
                using var conn = new SqlConnection(_conn);
                using var cmd = new SqlCommand(@"
                    UPDATE NhanVien 
                    SET HoTen=@HoTen, SoDienThoai=@SoDienThoai, Email=@Email, TrangThai=@TrangThai
                    WHERE MaNV=@MaNV", conn);

                cmd.Parameters.AddWithValue("@MaNV", nv.MaNV);
                cmd.Parameters.AddWithValue("@HoTen", nv.HoTen);
                cmd.Parameters.AddWithValue("@SoDienThoai", (object?)nv.SoDienThoai ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Email", (object?)nv.Email ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@TrangThai", nv.TrangThai);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Update NhanVien: {ex.Message}");
                return false;
            }
        }

        // ==================== DELETE ====================
        public bool Delete(int id)
        {
            try
            {
                using var conn = new SqlConnection(_conn);
                using var cmd = new SqlCommand("DELETE FROM NhanVien WHERE MaNV=@id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();

                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Delete NhanVien: {ex.Message}");
                return false;
            }
        }

        // ==================== KPI ====================
        public int CountActive()
        {
            try
            {
                using var conn = new SqlConnection(_conn);
                using var cmd = new SqlCommand("SELECT COUNT(*) FROM NhanVien WHERE TrangThai = N'Hoạt động'", conn);
                conn.Open();
                return (int)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] CountActive NhanVien: {ex.Message}");
                return 0;
            }


        }
    }
