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

        // ===================== CREATE =====================
        [HttpPost("create")]
        public IActionResult Create([FromBody] LinhKien model)
        {
            try
            {
                if (_bus.Add(model))
                    return Ok(new { message = "Thêm linh kiện thành công!" });
                return BadRequest(new { message = "Thêm thất bại!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // ===================== UPDATE =====================
        [HttpPut("update/{id}")]
        public IActionResult Update(int id, [FromBody] LinhKien model)
        {
            try
            {
                model.MaLinhKien = id;
                if (_bus.Update(model))
                    return Ok(new { message = "Cập nhật thành công!" });
                return NotFound(new { message = "Không tìm thấy linh kiện cần cập nhật." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // ===================== DELETE =====================
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (_bus.Delete(id))
                    return Ok(new { message = "Xóa linh kiện thành công!" });
                return NotFound(new { message = "Không tìm thấy linh kiện để xóa." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
