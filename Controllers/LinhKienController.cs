using Microsoft.AspNetCore.Mvc;
using BaiMoiiii.BUS;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinhKienController : ControllerBase
    {
        private readonly LinhKienBUS _bus;

        public LinhKienController(LinhKienBUS bus)
        {
            _bus = bus;
        }

        // ===================== GET ALL =====================
        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            var list = _bus.GetAll();
            if (list == null || !list.Any())
                return NotFound(new { message = "Không có dữ liệu." });
            return Ok(list);
        }

        // ===================== GET BY ID =====================
        [HttpGet("get/{id}")]
        public IActionResult GetById(int id)
        {
            var lk = _bus.GetById(id);
            if (lk == null)
                return NotFound(new { message = $"Không tìm thấy linh kiện có ID = {id}" });
            return Ok(lk);
        }


    }
}
