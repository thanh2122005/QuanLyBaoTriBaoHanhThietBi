using BaiMoiiii.BUS;
using BaiMoiiii.MODEL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BaiMoiiii.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhieuKhoController : ControllerBase
    {
        private readonly PhieuKhoBUS _bus;
        private readonly LogHelper _logger;
        private readonly string _username;

        // Inject BUS + LogHelper
        public PhieuKhoController(PhieuKhoBUS bus, LogHelper logger)
        {
            _bus = bus;
            _logger = logger;
            _username = "Admin"; // sau này lấy từ JWT
        }
        // ===================== GET ALL =====================
        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            var list = _bus.GetAll();
            if (list == null || !list.Any())
                return NotFound(new { message = "Không có dữ liệu." });

            return Ok(list);
        }

        // ===================== GET BY ID =====================
        [HttpGet("get/{id}")]
        public IActionResult GetById(int id)
        {
            var item = _bus.GetById(id);

            if (item == null)
                return NotFound(new { message = "Không tìm thấy phiếu kho." });

            return Ok(item);
        }

        // ===================== CREATE =====================
        [HttpPost("create")]
        public IActionResult Create([FromBody] PhieuKho model)
        {
            if (model == null)
                return BadRequest(new { message = "Dữ liệu không hợp lệ." });

            if (_bus.Add(model))
            {
                _logger.WriteLog("PhieuKho", model.MaPhieuKho, "Thêm", null, model, _username);
                return Ok(new { message = "Thêm phiếu kho thành công!" });
            }

            return BadRequest(new { message = "Thêm phiếu kho thất bại!" });
        }

        // ===================== UPDATE =====================
        [HttpPut("update/{id}")]
        public IActionResult Update(int id, [FromBody] PhieuKho model)
        {
            if (model == null)
                return BadRequest(new { message = "Dữ liệu không hợp lệ." });

            var oldData = _bus.GetById(id);
            if (oldData == null)
                return NotFound(new { message = "Không tìm thấy phiếu kho cần cập nhật." });

            model.MaPhieuKho = id;

            if (_bus.Update(model))
            {
                _logger.WriteLog("PhieuKho", id, "Sửa", oldData, model, _username);
                return Ok(new { message = "Cập nhật phiếu kho thành công!" });
            }

            return BadRequest(new { message = "Cập nhật phiếu kho thất bại!" });
        }

        // ===================== DELETE =====================
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var oldData = _bus.GetById(id);

            if (oldData == null)
                return NotFound(new { message = "Không tìm thấy phiếu kho để xóa!" });

            if (_bus.Delete(id))
            {
                _logger.WriteLog("PhieuKho", id, "Xóa", oldData, null, _username);
                return Ok(new { message = "Xóa phiếu kho thành công!" });
            }

            return BadRequest(new { message = "Xóa phiếu kho thất bại!" });
        }
    }
}
