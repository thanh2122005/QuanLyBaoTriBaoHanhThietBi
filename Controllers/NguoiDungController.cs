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

    }
}
