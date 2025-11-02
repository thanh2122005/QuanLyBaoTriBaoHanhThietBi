using Microsoft.AspNetCore.Mvc;
using BaiMoiiii.BUS;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhieuCongViecController : ControllerBase
    {
        private readonly PhieuCongViecBUS _bus;

        public PhieuCongViecController(PhieuCongViecBUS bus)
        {
            _bus = bus;
        }

        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            var list = _bus.GetAll();
            return Ok(new { message = $"Đã tải {list.Count} phiếu công việc thành công!", data = list });
        }

        [HttpGet("get/{id}")]
        public IActionResult GetById(int id)
        {
            var obj = _bus.GetById(id);
            if (obj == null)
                return NotFound(new { message = "Không tìm thấy phiếu công việc!" });
            return Ok(obj);
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] PhieuCongViec model)
        {
            try
            {
                if (_bus.Add(model))
                    return Ok(new { message = "Thêm phiếu công việc thành công!" });
                return BadRequest(new { message = "Thêm thất bại!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("update/{id}")]
        public IActionResult Update(int id, [FromBody] PhieuCongViec model)
        {
            try
            {
                model.MaPhieuCV = id;
                if (_bus.Update(model))
                    return Ok(new { message = "Cập nhật phiếu công việc thành công!" });
                return NotFound(new { message = "Không tìm thấy phiếu công việc để cập nhật!" });
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
                    return Ok(new { message = "Xóa phiếu công việc thành công!" });
                return NotFound(new { message = "Không tìm thấy phiếu công việc để xóa!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
