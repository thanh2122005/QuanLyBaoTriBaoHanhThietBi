using BaiMoiiii.DAL;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.BUS
{
    public class PhieuSuCoBUS
    {
        private readonly PhieuSuCoDAL _dal;

        public PhieuSuCoBUS(IConfiguration config)
        {
            _dal = new PhieuSuCoDAL(config);
        }

        public List<PhieuSuCo> GetAll() => _dal.GetAll();
        public PhieuSuCo? GetById(int id) => _dal.GetById(id);
        public List<PhieuSuCo> GetByTaiSan(int maTaiSan) => _dal.GetByTaiSan(maTaiSan);

        public bool Add(PhieuSuCo s)
        {
            if (s.MaTaiSan <= 0)
                throw new ArgumentException("Mã tài sản không hợp lệ!");
            if (string.IsNullOrWhiteSpace(s.TrangThai))
                s.TrangThai = "Mới";
            s.NgayTao = DateTime.UtcNow;
            return _dal.Add(s);
        }

        public bool Update(PhieuSuCo s)
        {
            if (s.MaSuCo <= 0)
                throw new ArgumentException("Mã sự cố không hợp lệ!");
            return _dal.Update(s);
        }

        public bool Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Mã sự cố không hợp lệ!");
            return _dal.Delete(id);
        }

        public object GetSummaryByStatus()
        {
            var data = _dal.GetSummaryByStatus();
            return data.Select(x => new { x.TrangThai, x.SoLuong });
        }
    }
}
