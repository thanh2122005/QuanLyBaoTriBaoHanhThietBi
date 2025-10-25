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
    }
}
