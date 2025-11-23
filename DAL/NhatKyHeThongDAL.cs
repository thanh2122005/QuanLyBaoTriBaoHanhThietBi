using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.DAL
{
    public class NhatKyHeThongDAL
    {
        private readonly string _connectionString;

        public NhatKyHeThongDAL(string connectionString)
        {
            _connectionString = connectionString;
        }

        // ======================= GET ALL =======================
        public List<NhatKyHeThong> GetAll()
        {
            var list = new List<NhatKyHeThong>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM NhatKyHeThong ORDER BY ThoiGian DESC";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    list.Add(MapToNK(dr));
                }
            }
            return list;
        }

        // ======================= INSERT =======================
        public bool Insert(NhatKyHeThong log)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"INSERT INTO NhatKyHeThong 
                                (TenBang, MaBanGhi, HanhDong, GiaTriCu, GiaTriMoi, ThayDoiBoi)
                                 VALUES (@TenBang, @MaBanGhi, @HanhDong, @GiaTriCu, @GiaTriMoi, @ThayDoiBoi)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TenBang", log.TenBang);
                cmd.Parameters.AddWithValue("@MaBanGhi", log.MaBanGhi);
                cmd.Parameters.AddWithValue("@HanhDong", log.HanhDong);
                cmd.Parameters.AddWithValue("@GiaTriCu", (object)log.GiaTriCu ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@GiaTriMoi", (object)log.GiaTriMoi ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@ThayDoiBoi", (object)log.ThayDoiBoi ?? DBNull.Value);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // ======================= DELETE =======================
        public bool Delete(long id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM NhatKyHeThong WHERE MaLog = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // ======================= PAGED =======================
        public List<NhatKyHeThong> GetPaged(int page, int size)
        {
            var list = new List<NhatKyHeThong>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"
                    SELECT *
                    FROM NhatKyHeThong
                    ORDER BY ThoiGian DESC
                    OFFSET (@skip) ROWS
                    FETCH NEXT @size ROWS ONLY;
                ";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@skip", (page - 1) * size);
                cmd.Parameters.AddWithValue("@size", size);

                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    list.Add(MapToNK(dr));
                }
            }

            return list;
        }

        // ======================= COUNT =======================
        public int CountAll()
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT COUNT(*) FROM NhatKyHeThong";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                return (int)cmd.ExecuteScalar();
            }
        }

        // ======================= MAP FUNCTION =======================
        private static NhatKyHeThong MapToNK(SqlDataReader dr)
        {
            return new NhatKyHeThong
            {
                MaLog = (long)dr["MaLog"],
                TenBang = dr["TenBang"].ToString(),
                MaBanGhi = Convert.ToInt32(dr["MaBanGhi"]),
                HanhDong = dr["HanhDong"].ToString(),
                GiaTriCu = dr["GiaTriCu"]?.ToString(),
                GiaTriMoi = dr["GiaTriMoi"]?.ToString(),
                ThayDoiBoi = dr["ThayDoiBoi"]?.ToString(),
                ThoiGian = Convert.ToDateTime(dr["ThoiGian"])
            };
        }
    }
}
