using Microsoft.Data.SqlClient;
using BaiMoiiii.Models;

namespace BaiMoiiii.DAL
{
    public class ChecklistItemDAL
    {
        private readonly string? _conn;

        public ChecklistItemDAL(IConfiguration config)
        {
            _conn = config.GetConnectionString("DefaultConnection");
        }

        // ===================== LẤY DANH SÁCH THEO CHECKLIST =====================
        public List<ChecklistItem> GetByChecklist(int checklistId)
        {
            var list = new List<ChecklistItem>();
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new("SELECT * FROM ChecklistItem WHERE ChecklistID=@id", conn);
            cmd.Parameters.AddWithValue("@id", checklistId);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                list.Add(new ChecklistItem
                {
                    ItemID = Convert.ToInt32(dr["ItemID"]),
                    ChecklistID = Convert.ToInt32(dr["ChecklistID"]),
                    NoiDung = dr["NoiDung"].ToString() ?? ""
                });
            }
            return list;
        }

        // ===================== THÊM =====================
        public bool Add(ChecklistItem item)
        {
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new(@"INSERT INTO ChecklistItem (ChecklistID, NoiDung)
                                   VALUES (@cid, @nd)", conn);
            cmd.Parameters.AddWithValue("@cid", item.ChecklistID);
            cmd.Parameters.AddWithValue("@nd", item.NoiDung);
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // ===================== CẬP NHẬT =====================
        public bool Update(ChecklistItem item)
        {
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new(@"UPDATE ChecklistItem 
                                   SET NoiDung=@nd 
                                   WHERE ItemID=@id", conn);
            cmd.Parameters.AddWithValue("@id", item.ItemID);
            cmd.Parameters.AddWithValue("@nd", item.NoiDung);
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // ===================== XÓA =====================
        public bool Delete(int id)
        {
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new("DELETE FROM ChecklistItem WHERE ItemID=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }
    }
}
