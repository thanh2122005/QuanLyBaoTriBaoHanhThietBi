using Microsoft.AspNetCore.Mvc;
using BaiMoiiii.BUS;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NhatKyHeThongController : ControllerBase
    {
        private readonly NhatKyHeThongBUS _bus;

        public NhatKyHeThongController(NhatKyHeThongBUS bus)
        {
            _bus = bus;
        }

        // ===================== GET ALL =====================
        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            var list = _bus.GetAll();
            if (!list.Any())
                return NotFound(new { message = "Không có bản ghi nào trong nhật ký hệ thống!" });
            return Ok(list);
        }

        // ===================== GET BY ID =====================
        [HttpGet("get/{id}")]
        public IActionResult GetById(long id)
        {
            var log = _bus.GetById(id);
            if (log == null)
                return NotFound(new { message = $"Không tìm thấy log có ID = {id}" });
            return Ok(log);
        }

        // ===================== CREATE =====================
        [HttpPost("create")]
        public IActionResult Create([FromBody] NhatKyHeThong model)
        {
            try
            {
                if (_bus.Add(model))
                    return Ok(new { message = "Thêm log hệ thống thành công!" });
                return BadRequest(new { message = "Thêm thất bại!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

    }
}
