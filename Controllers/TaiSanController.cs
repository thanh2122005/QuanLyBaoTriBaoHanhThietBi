using Microsoft.AspNetCore.Mvc;
using BaiMoiiii.BUS;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaiSanController : ControllerBase
    {
        private readonly TaiSanBUS _bus;
        private readonly LogHelper _logger;
        private readonly string _username;

        // Inject từ DI container
        public TaiSanController(TaiSanBUS bus, LogHelper logger)
        {
            _bus = bus;
            _logger = logger;

            // Sau này sẽ thay bằng JWT user
            _username = "Admin";
        }

        // =================== GET ALL ===================
        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            var list = _bus.GetAll();
            if (!list.Any())
                return NotFound(new { message = "Không có tài sản nào trong hệ thống!" });

            return Ok(list);
        }

        // =================== GET BY ID ===================
        [HttpGet("get/{id}")]
        public IActionResult GetById(int id)
        {
            var ts = _bus.GetById(id);
            if (ts == null)
                return NotFound(new { message = "Không tìm thấy tài sản!" });

            return Ok(ts);
        }

        // =================== CREATE ===================
        [HttpPost("create")]
        public IActionResult Create([FromBody] TaiSan model)
        {
            try
            {
                if (_bus.Add(model))
                {
                    _logger.WriteLog("TaiSan", model.MaTaiSan ?? 0, "Thêm", null, model, _username);

                    return Ok(new { message = "Thêm tài sản thành công!" });
                }

                return BadRequest(new { message = "Không thể thêm tài sản!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // =================== UPDATE ===================
        [HttpPut("update/{id}")]
        public IActionResult Update(int id, [FromBody] TaiSan model)
        {
            try
            {
                var oldData = _bus.GetById(id);
                if (oldData == null)
                    return NotFound(new { message = "Không tìm thấy tài sản cần cập nhật!" });

                model.MaTaiSan = id;

                if (_bus.Update(model))
                {
                    _logger.WriteLog("TaiSan", id, "Sửa", oldData, model, _username);
                    return Ok(new { message = "Cập nhật tài sản thành công!" });
                }

                return BadRequest(new { message = "Cập nhật tài sản thất bại!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // =================== DELETE ===================
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var oldData = _bus.GetById(id);
                if (oldData == null)
                    return NotFound(new { message = "Không tìm thấy tài sản để xóa!" });

                if (_bus.Delete(id))
                {
                    _logger.WriteLog("TaiSan", id, "Xóa", oldData, null, _username);
                    return Ok(new { message = "Xóa tài sản thành công!" });
                }

                return BadRequest(new { message = "Xóa thất bại!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // =================== SEARCH ===================
        [HttpGet("search")]
        public IActionResult Search([FromQuery] string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return BadRequest(new { message = "Từ khóa tìm kiếm không được để trống!" });

            var list = _bus.Search(keyword);

            if (!list.Any())
                return NotFound(new { message = "Không tìm thấy tài sản phù hợp!" });

            return Ok(list);
        }
    }
}
