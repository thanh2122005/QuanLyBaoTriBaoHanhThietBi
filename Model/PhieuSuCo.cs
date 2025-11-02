using System;

namespace BaiMoiiii.MODEL
{
    public class PhieuSuCo
    {
        public int MaSuCo { get; set; }
        public int MaTaiSan { get; set; }
        public string? TenTaiSan { get; set; }
        public int? MaKH { get; set; }
        public string? TenKhachHang { get; set; }
        public string? NguoiBao { get; set; }
        public string MucUuTien { get; set; } = "Trung bình";
        public int? SLA_Gio { get; set; }
        public int? MaNV_TiepNhan { get; set; }
        public string? TenNhanVien { get; set; }
        public DateTime NgayTao { get; set; } = DateTime.UtcNow;
        public string TrangThai { get; set; } = "Mới";
        public string? MoTa { get; set; }
        public int? MaPhieuCV { get; set; }
    }
}
