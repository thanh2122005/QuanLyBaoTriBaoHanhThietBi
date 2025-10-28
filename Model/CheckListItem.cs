using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaiMoiiii.Models
{
    [Table("ChecklistItem")]
    public class ChecklistItem
    {
        [Key]
        public int ItemID { get; set; }

        [Required]
        public int ChecklistID { get; set; }

        [Required, MaxLength(500)]
        public string NoiDung { get; set; } = string.Empty;

        [ForeignKey("ChecklistID")]
        public Checklist? Checklist { get; set; }
    }
}
