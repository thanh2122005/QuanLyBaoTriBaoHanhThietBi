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


    }
}
