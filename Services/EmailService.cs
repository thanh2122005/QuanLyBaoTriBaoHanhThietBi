using System.Net;
using System.Net.Mail;

namespace BaiMoiiii.API.Services
{
    public class EmailService
    {
        private static string FROM = "damtrungdung180@gmail.com";
        private static string APP_PASSWORD = "tlyovogveazevrdq"; // App Password

        public static void SendOTP(string toEmail, string otp)
        {
            var smtp = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                EnableSsl = true,
                Credentials = new NetworkCredential(FROM, APP_PASSWORD)
            };

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(FROM, "Hệ thống Bảo trì");
            msg.To.Add(toEmail);
            msg.Subject = "Mã OTP xác thực đặt lại mật khẩu";
            msg.Body = $"Mã OTP của bạn là: {otp}\nOTP có hiệu lực trong 5 phút.";

            smtp.Send(msg);
        }
    }
}
