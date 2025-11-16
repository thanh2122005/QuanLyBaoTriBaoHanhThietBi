using Microsoft.AspNetCore.Mvc;
using BaiMoiiii.BUS;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaoHanhController : ControllerBase
    {
        private readonly BaoHanhBUS _bus;
        private readonly LogHelper _logger;
        private readonly string _username;

        public BaoHanhController(BaoHanhBUS bus, LogHelper logger)
        {
            _bus = bus;
            _logger = logger;

            // ⚠️ Sau này dùng JWT token thay cho cứng như thế này
            _username = "Admin";
        }

        // ================== GET ALL ==================
        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            var list = _bus.GetAll();
            if (list == null || !list.Any())
                return NotFound(new { message = "Không có dữ liệu." });

            return Ok(list);
        }

        // ================== GET BY ID ==================
        [HttpGet("get/{id}")]
        public IActionResult GetById(int id)
        {
            var item = _bus.GetById(id);
            if (item == null)
                return NotFound(new { message = "Không tìm thấy bản ghi." });

            return Ok(item);
        }

        // ================== CREATE ==================
        [HttpPost]
        public IActionResult Create([FromBody] BaoHanh model)
        {
            if (model == null)
                return BadRequest(new { message = "Dữ liệu không hợp lệ." });

            var result = _bus.Add(model);

            if (result)
            {
                _logger.WriteLog("BaoHanh", model.MaBaoHanh, "Thêm", null, model, _username);
                return Ok(new { message = "Thêm bảo hành thành công!" });
            }

            return BadRequest(new { message = "Thêm bảo hành thất bại!" });
        }

        // ================== UPDATE ==================
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] BaoHanh model)
        {
            if (model == null)
                return BadRequest(new { message = "Dữ liệu không hợp lệ." });

            var oldData = _bus.GetById(id);
            if (oldData == null)
                return NotFound(new { message = "Không tìm thấy bản ghi để cập nhật." });

            model.MaBaoHanh = id;
            var result = _bus.Update(model);

            if (result)
            {
                _logger.WriteLog("BaoHanh", id, "Sửa", oldData, model, _username);
                return Ok(new { message = "Cập nhật bảo hành thành công!" });
            }

            return BadRequest(new { message = "Cập nhật thất bại!" });
        }

        // ================== DELETE ==================
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var oldData = _bus.GetById(id);
            if (oldData == null)
                return NotFound(new { message = "Không tìm thấy bản ghi để xóa." });

            var result = _bus.Delete(id);

            if (result)
            {
                _logger.WriteLog("BaoHanh", id, "Xóa", oldData, null, _username);
                return Ok(new { message = "Xóa bảo hành thành công!" });
            }

            return BadRequest(new { message = "Xóa thất bại!" });
        }
    }
}
