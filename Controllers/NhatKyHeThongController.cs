using Microsoft.AspNetCore.Mvc;
using BaiMoiiii.BUS;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NhatKyHeThongController : ControllerBase
    {
        private readonly NhatKyHeThongBUS _bus;

        public NhatKyHeThongController(NhatKyHeThongBUS bus)
        {
            _bus = bus;
        }

        // ==================== GET ALL ====================
        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            try
            {
                var list = _bus.GetAll();
                if (!list.Any())
                    return NotFound(new { message = "Không có nhật ký nào!" });

                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // ==================== PAGING ====================
        [HttpGet("paging")]
        public IActionResult Paging(int page = 1, int pageSize = 20)
        {
            try
            {
                var data = _bus.GetPaged(page, pageSize);
                var total = _bus.CountAll();

                return Ok(new
                {
                    data,
                    totalItems = total,
                    totalPages = (int)Math.Ceiling((double)total / pageSize)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // ==================== GET BY ID ====================
        [HttpGet("get/{id}")]
        public IActionResult GetById(long id)
        {
            try
            {
                var list = _bus.GetAll();
                var item = list.FirstOrDefault(x => x.MaLog == id);

                if (item == null)
                    return NotFound(new { message = "Không tìm thấy nhật ký!" });

                return Ok(item);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // ==================== CREATE ====================
        [HttpPost("create")]
        public IActionResult Create([FromBody] NhatKyHeThong model)
        {
            try
            {
                if (model == null)
                    return BadRequest(new { message = "Dữ liệu không hợp lệ!" });

                if (_bus.AddLog(model))
                    return Ok(new { message = "Thêm nhật ký thành công!" });

                return BadRequest(new { message = "Không thể thêm nhật ký!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // ==================== DELETE ====================
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(long id)
        {
            try
            {
                var success = _bus.Delete(id);

                if (success)
                    return Ok(new { message = "Xóa nhật ký thành công!" });

                return NotFound(new { message = "Không tìm thấy nhật ký để xóa!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
