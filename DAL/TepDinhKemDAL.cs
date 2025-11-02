using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.DAL
{
    public class TepDinhKemDAL
    {
        private readonly string _conn;

        public TepDinhKemDAL(IConfiguration config)
        {
            _conn = config.GetConnectionString("DefaultConnection");
        }

        // 🔹 GET ALL
        public List<TepDinhKem> GetAll()
        {
            var list = new List<TepDinhKem>();
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new("SELECT * FROM TepDinhKem ORDER BY MaTep DESC", conn);
            conn.Open();

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
                list.Add(MapToTep(dr));

            return list;
        }

        // 🔹 GET BY ID
        public TepDinhKem? GetById(int id)
        {
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new("SELECT * FROM TepDinhKem WHERE MaTep=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();

            SqlDataReader dr = cmd.ExecuteReader();
            return dr.Read() ? MapToTep(dr) : null;
        }

        // 🔹 ADD
        public bool Add(TepDinhKem t)
        {
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new(@"
                INSERT INTO TepDinhKem (TenTep, DuongDan, LoaiTep, NgayTaiLen)
                VALUES (@TenTep, @DuongDan, @LoaiTep, @NgayTaiLen)", conn);

            cmd.Parameters.AddWithValue("@TenTep", t.TenTep);
            cmd.Parameters.AddWithValue("@DuongDan", (object?)t.DuongDan ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@LoaiTep", (object?)t.LoaiTep ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@NgayTaiLen", t.NgayTaiLen);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // 🔹 UPDATE
        public bool Update(TepDinhKem t)
        {
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new(@"
                UPDATE TepDinhKem 
                SET TenTep=@TenTep, DuongDan=@DuongDan, LoaiTep=@LoaiTep, NgayTaiLen=@NgayTaiLen
                WHERE MaTep=@MaTep", conn);

            cmd.Parameters.AddWithValue("@MaTep", t.MaTep);
            cmd.Parameters.AddWithValue("@TenTep", t.TenTep);
            cmd.Parameters.AddWithValue("@DuongDan", (object?)t.DuongDan ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@LoaiTep", (object?)t.LoaiTep ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@NgayTaiLen", t.NgayTaiLen);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // 🔹 DELETE
        public bool Delete(int id)
        {
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new("DELETE FROM TepDinhKem WHERE MaTep=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // 🔹 MAP FUNCTION
        private static TepDinhKem MapToTep(SqlDataReader dr)
        {
            return new TepDinhKem
            {
                MaTep = Convert.ToInt32(dr["MaTep"]),
                TenTep = dr["TenTep"].ToString(),
                DuongDan = dr["DuongDan"] == DBNull.Value ? null : dr["DuongDan"].ToString(),
                LoaiTep = dr["LoaiTep"] == DBNull.Value ? null : dr["LoaiTep"].ToString(),
                NgayTaiLen = Convert.ToDateTime(dr["NgayTaiLen"])
            };
        }
    }
}
