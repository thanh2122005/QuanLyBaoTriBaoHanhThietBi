using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaiMoiiii.MODEL
{
    [Table("PhieuSuCo")]
    public class PhieuSuCo
    {
        [Key]
        public int MaSuCo { get; set; }

        [Required]
        public int MaTaiSan { get; set; }

        [NotMapped]
        public string? TenTaiSan { get; set; }

        [StringLength(500)]
        public string? MoTa { get; set; }

        [Required, StringLength(20)]
        public string MucDo { get; set; } = "Trung bình";  // Thấp, Trung bình, Cao, Khẩn

        [Required]
        public DateTime NgayBaoCao { get; set; } = DateTime.UtcNow;

        [Required, StringLength(30)]
        public string TrangThai { get; set; } = "Mới";  // Mới, Đang xử lý, Đã giải quyết, Đóng
    }
}
