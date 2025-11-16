using System;

namespace BaiMoiiii.MODEL
{
    public class NguoiDung
    {
        public int MaNguoiDung { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhauHash { get; set; }
        public string? Email { get; set; }
        public int? MaNV { get; set; }       // có thể null
        public int VaiTroID { get; set; }
        public string TrangThai { get; set; }
        public DateTime NgayTao { get; set; }
        public DateTime? LanDangNhapCuoi { get; set; }
    }
}
