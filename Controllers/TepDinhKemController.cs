using Microsoft.AspNetCore.Mvc;
using BaiMoiiii.BUS;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TepDinhKemController : ControllerBase
    {
        private readonly TepDinhKemBUS _bus;

        public TepDinhKemController(TepDinhKemBUS bus)
        {
            _bus = bus;
        }

        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            var list = _bus.GetAll();
            if (!list.Any())
                return NotFound(new { message = "Không có tệp nào trong hệ thống!" });
            return Ok(list);
        }

        [HttpGet("get/{id}")]
        public IActionResult GetById(int id)
        {
            var t = _bus.GetById(id);
            if (t == null)
                return NotFound(new { message = "Không tìm thấy tệp!" });
            return Ok(t);
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] TepDinhKem model)
        {
            try
            {
                if (_bus.Add(model))
                    return Ok(new { message = "Thêm tệp đính kèm thành công!" });
                return BadRequest(new { message = "Không thể thêm tệp!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("update/{id}")]
        public IActionResult Update(int id, [FromBody] TepDinhKem model)
        {
            try
            {
                model.MaTep = id;
                if (_bus.Update(model))
                    return Ok(new { message = "Cập nhật tệp đính kèm thành công!" });
                return NotFound(new { message = "Không tìm thấy tệp để cập nhật!" });
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
                    return Ok(new { message = "Xóa tệp đính kèm thành công!" });
                return NotFound(new { message = "Không tìm thấy tệp để xóa!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
