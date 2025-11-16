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
        private readonly LogHelper _logger;
        private readonly string _username;

        public PhieuKho_ChiTietController(PhieuKho_ChiTietBUS bus, LogHelper logger)
        {
            _bus = bus;
            _logger = logger;

            // sau này thay bằng token/JWT
            _username = "Admin";
        }

        // ====================== GET ALL ======================
        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            var list = _bus.GetAll();
            return Ok(list);
        }

        // ====================== GET BY ID ======================
        [HttpGet("get/{id}")]
        public IActionResult GetById(int id)
        {
            var item = _bus.GetById(id);

            if (item == null)
                return NotFound(new { message = "Không tìm thấy chi tiết phiếu kho!" });

            return Ok(item);
        }

        // ====================== CREATE ======================
        [HttpPost("create")]
        public IActionResult Create([FromBody] PhieuKho_ChiTiet model)
        {
            try
            {
                if (_bus.Add(model))
                {
                    _logger.WriteLog("PhieuKho_ChiTiet", model.MaCT, "Thêm", null, model, _username);
                    return Ok(new { message = "Thêm chi tiết phiếu kho thành công!" });
                }

                return BadRequest(new { message = "Không thể thêm chi tiết phiếu kho!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // ====================== UPDATE ======================
        [HttpPut("update/{id}")]
        public IActionResult Update(int id, [FromBody] PhieuKho_ChiTiet model)
        {
            try
            {
                var oldData = _bus.GetById(id);
                if (oldData == null)
                    return NotFound(new { message = "Không tìm thấy chi tiết phiếu kho cần cập nhật!" });

                model.MaCT = id;

                if (_bus.Update(model))
                {
                    _logger.WriteLog("PhieuKho_ChiTiet", id, "Sửa", oldData, model, _username);
                    return Ok(new { message = "Cập nhật chi tiết phiếu kho thành công!" });
                }

                return BadRequest(new { message = "Không thể cập nhật chi tiết phiếu kho!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // ====================== DELETE ======================
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var oldData = _bus.GetById(id);
                if (oldData == null)
                    return NotFound(new { message = "Không tìm thấy chi tiết phiếu kho để xóa!" });

                if (_bus.Delete(id))
                {
                    _logger.WriteLog("PhieuKho_ChiTiet", id, "Xóa", oldData, null, _username);
                    return Ok(new { message = "Xóa chi tiết phiếu kho thành công!" });
                }

                return BadRequest(new { message = "Không thể xóa chi tiết phiếu kho!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // ====================== GET BY PHIEU ======================
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
