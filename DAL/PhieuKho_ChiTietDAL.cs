using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.DAL
{
    public class PhieuKho_ChiTietDAL
    {
        private readonly string _conn;

        public PhieuKho_ChiTietDAL(IConfiguration config)
        {
            _conn = config.GetConnectionString("DefaultConnection");
        }

        // ===================== GET ALL =====================
        public List<PhieuKho_ChiTiet> GetAll()
        {
            var list = new List<PhieuKho_ChiTiet>();
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new(@"
                SELECT ct.MaCT, ct.MaPhieuKho, ct.MaLinhKien, ct.SoLuong, ct.DonGia,
                       lk.TenLinhKien, pk.Loai AS LoaiPhieu, pk.NgayLap
                FROM PhieuKho_ChiTiet ct
                LEFT JOIN LinhKien lk ON ct.MaLinhKien = lk.MaLinhKien
                LEFT JOIN PhieuKho pk ON ct.MaPhieuKho = pk.MaPhieuKho", conn);

            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                list.Add(new PhieuKho_ChiTiet
                {
                    MaCT = Convert.ToInt32(dr["MaCT"]),
                    MaPhieuKho = Convert.ToInt32(dr["MaPhieuKho"]),
                    MaLinhKien = Convert.ToInt32(dr["MaLinhKien"]),
                    SoLuong = Convert.ToInt32(dr["SoLuong"]),
                    DonGia = dr["DonGia"] == DBNull.Value ? null : Convert.ToDecimal(dr["DonGia"]),
                    TenLinhKien = dr["TenLinhKien"] == DBNull.Value ? null : dr["TenLinhKien"].ToString(),
                    LoaiPhieu = dr["LoaiPhieu"] == DBNull.Value ? null : dr["LoaiPhieu"].ToString(),
                    NgayLap = dr["NgayLap"] == DBNull.Value ? null : Convert.ToDateTime(dr["NgayLap"])
                });
            }
            return list;
        }

        // ===================== GET BY ID =====================
        public PhieuKho_ChiTiet? GetById(int id)
        {
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new(@"
                SELECT ct.MaCT, ct.MaPhieuKho, ct.MaLinhKien, ct.SoLuong, ct.DonGia,
                       lk.TenLinhKien, pk.Loai AS LoaiPhieu, pk.NgayLap
                FROM PhieuKho_ChiTiet ct
                LEFT JOIN LinhKien lk ON ct.MaLinhKien = lk.MaLinhKien
                LEFT JOIN PhieuKho pk ON ct.MaPhieuKho = pk.MaPhieuKho
                WHERE ct.MaCT = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);

            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                return new PhieuKho_ChiTiet
                {
                    MaCT = Convert.ToInt32(dr["MaCT"]),
                    MaPhieuKho = Convert.ToInt32(dr["MaPhieuKho"]),
                    MaLinhKien = Convert.ToInt32(dr["MaLinhKien"]),
                    SoLuong = Convert.ToInt32(dr["SoLuong"]),
                    DonGia = dr["DonGia"] == DBNull.Value ? null : Convert.ToDecimal(dr["DonGia"]),
                    TenLinhKien = dr["TenLinhKien"] == DBNull.Value ? null : dr["TenLinhKien"].ToString(),
                    LoaiPhieu = dr["LoaiPhieu"] == DBNull.Value ? null : dr["LoaiPhieu"].ToString(),
                    NgayLap = dr["NgayLap"] == DBNull.Value ? null : Convert.ToDateTime(dr["NgayLap"])
                };
            }
            return null;
        }

        // ===================== ADD =====================
        public bool Add(PhieuKho_ChiTiet ct)
        {
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new(@"
                INSERT INTO PhieuKho_ChiTiet (MaPhieuKho, MaLinhKien, SoLuong, DonGia)
                VALUES (@MaPhieuKho, @MaLinhKien, @SoLuong, @DonGia)", conn);

            cmd.Parameters.AddWithValue("@MaPhieuKho", ct.MaPhieuKho);
            cmd.Parameters.AddWithValue("@MaLinhKien", ct.MaLinhKien);
            cmd.Parameters.AddWithValue("@SoLuong", ct.SoLuong);
            cmd.Parameters.AddWithValue("@DonGia", (object?)ct.DonGia ?? DBNull.Value);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // ===================== UPDATE =====================
        public bool Update(PhieuKho_ChiTiet ct)
        {
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new(@"
                UPDATE PhieuKho_ChiTiet
                SET MaPhieuKho=@MaPhieuKho, MaLinhKien=@MaLinhKien,
                    SoLuong=@SoLuong, DonGia=@DonGia
                WHERE MaCT=@MaCT", conn);

            cmd.Parameters.AddWithValue("@MaCT", ct.MaCT);
            cmd.Parameters.AddWithValue("@MaPhieuKho", ct.MaPhieuKho);
            cmd.Parameters.AddWithValue("@MaLinhKien", ct.MaLinhKien);
            cmd.Parameters.AddWithValue("@SoLuong", ct.SoLuong);
            cmd.Parameters.AddWithValue("@DonGia", (object?)ct.DonGia ?? DBNull.Value);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // ===================== DELETE =====================
        public bool Delete(int id)
        {
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new("DELETE FROM PhieuKho_ChiTiet WHERE MaCT=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // ===================== GET BY PHIEU =====================
        public List<PhieuKho_ChiTiet> GetByPhieu(int maPhieu)
        {
            var list = new List<PhieuKho_ChiTiet>();
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new(@"
                SELECT ct.*, lk.TenLinhKien
                FROM PhieuKho_ChiTiet ct
                LEFT JOIN LinhKien lk ON ct.MaLinhKien = lk.MaLinhKien
                WHERE ct.MaPhieuKho = @maPhieu", conn);
            cmd.Parameters.AddWithValue("@maPhieu", maPhieu);

            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                list.Add(new PhieuKho_ChiTiet
                {
                    MaCT = Convert.ToInt32(dr["MaCT"]),
                    MaPhieuKho = Convert.ToInt32(dr["MaPhieuKho"]),
                    MaLinhKien = Convert.ToInt32(dr["MaLinhKien"]),
                    SoLuong = Convert.ToInt32(dr["SoLuong"]),
                    DonGia = dr["DonGia"] == DBNull.Value ? null : Convert.ToDecimal(dr["DonGia"]),
                    TenLinhKien = dr["TenLinhKien"] == DBNull.Value ? null : dr["TenLinhKien"].ToString()
                });
            }
            return list;
        }
    }
}
