using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaiMoiiii.MODEL
{
    [Table("BaoHanh")]
    public class BaoHanh
    {
        [Key]
        public int MaBaoHanh { get; set; }

        [Required, StringLength(200)]
        public string NhaCungCap { get; set; } = string.Empty;

        [Required, DataType(DataType.Date)]
        public DateTime NgayBatDau { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime NgayKetThuc { get; set; }

        public string? DieuKhoan { get; set; }

        [Required]
        public int MaTaiSan { get; set; }   //

        [NotMapped]
        public string? TenTaiSan { get; set; } 
    }
}
