using Microsoft.AspNetCore.Mvc;
using BaiMoiiii.BUS;
using BaiMoiiii.Models;

namespace BaiMoiiii.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhachHangController : ControllerBase
    {
        private readonly KhachHangBUS _bus;

        public KhachHangController(KhachHangBUS bus)
        {
            _bus = bus;
        }

        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            var data = _bus.GetAll();
            if (!data.Any()) return NotFound(new { message = "Không có khách hàng nào!" });
            return Ok(data);
        }

        [HttpGet("get/{id}")]
        public IActionResult GetById(int id)
        {
            var item = _bus.GetById(id);
            if (item == null) return NotFound(new { message = "Không tìm thấy khách hàng!" });
            return Ok(item);
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] KhachHang kh)
        {
            try
            {
                if (_bus.Add(kh))
                    return Ok(new { message = "Thêm khách hàng thành công!" });
                return BadRequest(new { message = "Không thể thêm khách hàng!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("update/{id}")]
        public IActionResult Update(int id, [FromBody] KhachHang kh)
        {
            try
            {
                kh.MaKH = id;
                if (_bus.Update(kh))
                    return Ok(new { message = "Cập nhật thành công!" });
                return NotFound(new { message = "Không tìm thấy khách hàng để cập nhật!" });
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
                    return Ok(new { message = "Xóa khách hàng thành công!" });
                return NotFound(new { message = "Không tìm thấy khách hàng để xóa!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
