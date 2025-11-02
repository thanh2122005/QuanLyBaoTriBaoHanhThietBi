using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.DAL
{
    public class LinhKienDAL
    {
        private readonly string _conn;

        public LinhKienDAL(IConfiguration config)
        {
            _conn = config.GetConnectionString("DefaultConnection");
        }

        // ==================== GET ALL ====================
        public List<LinhKien> GetAll()
        {
            var list = new List<LinhKien>();
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new("SELECT * FROM LinhKien", conn);
            conn.Open();

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                list.Add(new LinhKien
                {
                    MaLinhKien = Convert.ToInt32(dr["MaLinhKien"]),
                    TenLinhKien = dr["TenLinhKien"].ToString(),
                    MaSo = dr["MaSo"] == DBNull.Value ? null : dr["MaSo"].ToString(),
                    TonKho = Convert.ToInt32(dr["TonKho"])
                });
            }

            return list;
        }

        // ==================== GET BY ID ====================
        public LinhKien? GetById(int id)
        {
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new("SELECT * FROM LinhKien WHERE MaLinhKien=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                return new LinhKien
                {
                    MaLinhKien = Convert.ToInt32(dr["MaLinhKien"]),
                    TenLinhKien = dr["TenLinhKien"].ToString(),
                    MaSo = dr["MaSo"] == DBNull.Value ? null : dr["MaSo"].ToString(),
                    TonKho = Convert.ToInt32(dr["TonKho"])
                };
            }

            return null;
        }

        // ==================== ADD ====================
        public bool Add(LinhKien lk)
        {
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new(@"
                INSERT INTO LinhKien (TenLinhKien, MaSo, TonKho)
                VALUES (@TenLinhKien, @MaSo, @TonKho)", conn);

            cmd.Parameters.AddWithValue("@TenLinhKien", lk.TenLinhKien);
            cmd.Parameters.AddWithValue("@MaSo", (object?)lk.MaSo ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@TonKho", lk.TonKho);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // ==================== UPDATE ====================
        public bool Update(LinhKien lk)
        {
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new(@"
                UPDATE LinhKien
                SET TenLinhKien=@TenLinhKien, MaSo=@MaSo, TonKho=@TonKho
                WHERE MaLinhKien=@MaLinhKien", conn);

            cmd.Parameters.AddWithValue("@MaLinhKien", lk.MaLinhKien);
            cmd.Parameters.AddWithValue("@TenLinhKien", lk.TenLinhKien);
            cmd.Parameters.AddWithValue("@MaSo", (object?)lk.MaSo ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@TonKho", lk.TonKho);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // ==================== DELETE ====================
        public bool Delete(int id)
        {
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new("DELETE FROM LinhKien WHERE MaLinhKien=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }
    }
}
