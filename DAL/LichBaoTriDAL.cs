using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.DAL
{
    public class LichBaoTriDAL
    {
        private readonly string _conn;

        public LichBaoTriDAL(IConfiguration config)
        {
            _conn = config.GetConnectionString("DefaultConnection");
        }

        // ==================== GET ALL ====================
        public List<LichBaoTri> GetAll()
        {
            var list = new List<LichBaoTri>();
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new("SELECT * FROM LichBaoTri", conn);
            conn.Open();

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                list.Add(new LichBaoTri
                {
                    MaLich = Convert.ToInt32(dr["MaLich"]),
                    MaTaiSan = Convert.ToInt32(dr["MaTaiSan"]),
                    MaNV = dr["MaNV"] == DBNull.Value ? null : Convert.ToInt32(dr["MaNV"]),
                    TanSuat = dr["TanSuat"].ToString(),
                    SoNgayLapLai = dr["SoNgayLapLai"] == DBNull.Value ? null : Convert.ToInt32(dr["SoNgayLapLai"]),
                    NgayKeTiep = Convert.ToDateTime(dr["NgayKeTiep"]),
                    ChecklistMacDinh = dr["ChecklistMacDinh"]?.ToString(),
                    HieuLuc = Convert.ToBoolean(dr["HieuLuc"])
                });
            }

            return list;
        }

        // ==================== GET BY ID ====================
        public LichBaoTri? GetById(int id)
        {
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new("SELECT * FROM LichBaoTri WHERE MaLich=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                return new LichBaoTri
                {
                    MaLich = Convert.ToInt32(dr["MaLich"]),
                    MaTaiSan = Convert.ToInt32(dr["MaTaiSan"]),
                    MaNV = dr["MaNV"] == DBNull.Value ? null : Convert.ToInt32(dr["MaNV"]),
                    TanSuat = dr["TanSuat"].ToString(),
                    SoNgayLapLai = dr["SoNgayLapLai"] == DBNull.Value ? null : Convert.ToInt32(dr["SoNgayLapLai"]),
                    NgayKeTiep = Convert.ToDateTime(dr["NgayKeTiep"]),
                    ChecklistMacDinh = dr["ChecklistMacDinh"]?.ToString(),
                    HieuLuc = Convert.ToBoolean(dr["HieuLuc"])
                };
            }
            return null;
        }

        // ==================== ADD ====================
        public bool Add(LichBaoTri l)
        {
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new(@"
                INSERT INTO LichBaoTri 
                    (MaTaiSan, MaNV, TanSuat, SoNgayLapLai, NgayKeTiep, ChecklistMacDinh, HieuLuc)
                VALUES 
                    (@MaTaiSan, @MaNV, @TanSuat, @SoNgayLapLai, @NgayKeTiep, @ChecklistMacDinh, @HieuLuc)", conn);

            cmd.Parameters.AddWithValue("@MaTaiSan", l.MaTaiSan);
            cmd.Parameters.AddWithValue("@MaNV", (object?)l.MaNV ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@TanSuat", l.TanSuat);
            cmd.Parameters.AddWithValue("@SoNgayLapLai", (object?)l.SoNgayLapLai ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@NgayKeTiep", l.NgayKeTiep);
            cmd.Parameters.AddWithValue("@ChecklistMacDinh", (object?)l.ChecklistMacDinh ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@HieuLuc", l.HieuLuc);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // ==================== UPDATE ====================
        public bool Update(LichBaoTri l)
        {
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new(@"
                UPDATE LichBaoTri
                SET MaTaiSan=@MaTaiSan, MaNV=@MaNV, TanSuat=@TanSuat,
                    SoNgayLapLai=@SoNgayLapLai, NgayKeTiep=@NgayKeTiep,
                    ChecklistMacDinh=@ChecklistMacDinh, HieuLuc=@HieuLuc
                WHERE MaLich=@MaLich", conn);

            cmd.Parameters.AddWithValue("@MaLich", l.MaLich);
            cmd.Parameters.AddWithValue("@MaTaiSan", l.MaTaiSan);
            cmd.Parameters.AddWithValue("@MaNV", (object?)l.MaNV ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@TanSuat", l.TanSuat);
            cmd.Parameters.AddWithValue("@SoNgayLapLai", (object?)l.SoNgayLapLai ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@NgayKeTiep", l.NgayKeTiep);
            cmd.Parameters.AddWithValue("@ChecklistMacDinh", (object?)l.ChecklistMacDinh ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@HieuLuc", l.HieuLuc);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // ==================== DELETE ====================
        public bool Delete(int id)
        {
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new("DELETE FROM LichBaoTri WHERE MaLich=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }
    }
}
