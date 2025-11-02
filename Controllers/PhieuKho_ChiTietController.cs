using Microsoft.AspNetCore.Mvc;
using BaiMoiiii.BUS;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhieuKho_ChiTietController : ControllerBase
    {
        private readonly PhieuKho_ChiTietBUS _bus;

        public PhieuKho_ChiTietController(PhieuKho_ChiTietBUS bus)
        {
            _bus = bus;
        }

        [HttpGet("get-all")]
        public IActionResult GetAll() => Ok(_bus.GetAll());

        [HttpGet("get/{id}")]
        public IActionResult GetById(int id)
        {
            var item = _bus.GetById(id);
            if (item == null)
                return NotFound(new { message = "Không tìm thấy chi tiết phiếu kho!" });
            return Ok(item);
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] PhieuKho_ChiTiet model)
        {
            try
            {
                if (_bus.Add(model))
                    return Ok(new { message = "Thêm chi tiết phiếu kho thành công!" });
                return BadRequest(new { message = "Không thể thêm chi tiết phiếu kho!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("update/{id}")]
        public IActionResult Update(int id, [FromBody] PhieuKho_ChiTiet model)
        {
            try
            {
                model.MaCT = id;
                if (_bus.Update(model))
                    return Ok(new { message = "Cập nhật chi tiết phiếu kho thành công!" });
                return NotFound(new { message = "Không tìm thấy chi tiết phiếu kho cần cập nhật!" });
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
                    return Ok(new { message = "Xóa chi tiết phiếu kho thành công!" });
                return NotFound(new { message = "Không tìm thấy chi tiết phiếu kho để xóa!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("by-phieu/{phieuId}")]
        public IActionResult GetByPhieu(int phieuId)
        {
            var list = _bus.GetByPhieu(phieuId);
            if (!list.Any())
                return NotFound(new { message = $"Không có chi tiết nào thuộc phiếu kho #{phieuId}!" });
            return Ok(list);
        }
    }
}
