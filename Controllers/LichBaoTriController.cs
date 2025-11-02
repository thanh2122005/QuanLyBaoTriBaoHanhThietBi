using Microsoft.AspNetCore.Mvc;
using BaiMoiiii.BUS;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LichBaoTriController : ControllerBase
    {
        private readonly LichBaoTriBUS _bus;

        public LichBaoTriController(LichBaoTriBUS bus)
        {
            _bus = bus;
        }

        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            var list = _bus.GetAll();
            if (list == null || !list.Any())
                return NotFound(new { message = "Không có dữ liệu." });
            return Ok(list);
        }

        [HttpGet("get/{id}")]
        public IActionResult GetById(int id)
        {
            var item = _bus.GetById(id);
            if (item == null)
                return NotFound(new { message = "Không tìm thấy bản ghi." });
            return Ok(item);
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] LichBaoTri model)
        {
            try
            {
                if (_bus.Add(model))
                    return Ok(new { message = "Thêm lịch bảo trì thành công!" });
                return BadRequest(new { message = "Thêm thất bại." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("update/{id}")]
        public IActionResult Update(int id, [FromBody] LichBaoTri model)
        {
            try
            {
                model.MaLich = id;
                if (_bus.Update(model))
                    return Ok(new { message = "Cập nhật thành công!" });
                return NotFound(new { message = "Không tìm thấy bản ghi cần cập nhật." });
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
                    return Ok(new { message = "Xóa thành công!" });
                return NotFound(new { message = "Không tìm thấy bản ghi để xóa." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
