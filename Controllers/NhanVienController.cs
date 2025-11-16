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
        private readonly LogHelper _logger;
        private readonly string _username;

        public NhanVienController(NhanVienBUS bus, LogHelper logger)
        {
            _bus = bus;
            _logger = logger;

            // sau này thay bằng JWT user
            _username = "Admin";
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
                {
                    _logger.WriteLog("NhanVien", model.MaNV, "Thêm", null, model, _username);
                    return Ok(new { message = "Thêm nhân viên thành công!" });
                }

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
                var oldData = _bus.GetById(id);
                if (oldData == null)
                    return NotFound(new { message = "Không tìm thấy nhân viên cần cập nhật." });

                model.MaNV = id;

                if (_bus.Update(model))
                {
                    _logger.WriteLog("NhanVien", id, "Sửa", oldData, model, _username);
                    return Ok(new { message = "Cập nhật nhân viên thành công!" });
                }

                return BadRequest(new { message = "Cập nhật thất bại!" });
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
                var oldData = _bus.GetById(id);
                if (oldData == null)
                    return NotFound(new { message = "Không tìm thấy nhân viên để xóa." });

                if (_bus.Delete(id))
                {
                    _logger.WriteLog("NhanVien", id, "Xóa", oldData, null, _username);
                    return Ok(new { message = "Xóa nhân viên thành công!" });
                }

                return BadRequest(new { message = "Xóa thất bại!" });
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
