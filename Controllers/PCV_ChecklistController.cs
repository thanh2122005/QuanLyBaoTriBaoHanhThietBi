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

        // 🟢 API: Lấy tiến độ theo trạng thái Phiếu công việc
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
                        if (reader.Read())
                        {
                            string trangThai = reader["TrangThai"].ToString() ?? "";

                            double tiendo = 0;
                            if (trangThai == "Mới") tiendo = 0;
                            else if (trangThai == "Đang xử lý") tiendo = 50;
                            else if (trangThai == "Hoàn thành") tiendo = 100;

                            return Ok(new
                            {
                                MaPhieuCV = maPCV,
                                TrangThai = trangThai,
                                TienDo = tiendo
                            });
                        }
                        else
                        {
                            return NotFound(new { message = $"Không tìm thấy phiếu công việc có mã {maPCV}" });
                        }
                    }
                }
            }
        }
    }
}
