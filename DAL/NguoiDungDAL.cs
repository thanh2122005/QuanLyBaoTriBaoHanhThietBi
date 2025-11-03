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


    }
}
