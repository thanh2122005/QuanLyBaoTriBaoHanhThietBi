using Microsoft.AspNetCore.Mvc;
using BaiMoiiii.BUS;
using BaiMoiiii.MODEL;
using System;

namespace BaiMoiiii.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhieuCongViecController : ControllerBase
    {
        private readonly PhieuCongViecBUS _bus;

        public PhieuCongViecController(PhieuCongViecBUS bus)
        {
            _bus = bus;
        }

        // ===================== GET ALL =====================
        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            var list = _bus.GetAll();
            return Ok(new
            {
                message = $"Đã tải {list.Count} phiếu công việc thành công!",
                data = list
            });
        }

        // ===================== GET BY ID =====================
        [HttpGet("get/{id}")]
        public IActionResult GetById(int id)
        {
            var obj = _bus.GetById(id);
            if (obj == null)
                return NotFound(new { message = $"Không tìm thấy phiếu công việc có ID = {id}" });
            return Ok(new { message = "Tải thành công!", data = obj });
        }

        // ===================== CREATE =====================
        [HttpPost("create")]
        public IActionResult Create([FromBody] PhieuCongViec model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Dữ liệu không hợp lệ!" });

            try
            {
                bool result = _bus.Add(model);
                if (result)
                    return Ok(new { message = "Thêm phiếu công việc thành công!" });
                return BadRequest(new { message = "Không thể thêm phiếu công việc!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // ===================== UPDATE =====================
        [HttpPut("update/{id}")]
        public IActionResult Update(int id, [FromBody] PhieuCongViec model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Dữ liệu không hợp lệ!" });

            try
            {
                model.MaPhieuCV = id;
                bool result = _bus.Update(model);
                if (result)
                    return Ok(new { message = "Cập nhật phiếu công việc thành công!" });
                return NotFound(new { message = $"Không tìm thấy phiếu công việc có ID = {id}" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // ===================== DELETE =====================
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                bool result = _bus.Delete(id);
                if (result)
                    return Ok(new { message = "Xóa phiếu công việc thành công!" });
                return NotFound(new { message = $"Không tìm thấy phiếu công việc có ID = {id}" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
