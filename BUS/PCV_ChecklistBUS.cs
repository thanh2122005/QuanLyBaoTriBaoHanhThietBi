using BaiMoiiii.DAL;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.BUS
{
    public class PCV_ChecklistBUS
    {
        private readonly PCV_ChecklistDAL _dal;

        // Nhận DAL qua DI
        public PCV_ChecklistBUS(PCV_ChecklistDAL dal)
        {
            _dal = dal;
        }

        public List<PCV_Checklist> GetAll() => _dal.GetAll();

        public object GetSummary()
        {
            var data = _dal.GetSummary();

            int tongPhieu = data.Count;
            int daHoanTat = data.Count(x => x.DaHoanTat);
            int chuaHoanTat = tongPhieu - daHoanTat;

            var chiTiet = data.Select(x => new
            {
                x.MaPhieuCV,
                x.TongSo,
                x.HoanThanh,
                x.ChuaHoanThanh,
                x.DaHoanTat
            }).ToList();

            return new
            {
                TongPhieu = tongPhieu,
                DaHoanTat = daHoanTat,
                ChuaHoanTat = chuaHoanTat,
                ChiTiet = chiTiet
            };
        }
    }
}
