using System;

namespace BaiMoiiii.MODEL
{
    public class PhieuCongViec
    {
        public int MaPhieuCV { get; set; }
        public string Loai { get; set; }            // PM / CM
        public int? MaLich { get; set; }
        public int MaTaiSan { get; set; }
        public string TieuDe { get; set; }
        public string MucUuTien { get; set; }
        public int? SLA_Gio { get; set; }
        public string? MoTa { get; set; }
        public int? MaNV_PhanCong { get; set; }
        public DateTime NgayTao { get; set; }
        public string TrangThai { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayHoanThanh { get; set; }
        public string? GhiChu { get; set; }

        // Dữ liệu mô tả liên kết (nếu cần hiển thị)
        public string? TenTaiSan { get; set; }
        public string? TenNhanVien { get; set; }
    }
}
