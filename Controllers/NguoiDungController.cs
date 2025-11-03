using Microsoft.AspNetCore.Mvc;
using BaiMoiiii.BUS;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NguoiDungController : ControllerBase
    {
        private readonly NguoiDungBUS _bus;

        public NguoiDungController(NguoiDungBUS bus)
        {
            _bus = bus;
        }

        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            var list = _bus.GetAll();
            if (!list.Any())
                return NotFound(new { message = "Không có dữ liệu." });
            return Ok(list);
        }

        [HttpGet("get/{id}")]
        public IActionResult GetById(int id)
        {
            var user = _bus.GetById(id);
            if (user == null)
                return NotFound(new { message = $"Không tìm thấy người dùng có ID = {id}" });
            return Ok(user);
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] NguoiDung model)
        {
            try
            {
                if (_bus.Add(model))
                    return Ok(new { message = "Thêm người dùng thành công!" });
                return BadRequest(new { message = "Thêm thất bại!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

    }
}
