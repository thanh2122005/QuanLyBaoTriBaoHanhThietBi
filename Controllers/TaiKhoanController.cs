using Microsoft.AspNetCore.Mvc;
using BaiMoiiii.BUS;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaiKhoanController : ControllerBase
    {
        private readonly TaiKhoanBUS _bus;

        public TaiKhoanController(TaiKhoanBUS bus)
        {
            _bus = bus;
        }

        // ===== Lấy tất cả =====
        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            var list = _bus.GetAll();
            if (list == null || !list.Any())
                return NotFound(new { message = "Không có dữ liệu." });
            return Ok(list);
        }

        // ===== Lấy theo ID =====
        [HttpGet("get/{id}")]
        public IActionResult GetById(int id)
        {
            var item = _bus.GetById(id);
            if (item == null)
                return NotFound(new { message = "Không tìm thấy tài khoản." });
            return Ok(item);
        }

        // ===== Thêm mới =====
        [HttpPost]
        public IActionResult Create([FromBody] TaiKhoan model)
        {
            if (model == null)
                return BadRequest(new { message = "Dữ liệu không hợp lệ." });

            var result = _bus.Add(model);
            if (result)
                return Ok(new { message = "Thêm tài khoản thành công!" });

            return BadRequest(new { message = "Thêm thất bại!" });
        }

        // ===== Cập nhật =====
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] TaiKhoan model)
        {
            if (model == null)
                return BadRequest(new { message = "Dữ liệu không hợp lệ." });

            model.MaTaiKhoan = id;
            var result = _bus.Update(model);
            if (result)
                return Ok(new { message = "Cập nhật tài khoản thành công!" });

            return BadRequest(new { message = "Cập nhật thất bại!" });
        }

        // ===== Xóa =====
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _bus.Delete(id);
            if (result)
                return Ok(new { message = "Xóa tài khoản thành công!" });

            return BadRequest(new { message = "Xóa thất bại!" });
        }
    }
}
