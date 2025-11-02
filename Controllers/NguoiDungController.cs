using Microsoft.AspNetCore.Mvc;
using BaiMoiiii.BUS;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NguoiDungController : ControllerBase
    {
        private readonly NguoiDungBUS _bus;

        public NguoiDungController(NguoiDungBUS bus)
        {
            _bus = bus;
        }

        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            var list = _bus.GetAll();
            if (!list.Any())
                return NotFound(new { message = "Không có dữ liệu." });
            return Ok(list);
        }

        [HttpGet("get/{id}")]
        public IActionResult GetById(int id)
        {
            var user = _bus.GetById(id);
            if (user == null)
                return NotFound(new { message = $"Không tìm thấy người dùng có ID = {id}" });
            return Ok(user);
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] NguoiDung model)
        {
            try
            {
                if (_bus.Add(model))
                    return Ok(new { message = "Thêm người dùng thành công!" });
                return BadRequest(new { message = "Thêm thất bại!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("update/{id}")]
        public IActionResult Update(int id, [FromBody] NguoiDung model)
        {
            try
            {
                model.MaNguoiDung = id;
                if (_bus.Update(model))
                    return Ok(new { message = "Cập nhật thành công!" });
                return NotFound(new { message = "Không tìm thấy người dùng cần cập nhật." });
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
                    return Ok(new { message = "Xóa người dùng thành công!" });
                return NotFound(new { message = "Không tìm thấy người dùng để xóa." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("by-role/{roleId}")]
        public IActionResult GetByRole(int roleId)
        {
            var list = _bus.GetByRole(roleId);
            if (!list.Any())
                return NotFound(new { message = "Không có người dùng trong vai trò này." });
            return Ok(list);
        }

        [HttpGet("by-username/{username}")]
        public IActionResult GetByUsername(string username)
        {
            var user = _bus.GetByUsername(username);
            if (user == null)
                return NotFound(new { message = $"Không tìm thấy người dùng có tên đăng nhập '{username}'" });
            return Ok(user);
        }
    }
}
