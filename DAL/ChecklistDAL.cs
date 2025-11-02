using Microsoft.Data.SqlClient;
using BaiMoiiii.Models;

namespace BaiMoiiii.DAL
{
    public class ChecklistDAL
    {
        private readonly string? _conn;

        public ChecklistDAL(IConfiguration config)
        {
            _conn = config.GetConnectionString("DefaultConnection");
        }

        // ===================== LẤY TẤT CẢ CHECKLIST =====================
        public List<Checklist> GetAll()
        {
            var list = new List<Checklist>();
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new("SELECT * FROM Checklist", conn);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                list.Add(new Checklist
                {
                    ChecklistID = Convert.ToInt32(dr["ChecklistID"]),
                    Ten = dr["Ten"].ToString() ?? "",
                    MoTa = dr["MoTa"] == DBNull.Value ? null : dr["MoTa"].ToString()
                });
            }
            return list;
        }

        // ===================== THÊM CHECKLIST =====================
        public bool Add(Checklist c)
        {
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new(@"INSERT INTO Checklist (Ten, MoTa)
                                   VALUES (@ten, @mota)", conn);
            cmd.Parameters.AddWithValue("@ten", c.Ten);
            cmd.Parameters.AddWithValue("@mota", (object?)c.MoTa ?? DBNull.Value);
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // ===================== CẬP NHẬT CHECKLIST =====================
        public bool Update(Checklist c)
        {
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new(@"UPDATE Checklist 
                                   SET Ten=@ten, MoTa=@mota 
                                   WHERE ChecklistID=@id", conn);
            cmd.Parameters.AddWithValue("@id", c.ChecklistID);
            cmd.Parameters.AddWithValue("@ten", c.Ten);
            cmd.Parameters.AddWithValue("@mota", (object?)c.MoTa ?? DBNull.Value);
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // ===================== XÓA CHECKLIST =====================
        public bool Delete(int id)
        {
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new("DELETE FROM Checklist WHERE ChecklistID=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }
    }
}
