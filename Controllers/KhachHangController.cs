using Microsoft.AspNetCore.Mvc;
using BaiMoiiii.BUS;
using BaiMoiiii.Models;

<<<<<<< HEAD
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


        // ===================== GET BY ID ====================
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


        // ===================== ADD ====================
        [HttpPost("create")]
        public IActionResult Add([FromBody] KhachHang kh)
=======
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
>>>>>>> origin/Dung
        {
            try
            {
                if (_bus.Add(kh))
                    return Ok(new { message = "Thêm khách hàng thành công!" });
<<<<<<< HEAD
                return BadRequest(new { message = "Không thể thêm khách hàng." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // ===================== UPDATE ====================
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

        // ===================== DELETE ====================
=======
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

>>>>>>> origin/Dung
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (_bus.Delete(id))
                    return Ok(new { message = "Xóa khách hàng thành công!" });
<<<<<<< HEAD
                return BadRequest(new { message = "Không thể xóa khách hàng." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

=======
                return NotFound(new { message = "Không tìm thấy khách hàng để xóa!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
>>>>>>> origin/Dung
    }
}
