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


        [HttpGet("all")]
        public IActionResult GetAllChecklists()
        {
            var data = _bus.GetAll();
            return data.Any() ? Ok(data) : NotFound(new { message = "Không tìm thấy checklist nào!" });
        }


        [HttpGet("summary")]
        public IActionResult GetChecklistSummary()
        {
            return Ok(_bus.GetSummary());
        }
    }
}
