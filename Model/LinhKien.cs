using System;

namespace BaiMoiiii.MODEL
{
    public class LinhKien
    {
        public int MaLinhKien { get; set; }      // Khóa chính
        public string TenLinhKien { get; set; }  // Tên linh kiện
        public string? MaSo { get; set; }        // Mã số (có thể null)
        public int TonKho { get; set; }          // Số lượng tồn
    }
}
