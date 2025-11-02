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
            try
            {
                var list = _bus.GetAll();
                if (list == null || !list.Any())
                    return NotFound(new { message = "Không có dữ liệu linh kiện." });

                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi khi lấy danh sách linh kiện.", error = ex.Message });
            }
        }

        // ===================== GET BY ID =====================
        [HttpGet("get/{id:int}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var lk = _bus.GetById(id);
                if (lk == null)
                    return NotFound(new { message = $"Không tìm thấy linh kiện có ID = {id}" });

                return Ok(lk);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi khi lấy linh kiện theo ID.", error = ex.Message });
            }
        }

        // ===================== CREATE =====================
        [HttpPost("create")]
        public IActionResult Create([FromBody] LinhKien model)
        {
            if (model == null)
                return BadRequest(new { message = "Dữ liệu linh kiện không hợp lệ." });

            try
            {
                var result = _bus.Add(model);
                if (result)
                    return CreatedAtAction(nameof(GetById), new { id = model.MaLinhKien },
                        new { message = "Thêm linh kiện thành công!", data = model });

                return BadRequest(new { message = "Không thể thêm linh kiện." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi khi thêm linh kiện.", error = ex.Message });
            }
        }

        // ===================== UPDATE =====================
        [HttpPut("update/{id:int}")]
        public IActionResult Update(int id, [FromBody] LinhKien model)
        {
            if (model == null)
                return BadRequest(new { message = "Dữ liệu linh kiện không hợp lệ." });

            try
            {
                model.MaLinhKien = id;
                var result = _bus.Update(model);

                if (result)
                    return Ok(new { message = "Cập nhật linh kiện thành công!" });

                return NotFound(new { message = $"Không tìm thấy linh kiện có ID = {id}" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi khi cập nhật linh kiện.", error = ex.Message });
            }
        }

        // ===================== DELETE =====================
        [HttpDelete("delete/{id:int}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var result = _bus.Delete(id);

                if (result)
                    return Ok(new { message = "Xóa linh kiện thành công!" });

                return NotFound(new { message = $"Không tìm thấy linh kiện có ID = {id}" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi khi xóa linh kiện.", error = ex.Message });
            }
        }
    }
}
