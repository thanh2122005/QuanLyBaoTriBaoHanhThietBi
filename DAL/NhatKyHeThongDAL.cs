using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.DAL
{
    public class NhatKyHeThongDAL
    {
        private readonly string _conn;

        public NhatKyHeThongDAL(IConfiguration config)
        {
            _conn = config.GetConnectionString("DefaultConnection");
        }

        // ===================== GET ALL =====================
        public List<NhatKyHeThong> GetAll()
        {
            var list = new List<NhatKyHeThong>();
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new("SELECT * FROM NhatKyHeThong ORDER BY ThoiGian DESC", conn);
            conn.Open();

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                list.Add(new NhatKyHeThong
                {
                    MaLog = Convert.ToInt64(dr["MaLog"]),
                    TenBang = dr["TenBang"].ToString(),
                    MaBanGhi = Convert.ToInt32(dr["MaBanGhi"]),
                    HanhDong = dr["HanhDong"].ToString(),
                    GiaTriCu = dr["GiaTriCu"] == DBNull.Value ? null : dr["GiaTriCu"].ToString(),
                    GiaTriMoi = dr["GiaTriMoi"] == DBNull.Value ? null : dr["GiaTriMoi"].ToString(),
                    ThayDoiBoi = dr["ThayDoiBoi"] == DBNull.Value ? null : dr["ThayDoiBoi"].ToString(),
                    ThoiGian = Convert.ToDateTime(dr["ThoiGian"])
                });
            }

            return list;
        }

        // ===================== GET BY ID =====================
        public NhatKyHeThong? GetById(long id)
        {
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new("SELECT * FROM NhatKyHeThong WHERE MaLog=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                return new NhatKyHeThong
                {
                    MaLog = Convert.ToInt64(dr["MaLog"]),
                    TenBang = dr["TenBang"].ToString(),
                    MaBanGhi = Convert.ToInt32(dr["MaBanGhi"]),
                    HanhDong = dr["HanhDong"].ToString(),
                    GiaTriCu = dr["GiaTriCu"] == DBNull.Value ? null : dr["GiaTriCu"].ToString(),
                    GiaTriMoi = dr["GiaTriMoi"] == DBNull.Value ? null : dr["GiaTriMoi"].ToString(),
                    ThayDoiBoi = dr["ThayDoiBoi"] == DBNull.Value ? null : dr["ThayDoiBoi"].ToString(),
                    ThoiGian = Convert.ToDateTime(dr["ThoiGian"])
                };
            }

            return null;
        }

        // ===================== ADD =====================
        public bool Add(NhatKyHeThong log)
        {
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new(@"
                INSERT INTO NhatKyHeThong 
                    (TenBang, MaBanGhi, HanhDong, GiaTriCu, GiaTriMoi, ThayDoiBoi, ThoiGian)
                VALUES 
                    (@TenBang, @MaBanGhi, @HanhDong, @GiaTriCu, @GiaTriMoi, @ThayDoiBoi, @ThoiGian)", conn);

            cmd.Parameters.AddWithValue("@TenBang", log.TenBang);
            cmd.Parameters.AddWithValue("@MaBanGhi", log.MaBanGhi);
            cmd.Parameters.AddWithValue("@HanhDong", log.HanhDong);
            cmd.Parameters.AddWithValue("@GiaTriCu", (object?)log.GiaTriCu ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@GiaTriMoi", (object?)log.GiaTriMoi ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ThayDoiBoi", (object?)log.ThayDoiBoi ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ThoiGian", log.ThoiGian);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // ===================== UPDATE =====================
        public bool Update(NhatKyHeThong log)
        {
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new(@"
                UPDATE NhatKyHeThong
                SET TenBang=@TenBang, MaBanGhi=@MaBanGhi, HanhDong=@HanhDong,
                    GiaTriCu=@GiaTriCu, GiaTriMoi=@GiaTriMoi, ThayDoiBoi=@ThayDoiBoi, ThoiGian=@ThoiGian
                WHERE MaLog=@MaLog", conn);

            cmd.Parameters.AddWithValue("@MaLog", log.MaLog);
            cmd.Parameters.AddWithValue("@TenBang", log.TenBang);
            cmd.Parameters.AddWithValue("@MaBanGhi", log.MaBanGhi);
            cmd.Parameters.AddWithValue("@HanhDong", log.HanhDong);
            cmd.Parameters.AddWithValue("@GiaTriCu", (object?)log.GiaTriCu ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@GiaTriMoi", (object?)log.GiaTriMoi ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ThayDoiBoi", (object?)log.ThayDoiBoi ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ThoiGian", log.ThoiGian);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // ===================== DELETE =====================
        public bool Delete(long id)
        {
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new("DELETE FROM NhatKyHeThong WHERE MaLog=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }
    }
}
