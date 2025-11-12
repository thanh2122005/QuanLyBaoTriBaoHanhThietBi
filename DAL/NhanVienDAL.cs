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

        // ==================== GET ALL ====================
        public List<NhanVien> GetAll()
        {
            var list = new List<NhanVien>();
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new("SELECT * FROM NhanVien", conn);
            conn.Open();

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                list.Add(new NhanVien
                {
                    MaNV = Convert.ToInt32(dr["MaNV"]),
                    HoTen = dr["HoTen"].ToString(),
                    SoDienThoai = dr["SoDienThoai"] == DBNull.Value ? null : dr["SoDienThoai"].ToString(),
                    Email = dr["Email"] == DBNull.Value ? null : dr["Email"].ToString(),
                    TrangThai = dr["TrangThai"].ToString()
                });
            }

            return list;
        }

        // ==================== GET BY ID ====================
        public NhanVien? GetById(int id)
        {
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new("SELECT * FROM NhanVien WHERE MaNV=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
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

            return null;
        }

        // ==================== ADD ====================
        public bool Add(NhanVien nv)
        {
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new(@"
                INSERT INTO NhanVien (HoTen, SoDienThoai, Email, TrangThai)
                VALUES (@HoTen, @SoDienThoai, @Email, @TrangThai)", conn);

            cmd.Parameters.AddWithValue("@HoTen", nv.HoTen);
            cmd.Parameters.AddWithValue("@SoDienThoai", (object?)nv.SoDienThoai ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Email", (object?)nv.Email ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@TrangThai", nv.TrangThai);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // ==================== UPDATE ====================
        public bool Update(NhanVien nv)
        {
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new(@"
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

        // ==================== DELETE ====================
        public bool Delete(int id)
        {
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new("DELETE FROM NhanVien WHERE MaNV=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // ==================== KPI: SỐ NHÂN VIÊN HOẠT ĐỘNG ====================
        public int CountActive()
        {
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new("SELECT COUNT(*) FROM NhanVien WHERE TrangThai = N'Hoạt động'", conn);
            conn.Open();
            return (int)cmd.ExecuteScalar();
        }
    }
}
