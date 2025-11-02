using System;

namespace BaiMoiiii.MODEL
{
    public class PhieuKho
    {
        public int MaPhieuKho { get; set; }
        public string Loai { get; set; } = "";  // "Nhap" hoặc "Xuat"
        public DateTime NgayLap { get; set; }
        public string? GhiChu { get; set; }
        public decimal? TongGiaTri { get; set; } // hỗ trợ KPI
    }
}
