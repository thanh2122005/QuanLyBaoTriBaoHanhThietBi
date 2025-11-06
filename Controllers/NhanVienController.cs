using Microsoft.AspNetCore.Mvc;
using BaiMoiiii.BUS;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NhanVienController : ControllerBase
    {
        private readonly NhanVienBUS _bus;

        public NhanVienController(NhanVienBUS bus)
        {
            _bus = bus;
        }

        // ===================== GET ALL =====================
        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            try
            {
                var list = _bus.GetAll();
                if (list == null || !list.Any())
                    return NotFound(new { message = "Không có nhân viên nào trong hệ thống." });

                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi khi lấy danh sách nhân viên.", error = ex.Message });
            }
        }

        // ===================== GET BY ID =====================
        [HttpGet("get/{id:int}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var nv = _bus.GetById(id);
                if (nv == null)
                    return NotFound(new { message = $"Không tìm thấy nhân viên có ID = {id}" });
                return Ok(nv);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi khi lấy nhân viên theo ID.", error = ex.Message });
            }
        }


    }
}
    