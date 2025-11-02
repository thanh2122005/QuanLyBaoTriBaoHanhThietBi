using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.DAL
{
    public class PhieuKhoDAL
    {
        private readonly string _conn;

        public PhieuKhoDAL(IConfiguration config)
        {
            _conn = config.GetConnectionString("DefaultConnection");
        }

        // ===================== GET ALL =====================
        public List<PhieuKho> GetAll()
        {
            var list = new List<PhieuKho>();
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new("SELECT * FROM PhieuKho ORDER BY NgayLap DESC", conn);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                list.Add(new PhieuKho
                {
                    MaPhieuKho = Convert.ToInt32(dr["MaPhieuKho"]),
                    Loai = dr["Loai"].ToString(),
                    NgayLap = Convert.ToDateTime(dr["NgayLap"]),
                    GhiChu = dr["GhiChu"] == DBNull.Value ? null : dr["GhiChu"].ToString()
                });
            }
            return list;
        }

        // ===================== GET BY ID =====================
        public PhieuKho? GetById(int id)
        {
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new("SELECT * FROM PhieuKho WHERE MaPhieuKho=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                return new PhieuKho
                {
                    MaPhieuKho = Convert.ToInt32(dr["MaPhieuKho"]),
                    Loai = dr["Loai"].ToString(),
                    NgayLap = Convert.ToDateTime(dr["NgayLap"]),
                    GhiChu = dr["GhiChu"] == DBNull.Value ? null : dr["GhiChu"].ToString()
                };
            }
            return null;
        }

        // ===================== ADD =====================
        public bool Add(PhieuKho pk)
        {
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new(@"
                INSERT INTO PhieuKho (Loai, NgayLap, GhiChu)
                VALUES (@Loai, @NgayLap, @GhiChu)", conn);

            cmd.Parameters.AddWithValue("@Loai", pk.Loai);
            cmd.Parameters.AddWithValue("@NgayLap", pk.NgayLap);
            cmd.Parameters.AddWithValue("@GhiChu", (object?)pk.GhiChu ?? DBNull.Value);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // ===================== UPDATE =====================
        public bool Update(PhieuKho pk)
        {
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new(@"
                UPDATE PhieuKho 
                SET Loai=@Loai, NgayLap=@NgayLap, GhiChu=@GhiChu
                WHERE MaPhieuKho=@MaPhieuKho", conn);

            cmd.Parameters.AddWithValue("@Loai", pk.Loai);
            cmd.Parameters.AddWithValue("@NgayLap", pk.NgayLap);
            cmd.Parameters.AddWithValue("@GhiChu", (object?)pk.GhiChu ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@MaPhieuKho", pk.MaPhieuKho);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // ===================== DELETE =====================
        public bool Delete(int id)
        {
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new("DELETE FROM PhieuKho WHERE MaPhieuKho=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // ===================== GET BY TYPE =====================
        public List<PhieuKho> GetByType(string loai)
        {
            var list = new List<PhieuKho>();
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new("SELECT * FROM PhieuKho WHERE Loai=@loai ORDER BY NgayLap DESC", conn);
            cmd.Parameters.AddWithValue("@loai", loai);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                list.Add(new PhieuKho
                {
                    MaPhieuKho = Convert.ToInt32(dr["MaPhieuKho"]),
                    Loai = dr["Loai"].ToString(),
                    NgayLap = Convert.ToDateTime(dr["NgayLap"]),
                    GhiChu = dr["GhiChu"] == DBNull.Value ? null : dr["GhiChu"].ToString()
                });
            }
            return list;
        }

        // ===================== KPI SUMMARY =====================
        public List<(string Loai, int SoLuong)> GetKpiSummary()
        {
            var list = new List<(string, int)>();
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new(@"
                SELECT Loai, COUNT(*) AS SoLuong 
                FROM PhieuKho 
                GROUP BY Loai", conn);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                list.Add((dr["Loai"].ToString(), Convert.ToInt32(dr["SoLuong"])));
            }
            return list;
        }

        // ===================== KPI TOTAL VALUE =====================
        public List<PhieuKho> GetTotalValue()
        {
            var list = new List<PhieuKho>();
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new(@"
                SELECT pk.MaPhieuKho, pk.Loai, pk.NgayLap, pk.GhiChu,
                       ISNULL(SUM(ct.SoLuong * ct.DonGia), 0) AS TongGiaTri
                FROM PhieuKho pk
                LEFT JOIN PhieuKho_ChiTiet ct ON pk.MaPhieuKho = ct.MaPhieuKho
                GROUP BY pk.MaPhieuKho, pk.Loai, pk.NgayLap, pk.GhiChu
                ORDER BY pk.NgayLap DESC", conn);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                list.Add(new PhieuKho
                {
                    MaPhieuKho = Convert.ToInt32(dr["MaPhieuKho"]),
                    Loai = dr["Loai"].ToString(),
                    NgayLap = Convert.ToDateTime(dr["NgayLap"]),
                    GhiChu = dr["GhiChu"] == DBNull.Value ? null : dr["GhiChu"].ToString(),
                    TongGiaTri = Convert.ToDecimal(dr["TongGiaTri"])
                });
            }
            return list;
        }
    }
}
