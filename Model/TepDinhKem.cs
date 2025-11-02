namespace BaiMoiiii.MODEL
{
    public class TepDinhKem
    {
        public int MaTep { get; set; }
        public string TenTep { get; set; }
        public string? DuongDan { get; set; }
        public string? LoaiTep { get; set; }
        public DateTime NgayTaiLen { get; set; } = DateTime.UtcNow;
    }
}
