using Microsoft.AspNetCore.Mvc;
using BaiMoiiii.BUS;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NhanVienController : ControllerBase
    {
        private readonly NhanVienBUS _bus;

        public NhanVienController(NhanVienBUS bus)
        {
            _bus = bus;
        }

        // ===================== GET ALL =====================
        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            try
            {
                var list = _bus.GetAll();
                if (list == null || !list.Any())
                    return NotFound(new { message = "Không có nhân viên nào trong hệ thống." });

                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi khi lấy danh sách nhân viên.", error = ex.Message });
            }
        }

        // ===================== GET BY ID =====================
        [HttpGet("get/{id:int}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var nv = _bus.GetById(id);
                if (nv == null)
                    return NotFound(new { message = $"Không tìm thấy nhân viên có ID = {id}" });
                return Ok(nv);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi khi lấy nhân viên theo ID.", error = ex.Message });
            }
        }

        // ===================== CREATE =====================
        [HttpPost("create")]
        public IActionResult Create([FromBody] NhanVien model)
        {
            if (model == null)
                return BadRequest(new { message = "Dữ liệu không hợp lệ." });

            try
            {
                var result = _bus.Add(model);
                if (result)
                    return CreatedAtAction(nameof(GetById), new { id = model.MaNV },
                        new { message = "Thêm nhân viên thành công!", data = model });

                return BadRequest(new { message = "Không thể thêm nhân viên." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi khi thêm nhân viên.", error = ex.Message });
            }
        }


        // ===================== UPDATE =====================
        [HttpPut("update/{id:int}")]
        public IActionResult Update(int id, [FromBody] NhanVien model)
        {
            if (model == null)
                return BadRequest(new { message = "Dữ liệu không hợp lệ." });

            try
            {
                model.MaNV = id;
                if (_bus.Update(model))
                    return Ok(new { message = "Cập nhật nhân viên thành công!" });

                return NotFound(new { message = $"Không tìm thấy nhân viên có ID = {id}" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi khi cập nhật nhân viên.", error = ex.Message });
            }
        }

        // ===================== DELETE =====================
        [HttpDelete("delete/{id:int}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (_bus.Delete(id))
                    return Ok(new { message = "Xóa nhân viên thành công!" });

                return NotFound(new { message = $"Không tìm thấy nhân viên có ID = {id}" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi khi xóa nhân viên.", error = ex.Message });
            }
        }


    }
}
    