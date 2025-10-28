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


    }
}
