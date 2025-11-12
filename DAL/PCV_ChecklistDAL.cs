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

        // ===================== GET ALL =====================
        public List<PCV_Checklist> GetAll()
        {
            var list = new List<PCV_Checklist>();
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new(@"
                SELECT c.ID, c.MaPhieuCV, c.ItemID, c.DaHoanThanh,
                       i.TenMuc, i.MoTa
                FROM PCV_Checklist c
                LEFT JOIN ChecklistItem i ON c.ItemID = i.ItemID", conn);

            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                list.Add(new PCV_Checklist
                {
                    ID = Convert.ToInt32(dr["ID"]),
                    MaPhieuCV = Convert.ToInt32(dr["MaPhieuCV"]),
                    ItemID = Convert.ToInt32(dr["ItemID"]),
                    DaHoanThanh = Convert.ToBoolean(dr["DaHoanThanh"]),
                    TenMuc = dr["TenMuc"] == DBNull.Value ? null : dr["TenMuc"].ToString(),
                    MoTa = dr["MoTa"] == DBNull.Value ? null : dr["MoTa"].ToString()
                });
            }
            return list;
        }

        // ===================== GET SUMMARY =====================
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
                GROUP BY MaPhieuCV", conn);

            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                list.Add((
                    Convert.ToInt32(dr["MaPhieuCV"]),
                    Convert.ToInt32(dr["TongSo"]),
                    Convert.ToInt32(dr["HoanThanh"]),
                    Convert.ToInt32(dr["ChuaHoanThanh"]),
                    Convert.ToBoolean(dr["DaHoanTat"])
                ));
            }
            return list;
        }
    }
}
