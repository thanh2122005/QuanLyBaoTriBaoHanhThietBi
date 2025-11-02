using Microsoft.AspNetCore.Mvc;
using BaiMoiiii.BUS;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhieuSuCoController : ControllerBase
    {
        private readonly PhieuSuCoBUS _bus;

        public PhieuSuCoController(PhieuSuCoBUS bus)
        {
            _bus = bus;
        }

        [HttpGet("get-all")]
        public IActionResult GetAll() => Ok(_bus.GetAll());

        [HttpGet("get/{id}")]
        public IActionResult GetById(int id)
        {
            var obj = _bus.GetById(id);
            if (obj == null)
                return NotFound(new { message = "Không tìm thấy phiếu sự cố!" });
            return Ok(obj);
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] PhieuSuCo model)
        {
            try
            {
                if (_bus.Add(model))
                    return Ok(new { message = "Thêm phiếu sự cố thành công!" });
                return BadRequest(new { message = "Không thể thêm phiếu sự cố!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("update/{id}")]
        public IActionResult Update(int id, [FromBody] PhieuSuCo model)
        {
            try
            {
                model.MaSuCo = id;
                if (_bus.Update(model))
                    return Ok(new { message = "Cập nhật phiếu sự cố thành công!" });
                return NotFound(new { message = "Không tìm thấy phiếu sự cố để cập nhật!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (_bus.Delete(id))
                    return Ok(new { message = "Xóa phiếu sự cố thành công!" });
                return NotFound(new { message = "Không tìm thấy phiếu sự cố để xóa!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("by-tai-san/{maTaiSan}")]
        public IActionResult GetByTaiSan(int maTaiSan)
        {
            var list = _bus.GetByTaiSan(maTaiSan);
            if (!list.Any())
                return NotFound(new { message = $"Không có phiếu sự cố cho tài sản #{maTaiSan}!" });
            return Ok(list);
        }

        [HttpGet("kpi/summary")]
        public IActionResult GetSummaryByStatus() => Ok(_bus.GetSummaryByStatus());
    }
}
