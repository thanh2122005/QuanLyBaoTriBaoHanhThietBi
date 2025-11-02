using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.DAL
{
    public class QuyenDAL
    {
        private readonly string _conn;

        public QuyenDAL(IConfiguration config)
        {
            _conn = config.GetConnectionString("DefaultConnection");
        }

        // ===================== GET ALL =====================
        public List<Quyen> GetAll()
        {
            var list = new List<Quyen>();
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new("SELECT * FROM Quyen ORDER BY QuyenID", conn);

            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                list.Add(MapToQuyen(dr));
            }
            return list;
        }

        // ===================== GET BY ID =====================
        public Quyen? GetById(int id)
        {
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new("SELECT * FROM Quyen WHERE QuyenID=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);

            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            return dr.Read() ? MapToQuyen(dr) : null;
        }

        // ===================== ADD =====================
        public bool Add(Quyen q)
        {
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new(@"
                INSERT INTO Quyen (TenQuyen, Nhom, MoTa)
                VALUES (@TenQuyen, @Nhom, @MoTa)", conn);

            cmd.Parameters.AddWithValue("@TenQuyen", q.TenQuyen);
            cmd.Parameters.AddWithValue("@Nhom", (object?)q.Nhom ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@MoTa", (object?)q.MoTa ?? DBNull.Value);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // ===================== UPDATE =====================
        public bool Update(Quyen q)
        {
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new(@"
                UPDATE Quyen 
                SET TenQuyen=@TenQuyen, Nhom=@Nhom, MoTa=@MoTa
                WHERE QuyenID=@QuyenID", conn);

            cmd.Parameters.AddWithValue("@QuyenID", q.QuyenID);
            cmd.Parameters.AddWithValue("@TenQuyen", q.TenQuyen);
            cmd.Parameters.AddWithValue("@Nhom", (object?)q.Nhom ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@MoTa", (object?)q.MoTa ?? DBNull.Value);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // ===================== DELETE =====================
        public bool Delete(int id)
        {
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new("DELETE FROM Quyen WHERE QuyenID=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // ===================== GET BY GROUP =====================
        public List<Quyen> GetByGroup(string nhom)
        {
            var list = new List<Quyen>();
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new("SELECT * FROM Quyen WHERE Nhom=@nhom", conn);
            cmd.Parameters.AddWithValue("@nhom", nhom);

            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                list.Add(MapToQuyen(dr));
            }
            return list;
        }

        // ===================== SEARCH =====================
        public List<Quyen> Search(string keyword)
        {
            var list = new List<Quyen>();
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new(@"
                SELECT * FROM Quyen 
                WHERE TenQuyen LIKE @kw OR MoTa LIKE @kw", conn);
            cmd.Parameters.AddWithValue("@kw", "%" + keyword + "%");

            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                list.Add(MapToQuyen(dr));
            }
            return list;
        }

        // ===================== MAP FUNCTION =====================
        private static Quyen MapToQuyen(SqlDataReader dr)
        {
            return new Quyen
            {
                QuyenID = Convert.ToInt32(dr["QuyenID"]),
                TenQuyen = dr["TenQuyen"].ToString(),
                Nhom = dr["Nhom"] == DBNull.Value ? null : dr["Nhom"].ToString(),
                MoTa = dr["MoTa"] == DBNull.Value ? null : dr["MoTa"].ToString()
            };
        }
    }
}
