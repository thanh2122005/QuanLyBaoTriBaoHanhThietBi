using BaiMoiiii.API.Services;
using BaiMoiiii.BUS;
using BaiMoiiii.MODEL;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BaiMoiiii.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaiKhoanController : ControllerBase
    {
        private readonly TaiKhoanBUS _bus;

        // Lưu OTP tạm trong RAM (email → otp)
        private static Dictionary<string, string> _otpStore = new();

        public TaiKhoanController(TaiKhoanBUS bus)
        {
            _bus = bus;
        }

        // ====================== GET ALL ======================
        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            var list = _bus.GetAll();
            if (list == null || !list.Any())
                return NotFound(new { message = "Không có dữ liệu." });

            return Ok(list);
        }

        // ====================== GET BY ID ======================
        [HttpGet("get/{id}")]
        public IActionResult GetById(int id)
        {
            var item = _bus.GetById(id);
            if (item == null)
                return NotFound(new { message = "Không tìm thấy tài khoản." });

            return Ok(item);
        }

        // ====================== CREATE ======================
        [HttpPost("create")]
        public IActionResult Create([FromBody] TaiKhoan model)
        {
            if (model == null)
                return BadRequest(new { message = "Dữ liệu không hợp lệ." });

            if (_bus.Add(model))
                return Ok(new { message = "Thêm tài khoản thành công!" });

            return BadRequest(new { message = "Thêm thất bại!" });
        }

        // ====================== UPDATE ======================
        [HttpPut("update/{id}")]
        public IActionResult Update(int id, [FromBody] TaiKhoan model)
        {
            if (model == null)
                return BadRequest(new { message = "Dữ liệu không hợp lệ." });

            var old = _bus.GetById(id);
            if (old == null)
                return NotFound(new { message = "Không tìm thấy tài khoản!" });

            model.MaTaiKhoan = id;

            if (_bus.Update(model))
                return Ok(new { message = "Cập nhật tài khoản thành công!" });

            return BadRequest(new { message = "Cập nhật thất bại!" });
        }

        // ====================== DELETE ======================
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var old = _bus.GetById(id);
            if (old == null)
                return NotFound(new { message = "Không tìm thấy tài khoản để xóa!" });

            if (_bus.Delete(id))
                return Ok(new { message = "Xóa tài khoản thành công!" });

            return BadRequest(new { message = "Xóa thất bại!" });
        }


        // ============================================================
        // ===============   GỬI OTP QUA EMAIL   ======================
        // ============================================================
        [HttpPost("send-otp")]
        public IActionResult SendOtp([FromBody] ForgotPasswordRequest req)
        {
            if (req == null || string.IsNullOrEmpty(req.Email))
                return BadRequest(new { message = "Email không được bỏ trống!" });

            var user = _bus.GetAll().FirstOrDefault(x => x.Email == req.Email);
            if (user == null)
                return NotFound(new { message = "Email không tồn tại trong hệ thống!" });

            string otp = new Random().Next(100000, 999999).ToString();

            _otpStore[req.Email] = otp;

            try
            {
                EmailService.SendOTP(req.Email, otp);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Gửi email thất bại!", error = ex.Message });
            }

            return Ok(new { message = "OTP đã gửi vào email của bạn!" });
        }


        // ============================================================
        // ===============   XÁC MINH OTP   ===========================
        // ============================================================
        [HttpPost("verify-otp")]
        public IActionResult VerifyOtp([FromBody] VerifyOtpRequest req)
        {
            if (!_otpStore.ContainsKey(req.Email))
                return BadRequest(new { message = "OTP chưa được gửi hoặc đã hết hạn!" });

            if (_otpStore[req.Email] != req.Otp)
                return BadRequest(new { message = "OTP không hợp lệ!" });

            return Ok(new { message = "Xác minh OTP thành công!" });
        }


        [HttpPost("reset-password")]
        public IActionResult ResetPassword([FromBody] ResetPasswordDto req)
        {
            var account = _bus.GetByEmail(req.Email);
            if (account == null)
                return NotFound(new { message = "Không tìm thấy tài khoản!" });

            account.MatKhauHash = req.NewPassword;

            if (_bus.Update(account))
                return Ok(new { message = "Đặt lại mật khẩu thành công!" });

            return BadRequest(new { message = "Đặt lại mật khẩu thất bại!" });
        }

    }
}
