using System;

namespace BaiMoiiii.MODEL
{
    public class PhieuKho_ChiTiet
    {
        public int MaCT { get; set; }
        public int MaPhieuKho { get; set; }
        public int MaLinhKien { get; set; }
        public int SoLuong { get; set; }
        public decimal? DonGia { get; set; }

        // Thông tin mở rộng (nếu join LinhKien, PhieuKho)
        public string? TenLinhKien { get; set; }
        public string? LoaiPhieu { get; set; }
        public DateTime? NgayLap { get; set; }
    }
}
