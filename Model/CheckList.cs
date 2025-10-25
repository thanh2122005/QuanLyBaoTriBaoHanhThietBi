using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaiMoiiii.Models
{
    [Table("Checklist")]
    public class Checklist
    {
        [Key]
        public int ChecklistID { get; set; }

        [Required, MaxLength(200)]
        public string Ten { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? MoTa { get; set; }
    }
}
