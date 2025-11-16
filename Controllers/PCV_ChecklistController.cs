using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BaiMoiiii.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PCV_ChecklistController : ControllerBase
    {
        private readonly IConfiguration _config;

        public PCV_ChecklistController(IConfiguration config)
        {
            _config = config;
        }

        // 🟢 API: Tiến độ của 1 phiếu công việc
        [HttpGet("get-progress/{maPCV}")]
        public IActionResult GetProgress(int maPCV)
        {
            string connectionString = _config.GetConnectionString("DefaultConnection");

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"
                    SELECT MaPhieuCV, TrangThai
                    FROM PhieuCongViec
                    WHERE MaPhieuCV = @maPCV";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@maPCV", maPCV);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (!reader.Read())
                            return NotFound(new { message = $"Không tìm thấy PCV có mã {maPCV}" });

                        string trangThai = reader["TrangThai"].ToString() ?? "";
                        double tiendo = CalcProgress(trangThai);

                        return Ok(new
                        {
                            MaPhieuCV = maPCV,
                            TrangThai = trangThai,
                            TienDo = tiendo
                        });
                    }
                }
            }
        }


        // 🟢 API: Lấy tiến độ TẤT CẢ phiếu công việc
        [HttpGet("get-all-progress")]
        public IActionResult GetAllProgress()
        {
            string connStr = _config.GetConnectionString("DefaultConnection");

            List<object> result = new();

            using (SqlConnection conn = new(connStr))
            {
                conn.Open();
                string sql = @"
                    SELECT 
                        MaPhieuCV, 
                        TenCongViec, 
                        NhanVienThucHien AS NhanVien,
                        TrangThai
                    FROM PhieuCongViec
                    ORDER BY MaPhieuCV DESC";

                using (SqlCommand cmd = new(sql, conn))
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string trangThai = dr["TrangThai"].ToString() ?? "";
                        double tiendo = CalcProgress(trangThai);

                        result.Add(new
                        {
                            MaPhieuCV = Convert.ToInt32(dr["MaPhieuCV"]),
                            TenCongViec = dr["TenCongViec"]?.ToString(),
                            NhanVien = dr["NhanVien"]?.ToString(),
                            TrangThai = trangThai,
                            TienDo = tiendo
                        });
                    }
                }
            }

            return Ok(result);
        }


        // 🔧 Hàm tính % tiến độ
        private double CalcProgress(string trangThai)
        {
            return trangThai switch
            {
                "Mới" => 0,
                "Đang xử lý" => 50,
                "Hoàn thành" => 100,
                _ => 0
            };
        }
    }
}
