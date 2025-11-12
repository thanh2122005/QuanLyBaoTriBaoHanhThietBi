using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaiMoiiii.MODEL
{
    [Table("TaiKhoan")]
    public class TaiKhoan
    {
        [Key]
        public int MaTaiKhoan { get; set; }

        [Required, StringLength(100)]
        public string TenDangNhap { get; set; } = string.Empty;

        [Required, StringLength(500)]
        public string MatKhauHash { get; set; } = string.Empty;

        [Required, StringLength(20)]
        public string Role { get; set; } = "NhanVien"; // QuanLy hoặc NhanVien

        [StringLength(100)]
        public string? FullName { get; set; }

        [EmailAddress, StringLength(100)]
        public string? Email { get; set; }

        [StringLength(20)]
        public string? Phone { get; set; }

        [Required, StringLength(20)]
        public string TrangThai { get; set; } = "Hoạt động"; // Hoạt động hoặc Khóa

        [DataType(DataType.DateTime)]
        public DateTime NgayTao { get; set; } = DateTime.UtcNow;
    }
}
