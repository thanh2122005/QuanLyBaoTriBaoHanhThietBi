using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaiMoiiii.MODEL
{
    [Table("PhieuKho")]
    public class PhieuKho
    {
        [Key]
        public int MaPhieuKho { get; set; }

        [Required, StringLength(10)]
        public string Loai { get; set; } = "Nhap"; // Nhập hoặc Xuất

        [Required, DataType(DataType.DateTime)]
        public DateTime NgayLap { get; set; } = DateTime.UtcNow;

        public int? MaNV { get; set; }

        [NotMapped]
        public string? HoTen { get; set; } // Lấy từ bảng NhanVien

        [StringLength(200)]
        public string? TenNhanVien { get; set; } // Lưu tên nhân viên tại thời điểm lập phiếu

        [StringLength(500)]
        public string? GhiChu { get; set; }
    }
}
