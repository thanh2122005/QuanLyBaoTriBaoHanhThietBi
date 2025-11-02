using Microsoft.AspNetCore.Mvc;
using BaiMoiiii.BUS;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuyenController : ControllerBase
    {
        private readonly QuyenBUS _bus;

        public QuyenController(QuyenBUS bus)
        {
            _bus = bus;
        }

        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            var list = _bus.GetAll();
            if (!list.Any())
                return NotFound(new { message = "Không có quyền nào trong hệ thống!" });
            return Ok(list);
        }

        [HttpGet("get/{id}")]
        public IActionResult GetById(int id)
        {
            var q = _bus.GetById(id);
            if (q == null)
                return NotFound(new { message = "Không tìm thấy quyền!" });
            return Ok(q);
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] Quyen model)
        {
            try
            {
                if (_bus.Add(model))
                    return Ok(new { message = "Thêm quyền thành công!" });
                return BadRequest(new { message = "Không thể thêm quyền!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("update/{id}")]
        public IActionResult Update(int id, [FromBody] Quyen model)
        {
            try
            {
                model.QuyenID = id;
                if (_bus.Update(model))
                    return Ok(new { message = "Cập nhật quyền thành công!" });
                return NotFound(new { message = "Không tìm thấy quyền để cập nhật!" });
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
                    return Ok(new { message = "Xóa quyền thành công!" });
                return NotFound(new { message = "Không tìm thấy quyền để xóa!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("get-by-group/{nhom}")]
        public IActionResult GetByGroup(string nhom)
        {
            var list = _bus.GetByGroup(nhom);
            if (!list.Any())
                return NotFound(new { message = $"Không có quyền nào thuộc nhóm '{nhom}'!" });
            return Ok(list);
        }

        [HttpGet("search")]
        public IActionResult Search([FromQuery] string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return BadRequest(new { message = "Từ khóa tìm kiếm không được để trống!" });

            var list = _bus.Search(keyword);
            if (!list.Any())
                return NotFound(new { message = "Không tìm thấy quyền phù hợp!" });
            return Ok(list);
        }
    }
}
