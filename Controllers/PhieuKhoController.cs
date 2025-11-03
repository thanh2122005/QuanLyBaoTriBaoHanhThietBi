using Microsoft.AspNetCore.Mvc;
using BaiMoiiii.BUS;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhieuKhoController : ControllerBase
    {
        private readonly PhieuKhoBUS _bus;

        public PhieuKhoController(PhieuKhoBUS bus)
        {
            _bus = bus;
        }

        // ========== GET ALL ==========
        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            var list = _bus.GetAll();
            if (list == null || !list.Any())
                return NotFound(new { message = "Không có dữ liệu." });
            return Ok(list);
        }

        // ========== GET BY ID ==========
        [HttpGet("get/{id}")]
        public IActionResult GetById(int id)
        {
            var item = _bus.GetById(id);
            if (item == null)
                return NotFound(new { message = "Không tìm thấy phiếu kho." });
            return Ok(item);
        }

        // ========== CREATE ==========
        [HttpPost]
        public IActionResult Create([FromBody] PhieuKho model)
        {
            if (model == null)
                return BadRequest(new { message = "Dữ liệu không hợp lệ." });

            var result = _bus.Add(model);
            if (result)
                return Ok(new { message = "Thêm phiếu kho thành công!" });

            return BadRequest(new { message = "Thêm phiếu kho thất bại!" });
        }

        // ========== UPDATE ==========
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] PhieuKho model)
        {
            if (model == null)
                return BadRequest(new { message = "Dữ liệu không hợp lệ." });

            model.MaPhieuKho = id;
            var result = _bus.Update(model);
            if (result)
                return Ok(new { message = "Cập nhật phiếu kho thành công!" });

            return BadRequest(new { message = "Cập nhật phiếu kho thất bại!" });
        }

        // ========== DELETE ==========
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _bus.Delete(id);
            if (result)
                return Ok(new { message = "Xóa phiếu kho thành công!" });

            return BadRequest(new { message = "Xóa phiếu kho thất bại!" });
        }
    }
}
