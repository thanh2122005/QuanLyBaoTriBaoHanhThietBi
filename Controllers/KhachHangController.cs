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
        private readonly LogHelper _logger;
        private readonly string _username;

        public KhachHangController(KhachHangBUS bus, LogHelper logger)
        {
            _bus = bus;
            _logger = logger;

            // sau này thay bằng JWT
            _username = "Admin";
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
                {
                    _logger.WriteLog("KhachHang", kh.MaKH, "Thêm", null, kh, _username);
                    return Ok(new { message = "Thêm khách hàng thành công!" });
                }

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
                var oldData = _bus.GetById(id);
                if (oldData == null)
                    return NotFound(new { message = "Không tìm thấy khách hàng để cập nhật!" });

                kh.MaKH = id;

                if (_bus.Update(kh))
                {
                    _logger.WriteLog("KhachHang", id, "Sửa", oldData, kh, _username);
                    return Ok(new { message = "Cập nhật thành công!" });
                }

                return BadRequest(new { message = "Không thể cập nhật khách hàng!" });
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
                var oldData = _bus.GetById(id);
                if (oldData == null)
                    return NotFound(new { message = "Không tìm thấy khách hàng để xóa!" });

                if (_bus.Delete(id))
                {
                    _logger.WriteLog("KhachHang", id, "Xóa", oldData, null, _username);
                    return Ok(new { message = "Xóa khách hàng thành công!" });
                }

                return BadRequest(new { message = "Không thể xóa khách hàng!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
