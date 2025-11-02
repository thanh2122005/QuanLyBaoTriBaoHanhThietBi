using System;

namespace BaiMoiiii.MODEL
{
    public class TaiSan
    {
        public int MaTaiSan { get; set; }
        public string TenTaiSan { get; set; } = string.Empty;
        public string? ViTri { get; set; }
        public DateTime? NgayMua { get; set; }
        public int? MaBaoHanh { get; set; }
        public string? TenBaoHanh { get; set; }
        public int? MaKH { get; set; }
        public string? TenKhachHang { get; set; }
        public string TrangThai { get; set; } = "Đang hoạt động";
        public string? GhiChu { get; set; }
    }
}
