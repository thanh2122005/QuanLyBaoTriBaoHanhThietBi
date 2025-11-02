using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.DAL
{
    public class NguoiDungDAL
    {
        private readonly string _conn;

        public NguoiDungDAL(IConfiguration config)
        {
            _conn = config.GetConnectionString("DefaultConnection");
        }

        // =================== GET ALL ===================
        public List<NguoiDung> GetAll()
        {
            var list = new List<NguoiDung>();
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new("SELECT * FROM NguoiDung", conn);
            conn.Open();

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                list.Add(new NguoiDung
                {
                    MaNguoiDung = Convert.ToInt32(dr["MaNguoiDung"]),
                    TenDangNhap = dr["TenDangNhap"].ToString(),
                    MatKhauHash = dr["MatKhauHash"].ToString(),
                    Email = dr["Email"] == DBNull.Value ? null : dr["Email"].ToString(),
                    MaNV = dr["MaNV"] == DBNull.Value ? null : Convert.ToInt32(dr["MaNV"]),
                    VaiTroID = Convert.ToInt32(dr["VaiTroID"]),
                    TrangThai = dr["TrangThai"].ToString(),
                    NgayTao = Convert.ToDateTime(dr["NgayTao"]),
                    LanDangNhapCuoi = dr["LanDangNhapCuoi"] == DBNull.Value ? null : Convert.ToDateTime(dr["LanDangNhapCuoi"])
                });
            }

            return list;
        }

        // =================== GET BY ID ===================
        public NguoiDung? GetById(int id)
        {
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new("SELECT * FROM NguoiDung WHERE MaNguoiDung=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                return new NguoiDung
                {
                    MaNguoiDung = Convert.ToInt32(dr["MaNguoiDung"]),
                    TenDangNhap = dr["TenDangNhap"].ToString(),
                    MatKhauHash = dr["MatKhauHash"].ToString(),
                    Email = dr["Email"] == DBNull.Value ? null : dr["Email"].ToString(),
                    MaNV = dr["MaNV"] == DBNull.Value ? null : Convert.ToInt32(dr["MaNV"]),
                    VaiTroID = Convert.ToInt32(dr["VaiTroID"]),
                    TrangThai = dr["TrangThai"].ToString(),
                    NgayTao = Convert.ToDateTime(dr["NgayTao"]),
                    LanDangNhapCuoi = dr["LanDangNhapCuoi"] == DBNull.Value ? null : Convert.ToDateTime(dr["LanDangNhapCuoi"])
                };
            }
            return null;
        }

        // =================== ADD ===================
        public bool Add(NguoiDung nd)
        {
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new(@"
                INSERT INTO NguoiDung 
                    (TenDangNhap, MatKhauHash, Email, MaNV, VaiTroID, TrangThai, NgayTao, LanDangNhapCuoi)
                VALUES
                    (@TenDangNhap, @MatKhauHash, @Email, @MaNV, @VaiTroID, @TrangThai, @NgayTao, @LanDangNhapCuoi)", conn);

            cmd.Parameters.AddWithValue("@TenDangNhap", nd.TenDangNhap);
            cmd.Parameters.AddWithValue("@MatKhauHash", nd.MatKhauHash);
            cmd.Parameters.AddWithValue("@Email", (object?)nd.Email ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@MaNV", (object?)nd.MaNV ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@VaiTroID", nd.VaiTroID);
            cmd.Parameters.AddWithValue("@TrangThai", nd.TrangThai);
            cmd.Parameters.AddWithValue("@NgayTao", nd.NgayTao);
            cmd.Parameters.AddWithValue("@LanDangNhapCuoi", (object?)nd.LanDangNhapCuoi ?? DBNull.Value);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // =================== UPDATE ===================
        public bool Update(NguoiDung nd)
        {
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new(@"
                UPDATE NguoiDung
                SET TenDangNhap=@TenDangNhap, MatKhauHash=@MatKhauHash, Email=@Email,
                    MaNV=@MaNV, VaiTroID=@VaiTroID, TrangThai=@TrangThai,
                    LanDangNhapCuoi=@LanDangNhapCuoi
                WHERE MaNguoiDung=@MaNguoiDung", conn);

            cmd.Parameters.AddWithValue("@MaNguoiDung", nd.MaNguoiDung);
            cmd.Parameters.AddWithValue("@TenDangNhap", nd.TenDangNhap);
            cmd.Parameters.AddWithValue("@MatKhauHash", nd.MatKhauHash);
            cmd.Parameters.AddWithValue("@Email", (object?)nd.Email ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@MaNV", (object?)nd.MaNV ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@VaiTroID", nd.VaiTroID);
            cmd.Parameters.AddWithValue("@TrangThai", nd.TrangThai);
            cmd.Parameters.AddWithValue("@LanDangNhapCuoi", (object?)nd.LanDangNhapCuoi ?? DBNull.Value);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // =================== DELETE ===================
        public bool Delete(int id)
        {
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new("DELETE FROM NguoiDung WHERE MaNguoiDung=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // =================== GET BY ROLE ===================
        public List<NguoiDung> GetByRole(int roleId)
        {
            var list = new List<NguoiDung>();
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new("SELECT * FROM NguoiDung WHERE VaiTroID=@roleId", conn);
            cmd.Parameters.AddWithValue("@roleId", roleId);
            conn.Open();

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                list.Add(new NguoiDung
                {
                    MaNguoiDung = Convert.ToInt32(dr["MaNguoiDung"]),
                    TenDangNhap = dr["TenDangNhap"].ToString(),
                    MatKhauHash = dr["MatKhauHash"].ToString(),
                    Email = dr["Email"] == DBNull.Value ? null : dr["Email"].ToString(),
                    MaNV = dr["MaNV"] == DBNull.Value ? null : Convert.ToInt32(dr["MaNV"]),
                    VaiTroID = Convert.ToInt32(dr["VaiTroID"]),
                    TrangThai = dr["TrangThai"].ToString(),
                    NgayTao = Convert.ToDateTime(dr["NgayTao"]),
                    LanDangNhapCuoi = dr["LanDangNhapCuoi"] == DBNull.Value ? null : Convert.ToDateTime(dr["LanDangNhapCuoi"])
                });
            }
            return list;
        }

        // =================== GET BY USERNAME ===================
        public NguoiDung? GetByUsername(string username)
        {
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new("SELECT * FROM NguoiDung WHERE TenDangNhap=@username", conn);
            cmd.Parameters.AddWithValue("@username", username);
            conn.Open();

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                return new NguoiDung
                {
                    MaNguoiDung = Convert.ToInt32(dr["MaNguoiDung"]),
                    TenDangNhap = dr["TenDangNhap"].ToString(),
                    MatKhauHash = dr["MatKhauHash"].ToString(),
                    Email = dr["Email"] == DBNull.Value ? null : dr["Email"].ToString(),
                    MaNV = dr["MaNV"] == DBNull.Value ? null : Convert.ToInt32(dr["MaNV"]),
                    VaiTroID = Convert.ToInt32(dr["VaiTroID"]),
                    TrangThai = dr["TrangThai"].ToString(),
                    NgayTao = Convert.ToDateTime(dr["NgayTao"]),
                    LanDangNhapCuoi = dr["LanDangNhapCuoi"] == DBNull.Value ? null : Convert.ToDateTime(dr["LanDangNhapCuoi"])
                };
            }
            return null;
        }
    }
}
