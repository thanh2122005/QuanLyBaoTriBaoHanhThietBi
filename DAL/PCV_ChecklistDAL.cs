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
            var list = new List<PCV_Checklist>();
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new(@"
                SELECT c.ID, c.MaPhieuCV, c.ItemID, c.DaHoanThanh,
                       i.TenMuc, i.MoTa
                FROM PCV_Checklist c
                LEFT JOIN ChecklistItem i ON c.ItemID = i.ItemID", connection);

            connection.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())

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

        // ===================== TỔNG HỢP CHECKLIST =====================
        public List<(int MaPhieuCV, int TongSo, int HoanThanh, int ChuaHoanThanh, bool DaHoanTat)> GetSummary()
        {
            var list = new List<(int, int, int, int, bool)>();
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new(@"
                SELECT MaPhieuCV,
                       COUNT(*) AS TongSo,
                       SUM(CASE WHEN DaHoanThanh = 1 THEN 1 ELSE 0 END) AS HoanThanh,
                       SUM(CASE WHEN DaHoanThanh = 0 THEN 1 ELSE 0 END) AS ChuaHoanThanh,
                       CASE WHEN MIN(CASE WHEN DaHoanThanh = 1 THEN 1 ELSE 0 END) = 1 THEN 1 ELSE 0 END AS DaHoanTat
                FROM PCV_Checklist
                GROUP BY MaPhieuCV", connection);

            connection.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
                while (reader.Read())
            {
                summary.Add((
                    Convert.ToInt32(reader["MaPhieuCV"]),
                    Convert.ToInt32(reader["TongSo"]),
                    Convert.ToInt32(reader["HoanThanh"]),
                    Convert.ToInt32(reader["ChuaHoanThanh"]),
                    Convert.ToBoolean(reader["DaHoanTat"])
                ));
            }
            return summary;
        }


    }
}
