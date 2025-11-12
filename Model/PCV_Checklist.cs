using System;

namespace BaiMoiiii.MODEL
{
    public class PCV_Checklist
    {
        public int ID { get; set; }
        public int MaPhieuCV { get; set; }
        public int ItemID { get; set; }
        public bool DaHoanThanh { get; set; }

        // Thông tin chi tiết (nếu cần hiển thị)
        public string? TenMuc { get; set; }      // từ bảng ChecklistItem
        public string? MoTa { get; set; }        // mô tả checklist item
    }
}
