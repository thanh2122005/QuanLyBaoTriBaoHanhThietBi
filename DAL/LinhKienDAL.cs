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

        // ==================== HÀM CHUYỂN DATAREADER THÀNH OBJECT ====================
        private LinhKien MapReader(SqlDataReader dr)
        {
            return new LinhKien
            {
                MaLinhKien = Convert.ToInt32(dr["MaLinhKien"]),
                TenLinhKien = dr["TenLinhKien"].ToString(),
                MaSo = dr["MaSo"] == DBNull.Value ? null : dr["MaSo"].ToString(),
                TonKho = Convert.ToInt32(dr["TonKho"])
            };
        }

        // ==================== GET ALL ====================
        public List<LinhKien> GetAll()
        {
            var list = new List<LinhKien>();
            try
            {
                using var conn = new SqlConnection(_conn);
                using var cmd = new SqlCommand("SELECT * FROM LinhKien ORDER BY MaLinhKien DESC", conn);
                conn.Open();

                using var dr = cmd.ExecuteReader();
                while (dr.Read())
                    list.Add(MapReader(dr));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] GetAll LinhKien: {ex.Message}");
            }
            return list;
        }

        // ==================== GET BY ID ====================
        public LinhKien? GetById(int id)
        {
            try
            {
                using var conn = new SqlConnection(_conn);
                using var cmd = new SqlCommand("SELECT * FROM LinhKien WHERE MaLinhKien = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();

                using var dr = cmd.ExecuteReader();
                return dr.Read() ? MapReader(dr) : null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] GetById LinhKien: {ex.Message}");
                return null;
            }
        }

        // ==================== ADD ====================
        public bool Add(LinhKien lk)
        {
            try
            {
                using var conn = new SqlConnection(_conn);
                using var cmd = new SqlCommand(@"
                    INSERT INTO LinhKien (TenLinhKien, MaSo, TonKho)
                    VALUES (@TenLinhKien, @MaSo, @TonKho)", conn);

                cmd.Parameters.AddWithValue("@TenLinhKien", lk.TenLinhKien);
                cmd.Parameters.AddWithValue("@MaSo", (object?)lk.MaSo ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@TonKho", lk.TonKho);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Add LinhKien: {ex.Message}");
                return false;
            }
        }

        // ==================== UPDATE ====================
        public bool Update(LinhKien lk)
        {
            try
            {
                using var conn = new SqlConnection(_conn);
                using var cmd = new SqlCommand(@"
                    UPDATE LinhKien
                    SET TenLinhKien = @TenLinhKien,
                        MaSo = @MaSo,
                        TonKho = @TonKho
                    WHERE MaLinhKien = @MaLinhKien", conn);

                cmd.Parameters.AddWithValue("@MaLinhKien", lk.MaLinhKien);
                cmd.Parameters.AddWithValue("@TenLinhKien", lk.TenLinhKien);
                cmd.Parameters.AddWithValue("@MaSo", (object?)lk.MaSo ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@TonKho", lk.TonKho);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Update LinhKien: {ex.Message}");
                return false;
            }
        }

        // ==================== DELETE ====================
        public bool Delete(int id)
        {
            try
            {
                using var conn = new SqlConnection(_conn);
                using var cmd = new SqlCommand("DELETE FROM LinhKien WHERE MaLinhKien = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();

                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Delete LinhKien: {ex.Message}");
                return false;
            }
        }
    }
}
