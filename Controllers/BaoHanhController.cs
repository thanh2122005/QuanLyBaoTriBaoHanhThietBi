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

        public BaoHanhController(BaoHanhBUS bus)
        {
            _bus = bus;
        }

        // 🔹 GET: api/BaoHanh/get-all
        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            var list = _bus.GetAll();
            if (list == null || !list.Any())
                return NotFound(new { message = "Không có dữ liệu bảo hành nào." });

            return Ok(new
            {
                message = "Lấy danh sách bảo hành thành công!",
                data = list
            });
        }

        // 🔹 GET: api/BaoHanh/get/{id}
        [HttpGet("get/{id}")]
        public IActionResult GetById(int id)
        {
            var item = _bus.GetById(id);
            if (item == null)
                return NotFound(new { message = $"Không tìm thấy bảo hành có ID = {id}" });

            return Ok(new
            {
                message = "Lấy thông tin bảo hành thành công!",
                data = item
            });
        }

        // 🔹 POST: api/BaoHanh/create
        [HttpPost("create")]
        public IActionResult Create([FromBody] BaoHanh bh)
        {
            if (bh == null)
                return BadRequest(new { message = "Dữ liệu bảo hành không hợp lệ." });

            var result = _bus.Create(bh);
            if (!result)
                return StatusCode(500, new { message = "Không thể thêm bảo hành mới!" });

            return Ok(new
            {
                message = "Thêm mới bảo hành thành công!",
                data = bh
            });
        }

        // 🔹 PUT: api/BaoHanh/update/{id}
        [HttpPut("update/{id}")]
        public IActionResult Update(int id, [FromBody] BaoHanh bh)
        {
            if (bh == null || id != bh.MaBaoHanh)
                return BadRequest(new { message = "ID không hợp lệ hoặc dữ liệu bị thiếu." });

            var result = _bus.Update(bh);
            if (!result)
                return NotFound(new { message = $"Không thể cập nhật bảo hành có ID = {id}" });

            return Ok(new
            {
                message = "Cập nhật thông tin bảo hành thành công!",
                data = bh
            });
        }

        // 🔹 DELETE: api/BaoHanh/delete/{id}
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var result = _bus.Delete(id);
            if (!result)
                return NotFound(new { message = $"Không tìm thấy bảo hành có ID = {id}" });

            return Ok(new
            {
                message = "Xóa bảo hành thành công!",
                id
            });
        }
    }
}
