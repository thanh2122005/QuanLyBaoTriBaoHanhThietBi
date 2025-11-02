using Microsoft.AspNetCore.Mvc;
using BaiMoiiii.BUS;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhieuKhoController : ControllerBase
    {
        private readonly PhieuKhoBUS _bus;

        public PhieuKhoController(PhieuKhoBUS bus)
        {
            _bus = bus;
        }

        [HttpGet("get-all")]
        public IActionResult GetAll() => Ok(_bus.GetAll());

        [HttpGet("get/{id}")]
        public IActionResult GetById(int id)
        {
            var obj = _bus.GetById(id);
            if (obj == null)
                return NotFound(new { message = "Không tìm thấy phiếu kho!" });
            return Ok(obj);
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] PhieuKho model)
        {
            try
            {
                if (_bus.Add(model))
                    return Ok(new { message = "Thêm phiếu kho thành công!" });
                return BadRequest(new { message = "Không thể thêm phiếu kho!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("update/{id}")]
        public IActionResult Update(int id, [FromBody] PhieuKho model)
        {
            try
            {
                model.MaPhieuKho = id;
                if (_bus.Update(model))
                    return Ok(new { message = "Cập nhật phiếu kho thành công!" });
                return NotFound(new { message = "Không tìm thấy phiếu kho để cập nhật!" });
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
                    return Ok(new { message = "Xóa phiếu kho thành công!" });
                return NotFound(new { message = "Không tìm thấy phiếu kho để xóa!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("by-type/{loai}")]
        public IActionResult GetByType(string loai)
        {
            var list = _bus.GetByType(loai);
            if (!list.Any())
                return NotFound(new { message = $"Không có phiếu kho loại '{loai}'!" });
            return Ok(list);
        }

        [HttpGet("kpi/summary")]
        public IActionResult GetKpiSummary() => Ok(_bus.GetKpiSummary());

        [HttpGet("kpi/tong-gia-tri")]
        public IActionResult GetTotalValue() => Ok(_bus.GetTotalValue());
    }
}
