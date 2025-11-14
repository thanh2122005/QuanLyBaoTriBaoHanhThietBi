using Microsoft.AspNetCore.Mvc;
using BaiMoiiii.BUS;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaiKhoanController : ControllerBase
    {
        private readonly TaiKhoanBUS _bus;
        private readonly LogHelper _logger;
        private readonly string _username;

        public TaiKhoanController(TaiKhoanBUS bus, LogHelper logger)
        {
            _bus = bus;
            _logger = logger;

            // sau này thay bằng JWT
            _username = "Admin";
        }

        // ================= GET ALL =================
        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            var list = _bus.GetAll();

            if (list == null || !list.Any())
                return NotFound(new { message = "Không có dữ liệu." });

            return Ok(list);
        }

        // ================= GET BY ID =================
        [HttpGet("get/{id}")]
        public IActionResult GetById(int id)
        {
            var item = _bus.GetById(id);

            if (item == null)
                return NotFound(new { message = "Không tìm thấy tài khoản." });

            return Ok(item);
        }

        // ================= CREATE =================
        [HttpPost("create")]
        public IActionResult Create([FromBody] TaiKhoan model)
        {
            if (model == null)
                return BadRequest(new { message = "Dữ liệu không hợp lệ." });

            if (_bus.Add(model))
            {
                _logger.WriteLog("TaiKhoan", model.MaTaiKhoan, "Thêm", null, model, _username);
                return Ok(new { message = "Thêm tài khoản thành công!" });
            }

            return BadRequest(new { message = "Thêm thất bại!" });
        }

        // ================= UPDATE =================
        [HttpPut("update/{id}")]
        public IActionResult Update(int id, [FromBody] TaiKhoan model)
        {
            if (model == null)
                return BadRequest(new { message = "Dữ liệu không hợp lệ." });

            var oldData = _bus.GetById(id);
            if (oldData == null)
                return NotFound(new { message = "Không tìm thấy tài khoản cần cập nhật." });

            model.MaTaiKhoan = id;

            if (_bus.Update(model))
            {
                _logger.WriteLog("TaiKhoan", id, "Sửa", oldData, model, _username);
                return Ok(new { message = "Cập nhật tài khoản thành công!" });
            }

            return BadRequest(new { message = "Cập nhật thất bại!" });
        }

        // ================= DELETE =================
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var oldData = _bus.GetById(id);
            if (oldData == null)
                return NotFound(new { message = "Không tìm thấy tài khoản để xóa!" });

            if (_bus.Delete(id))
            {
                _logger.WriteLog("TaiKhoan", id, "Xóa", oldData, null, _username);
                return Ok(new { message = "Xóa tài khoản thành công!" });
            }

            return BadRequest(new { message = "Xóa thất bại!" });
        }
    }
}
