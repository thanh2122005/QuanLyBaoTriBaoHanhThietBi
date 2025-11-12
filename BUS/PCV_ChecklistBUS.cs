using BaiMoiiii.DAL;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.BUS
{
    public class PCV_ChecklistBUS
    {
        private readonly PCV_ChecklistDAL _dal;

        public PCV_ChecklistBUS(IConfiguration config)
        {
            _dal = new PCV_ChecklistDAL(config);
        }

        public List<PCV_Checklist> GetAll() => _dal.GetAll();

        public object GetSummary()
        {
            var data = _dal.GetSummary();

            int tongPhieu = data.Count;
            int daHoanTat = data.Count(x => x.DaHoanTat);
            int chuaHoanTat = data.Count(x => !x.DaHoanTat);

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
