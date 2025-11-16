using Microsoft.AspNetCore.Mvc;
using BaiMoiiii.BUS;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LichBaoTriController : ControllerBase
    {
        private readonly LichBaoTriBUS _bus;
        private readonly LogHelper _logger;
        private readonly string _username;

        public LichBaoTriController(LichBaoTriBUS bus, LogHelper logger)
        {
            _bus = bus;
            _logger = logger;

            // ⚠️ sau này thay bằng token/JWT
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
        [HttpPost("create")]
        public IActionResult Create([FromBody] LichBaoTri model)
        {
            try
            {
                if (_bus.Add(model))
                {
                    _logger.WriteLog("LichBaoTri", model.MaLich, "Thêm", null, model, _username);
                    return Ok(new { message = "Thêm lịch bảo trì thành công!" });
                }

                return BadRequest(new { message = "Thêm thất bại." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // ================== UPDATE ==================
        [HttpPut("update/{id}")]
        public IActionResult Update(int id, [FromBody] LichBaoTri model)
        {
            try
            {
                var oldData = _bus.GetById(id);
                if (oldData == null)
                    return NotFound(new { message = "Không tìm thấy bản ghi cần cập nhật." });

                model.MaLich = id;

                if (_bus.Update(model))
                {
                    _logger.WriteLog("LichBaoTri", id, "Sửa", oldData, model, _username);
                    return Ok(new { message = "Cập nhật thành công!" });
                }

                return BadRequest(new { message = "Cập nhật thất bại." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // ================== DELETE ==================
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var oldData = _bus.GetById(id);
                if (oldData == null)
                    return NotFound(new { message = "Không tìm thấy bản ghi để xóa." });

                if (_bus.Delete(id))
                {
                    _logger.WriteLog("LichBaoTri", id, "Xóa", oldData, null, _username);
                    return Ok(new { message = "Xóa thành công!" });
                }

                return BadRequest(new { message = "Xóa thất bại!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
