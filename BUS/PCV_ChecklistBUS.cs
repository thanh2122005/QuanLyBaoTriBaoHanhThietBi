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

        public List<PCV_Checklist> GetAll()
        {
            Console.WriteLine("BUS: Gọi DAL để lấy danh sách checklist...");
            return _dal.GetAll();
        }

        public object GetSummary()
        {
            var data = _dal.GetSummary();
            int totalForms = data.Count;
            int done = data.Count(x => x.DaHoanTat);
            int notDone = data.Count(x => !x.DaHoanTat);

            return new
            {
                TongPhieu = totalForms,
                DaHoanTat = done,
                ChuaHoanTat = notDone,
                ChiTiet = data.Select(x => new
                {
                    x.MaPhieuCV,
                    x.TongSo,
                    x.HoanThanh,
                    x.ChuaHoanThanh,
                    x.DaHoanTat
                }).ToList()
            };
        }

    }
}
