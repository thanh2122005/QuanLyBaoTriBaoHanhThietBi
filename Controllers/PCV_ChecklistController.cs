using Microsoft.AspNetCore.Mvc;
using BaiMoiiii.BUS;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PCV_ChecklistController : ControllerBase
    {
        private readonly PCV_ChecklistBUS _bus;

        public PCV_ChecklistController(PCV_ChecklistBUS bus)
        {
            _bus = bus;
        }

        // ===================== GET ALL =====================
        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            var list = _bus.GetAll();
            if (!list.Any())
                return NotFound(new { message = "Không có dữ liệu checklist nào!" });
            return Ok(list);
        }

        // ===================== GET SUMMARY =====================
        [HttpGet("summary")]
        public IActionResult GetSummary()
        {
            var result = _bus.GetSummary();
            return Ok(result);
        }
    }
}
