using Microsoft.AspNetCore.Mvc;
using BaiMoiiii.BUS;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaiTroController : ControllerBase
    {
        private readonly VaiTroBUS _bus;

        public VaiTroController(VaiTroBUS bus)
        {
            _bus = bus;
        }

        // ========== GET ALL ==========
        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            var list = _bus.GetAll();
            if (!list.Any())
                return NotFound(new { message = "Không có vai trò nào trong hệ thống!" });
            return Ok(list);
        }

        // ========== GET BY ID ==========
        [HttpGet("get/{id}")]
        public IActionResult GetById(int id)
        {
            var vt = _bus.GetById(id);
            if (vt == null)
                return NotFound(new { message = "Không tìm thấy vai trò!" });
            return Ok(vt);
        }

        // ========== CREATE ==========
        [HttpPost("create")]
        public IActionResult Create([FromBody] VaiTro model)
        {
            try
            {
                if (_bus.Add(model))
                    return Ok(new { message = "Thêm vai trò thành công!" });
                return BadRequest(new { message = "Không thể thêm vai trò!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // ========== UPDATE ==========
        [HttpPut("update/{id}")]
        public IActionResult Update(int id, [FromBody] VaiTro model)
        {
            try
            {
                model.VaiTroID = id;
                if (_bus.Update(model))
                    return Ok(new { message = "Cập nhật vai trò thành công!" });
                return NotFound(new { message = "Không tìm thấy vai trò để cập nhật!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // ========== DELETE ==========
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (_bus.Delete(id))
                    return Ok(new { message = "Xóa vai trò thành công!" });
                return NotFound(new { message = "Không tìm thấy vai trò để xóa!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // ========== GET BY NAME ==========
        [HttpGet("get-by-name/{name}")]
        public IActionResult GetByName(string name)
        {
            var vt = _bus.GetByName(name);
            if (vt == null)
                return NotFound(new { message = "Không tìm thấy vai trò có tên này!" });
            return Ok(vt);
        }

        // ========== LẤY DANH SÁCH QUYỀN ==========
        [HttpGet("{id}/permissions")]
        public IActionResult GetPermissions(int id)
        {
            try
            {
                var list = _bus.GetPermissions(id);
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
