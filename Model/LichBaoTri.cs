using System;

namespace BaiMoiiii.MODEL
{
    public class LichBaoTri
    {
        public int MaLich { get; set; }
        public int MaTaiSan { get; set; }
        public int? MaNV { get; set; }
        public string TanSuat { get; set; }
        public int? SoNgayLapLai { get; set; }
        public DateTime NgayKeTiep { get; set; }
        public string? ChecklistMacDinh { get; set; }
        public bool HieuLuc { get; set; }
    }
}
