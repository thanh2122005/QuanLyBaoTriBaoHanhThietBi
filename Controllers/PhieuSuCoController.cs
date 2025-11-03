using Microsoft.AspNetCore.Mvc;
using BaiMoiiii.BUS;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhieuSuCoController : ControllerBase
    {
        private readonly PhieuSuCoBUS _bus;

        public PhieuSuCoController(PhieuSuCoBUS bus)
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
                return NotFound(new { message = "Không tìm thấy phiếu sự cố." });
            return Ok(item);
        }

        // ===== Thêm =====
        [HttpPost]
        public IActionResult Create([FromBody] PhieuSuCo model)
        {
            if (model == null)
                return BadRequest(new { message = "Dữ liệu không hợp lệ." });

            var result = _bus.Add(model);
            if (result)
                return Ok(new { message = "Thêm phiếu sự cố thành công!" });

            return BadRequest(new { message = "Thêm thất bại!" });
        }

        // ===== Cập nhật =====
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] PhieuSuCo model)
        {
            if (model == null)
                return BadRequest(new { message = "Dữ liệu không hợp lệ." });

            model.MaSuCo = id;
            var result = _bus.Update(model);
            if (result)
                return Ok(new { message = "Cập nhật phiếu sự cố thành công!" });

            return BadRequest(new { message = "Cập nhật thất bại!" });
        }

        // ===== Xóa =====
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _bus.Delete(id);
            if (result)
                return Ok(new { message = "Xóa phiếu sự cố thành công!" });

            return BadRequest(new { message = "Xóa thất bại!" });
        }
    }
}
