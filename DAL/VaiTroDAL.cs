using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.DAL
{
    public class VaiTroDAL
    {
        private readonly string _conn;

        public VaiTroDAL(IConfiguration config)
        {
            _conn = config.GetConnectionString("DefaultConnection");
        }

        // ========== GET ALL ==========
        public List<VaiTro> GetAll()
        {
            var list = new List<VaiTro>();
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new("SELECT * FROM VaiTro ORDER BY VaiTroID", conn);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
                list.Add(MapToVaiTro(dr));
            return list;
        }

        // ========== GET BY ID ==========
        public VaiTro? GetById(int id)
        {
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new("SELECT * FROM VaiTro WHERE VaiTroID=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            return dr.Read() ? MapToVaiTro(dr) : null;
        }

        // ========== GET BY NAME ==========
        public VaiTro? GetByName(string name)
        {
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new("SELECT * FROM VaiTro WHERE TenVaiTro=@name", conn);
            cmd.Parameters.AddWithValue("@name", name);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            return dr.Read() ? MapToVaiTro(dr) : null;
        }

        // ========== ADD ==========
        public bool Add(VaiTro vt)
        {
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new(@"
                INSERT INTO VaiTro (TenVaiTro, MoTa, QuyenJSON)
                VALUES (@TenVaiTro, @MoTa, @QuyenJSON)", conn);

            cmd.Parameters.AddWithValue("@TenVaiTro", vt.TenVaiTro);
            cmd.Parameters.AddWithValue("@MoTa", (object?)vt.MoTa ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@QuyenJSON", (object?)vt.QuyenJSON ?? DBNull.Value);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // ========== UPDATE ==========
        public bool Update(VaiTro vt)
        {
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new(@"
                UPDATE VaiTro 
                SET TenVaiTro=@TenVaiTro, MoTa=@MoTa, QuyenJSON=@QuyenJSON
                WHERE VaiTroID=@VaiTroID", conn);

            cmd.Parameters.AddWithValue("@VaiTroID", vt.VaiTroID);
            cmd.Parameters.AddWithValue("@TenVaiTro", vt.TenVaiTro);
            cmd.Parameters.AddWithValue("@MoTa", (object?)vt.MoTa ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@QuyenJSON", (object?)vt.QuyenJSON ?? DBNull.Value);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // ========== DELETE ==========
        public bool Delete(int id)
        {
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new("DELETE FROM VaiTro WHERE VaiTroID=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // ========== MAP FUNCTION ==========
        private static VaiTro MapToVaiTro(SqlDataReader dr)
        {
            return new VaiTro
            {
                VaiTroID = Convert.ToInt32(dr["VaiTroID"]),
                TenVaiTro = dr["TenVaiTro"].ToString(),
                MoTa = dr["MoTa"] == DBNull.Value ? null : dr["MoTa"].ToString(),
                QuyenJSON = dr["QuyenJSON"] == DBNull.Value ? null : dr["QuyenJSON"].ToString()
            };
        }
    }
}
