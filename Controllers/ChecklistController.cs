using Microsoft.AspNetCore.Mvc;
using BaiMoiiii.BUS;
using BaiMoiiii.Models;

namespace BaiMoiiii.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChecklistController : ControllerBase
    {
        private readonly ChecklistBUS _bus;

        public ChecklistController(IConfiguration config)
        {
            _bus = new ChecklistBUS(config);
        }

        // ================== GET ALL ==================
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var list = _bus.GetAll();
                if (list == null || list.Count == 0)
                    return NotFound(new { message = "Không có checklist nào." });
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
