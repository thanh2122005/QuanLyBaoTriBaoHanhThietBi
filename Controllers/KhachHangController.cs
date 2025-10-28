using Microsoft.AspNetCore.Mvc;
using BaiMoiiii.BUS;
using BaiMoiiii.Models;

namespace BaiMoiiii.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KhachHangController : ControllerBase
    {
        private readonly KhachHangBus _bus;

        public KhachHangController(IConfiguration config)
        {
            _bus = new KhachHangBus(config);
        }

        // ===================== GET ALL =====================
        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            try
            {
                var list = _bus.GetAll();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // ===================== GET BY ID =====================
        [HttpGet("get/{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var kh = _bus.GetById(id);
                if (kh == null)
                    return NotFound(new { message = "Không tìm thấy khách hàng." });
                return Ok(kh);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        // ===================== ADD =====================
        [HttpPost("create")]
        public IActionResult Add([FromBody] KhachHang kh)
        {
            try
            {
                if (_bus.Add(kh))
                    return Ok(new { message = "Thêm khách hàng thành công!" });
                return BadRequest(new { message = "Không thể thêm khách hàng." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // ===================== UPDATE =====================
        [HttpPut("update")]
        public IActionResult Update([FromBody] KhachHang kh)
        {
            try
            {
                if (_bus.Update(kh))
                    return Ok(new { message = "Cập nhật khách hàng thành công!" });
                return BadRequest(new { message = "Không thể cập nhật khách hàng." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }



    }
}
