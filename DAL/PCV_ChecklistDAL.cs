using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.DAL
{
    public class PCV_ChecklistDAL
    {
        private readonly string _conn;

        public PCV_ChecklistDAL(IConfiguration config)
        {
            _conn = config.GetConnectionString("DefaultConnection");
        }

        // ===================== LẤY TOÀN BỘ CHECKLIST =====================
        public List<PCV_Checklist> GetAll()
        {
            List<PCV_Checklist> result = new();
            using SqlConnection connection = new(_connectionString);
            SqlCommand command = new(@"
                SELECT c.ID, c.MaPhieuCV, c.ItemID, c.DaHoanThanh,
                       i.TenMuc, i.MoTa
                FROM PCV_Checklist c
                LEFT JOIN ChecklistItem i ON c.ItemID = i.ItemID", connection);

            connection.Open();
            Console.WriteLine("Đang truy vấn dữ liệu checklist...");
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                result.Add(new PCV_Checklist
                {
                    ID = Convert.ToInt32(reader["ID"]),
                    MaPhieuCV = Convert.ToInt32(reader["MaPhieuCV"]),
                    ItemID = Convert.ToInt32(reader["ItemID"]),
                    DaHoanThanh = Convert.ToBoolean(reader["DaHoanThanh"]),
                    TenMuc = reader["TenMuc"] as string,
                    MoTa = reader["MoTa"] as string
                });
            }
            return result;
        }

    }
}
