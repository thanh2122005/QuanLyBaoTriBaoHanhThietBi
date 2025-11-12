using Microsoft.AspNetCore.Mvc;
using BaiMoiiii.BUS;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaiSanController : ControllerBase
    {
        private readonly TaiSanBUS _bus;

        public TaiSanController(TaiSanBUS bus)
        {
            _bus = bus;
        }

        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            var list = _bus.GetAll();
            if (!list.Any())
                return NotFound(new { message = "Không có tài sản nào trong hệ thống!" });
            return Ok(list);
        }

        [HttpGet("get/{id}")]
        public IActionResult GetById(int id)
        {
            var ts = _bus.GetById(id);
            if (ts == null)
                return NotFound(new { message = "Không tìm thấy tài sản!" });
            return Ok(ts);
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] TaiSan model)
        {
            try
            {
                if (_bus.Add(model))
                    return Ok(new { message = "Thêm tài sản thành công!" });
                return BadRequest(new { message = "Không thể thêm tài sản!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("update/{id}")]
        public IActionResult Update(int id, [FromBody] TaiSan model)
        {
            try
            {
                model.MaTaiSan = id;
                if (_bus.Update(model))
                    return Ok(new { message = "Cập nhật tài sản thành công!" });
                return NotFound(new { message = "Không tìm thấy tài sản cần cập nhật!" });
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
                    return Ok(new { message = "Xóa tài sản thành công!" });
                return NotFound(new { message = "Không tìm thấy tài sản để xóa!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("search")]
        public IActionResult Search([FromQuery] string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return BadRequest(new { message = "Từ khóa tìm kiếm không được để trống!" });

            var list = _bus.Search(keyword);
            if (!list.Any())
                return NotFound(new { message = "Không tìm thấy tài sản phù hợp!" });
            return Ok(list);
        }
    }
}
