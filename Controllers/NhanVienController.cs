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
            var list = _bus.GetAll();
            if (list == null || !list.Any())
                return NotFound(new { message = "Không có nhân viên nào trong hệ thống!" });

            return Ok(list);
        }

        // ===================== GET BY ID =====================
        [HttpGet("get/{id}")]
        public IActionResult GetById(int id)
        {
            var nv = _bus.GetById(id);
            if (nv == null)
                return NotFound(new { message = $"Không tìm thấy nhân viên có ID = {id}" });
            return Ok(nv);
        }

        // ===================== CREATE =====================
        [HttpPost("create")]
        public IActionResult Create([FromBody] NhanVien model)
        {
            try
            {
                if (_bus.Add(model))
                    return Ok(new { message = "Thêm nhân viên thành công!" });
                return BadRequest(new { message = "Thêm thất bại!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // ===================== UPDATE =====================
        [HttpPut("update/{id}")]
        public IActionResult Update(int id, [FromBody] NhanVien model)
        {
            try
            {
                model.MaNV = id;
                if (_bus.Update(model))
                    return Ok(new { message = "Cập nhật nhân viên thành công!" });
                return NotFound(new { message = "Không tìm thấy nhân viên cần cập nhật." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // ===================== DELETE =====================
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (_bus.Delete(id))
                    return Ok(new { message = "Xóa nhân viên thành công!" });
                return NotFound(new { message = "Không tìm thấy nhân viên để xóa." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // ===================== KPI: SỐ NHÂN VIÊN HOẠT ĐỘNG =====================
        [HttpGet("kpi/hoat-dong")]
        public IActionResult CountActive()
        {
            try
            {
                int count = _bus.CountActive();
                return Ok(new { message = "Thống kê thành công!", soNhanVienHoatDong = count });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
    