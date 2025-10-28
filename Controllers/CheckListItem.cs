using Microsoft.AspNetCore.Mvc;
using BaiMoiiii.BUS;
using BaiMoiiii.Models;

namespace BaiMoiiii.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChecklistItemController : ControllerBase
    {
        private readonly ChecklistItemBUS _bus;

        public ChecklistItemController(IConfiguration config)
        {
            _bus = new ChecklistItemBUS(config);
        }

        // ===================== GET BY CHECKLIST =====================
        [HttpGet("get-by-checklist/{checklistId}")]
        public IActionResult GetByChecklist(int checklistId)
        {
            try
            {
                var list = _bus.GetByChecklist(checklistId);
                if (list == null || list.Count == 0)
                    return NotFound(new { message = "Không có mục nào trong checklist này." });

                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // ===================== ADD =====================
        [HttpPost]
        public IActionResult Add([FromBody] ChecklistItem item)
        {
            try
            {
                if (_bus.Add(item))
                    return Ok(new { message = "Thêm mục checklist thành công!" });
                return BadRequest(new { message = "Không thể thêm mục checklist." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // ===================== UPDATE =====================
        [HttpPut]
        public IActionResult Update([FromBody] ChecklistItem item)
        {
            if (item == null)
                return BadRequest(new { message = "Dữ liệu gửi lên không hợp lệ!" });

            try
            {
                bool updated = _bus.Update(item);
                return updated
                    ? Ok(new { message = "✅ Cập nhật mục checklist thành công!" })
                    : NotFound(new { message = "Không tìm thấy mục checklist để cập nhật." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi máy chủ!", error = ex.Message });
            }
        }


        // ===================== DELETE =====================
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (_bus.Delete(id))
                    return Ok(new { message = "Xóa mục checklist thành công!" });
                return BadRequest(new { message = "Không thể xóa mục checklist." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
