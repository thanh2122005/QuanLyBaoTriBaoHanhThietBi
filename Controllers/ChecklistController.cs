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

        // ================== CREATE ==================
        [HttpPost]
        public IActionResult Add([FromBody] Checklist c)
        {
            try
            {
                if (_bus.Add(c))
                    return Ok(new { message = "Thêm checklist thành công!" });
                return BadRequest(new { message = "Không thể thêm checklist." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // ================== UPDATE ==================
        [HttpPut]
        public IActionResult Update([FromBody] Checklist c)
        {
            try
            {
                if (_bus.Update(c))
                    return Ok(new { message = "Cập nhật checklist thành công!" });
                return BadRequest(new { message = "Không thể cập nhật checklist." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // ================== DELETE ==================
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (_bus.Delete(id))
                    return Ok(new { message = "Xóa checklist thành công!" });
                return BadRequest(new { message = "Không thể xóa checklist." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
