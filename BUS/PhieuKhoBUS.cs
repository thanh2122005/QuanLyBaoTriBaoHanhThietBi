using BaiMoiiii.DAL;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.BUS
{
    public class PhieuKhoBUS
    {
        private readonly PhieuKhoDAL _dal;

        public PhieuKhoBUS(IConfiguration config)
        {
            _dal = new PhieuKhoDAL(config);
        }

        public List<PhieuKho> GetAll() => _dal.GetAll();
        public PhieuKho? GetById(int id) => _dal.GetById(id);
        public bool Add(PhieuKho pk)
        {
            if (string.IsNullOrWhiteSpace(pk.Loai))
                throw new ArgumentException("Loại phiếu không được trống!");
            pk.NgayLap = DateTime.UtcNow;
            return _dal.Add(pk);
        }

        public bool Update(PhieuKho pk)
        {
            if (pk.MaPhieuKho <= 0)
                throw new ArgumentException("Mã phiếu kho không hợp lệ!");
            return _dal.Update(pk);
        }

        public bool Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Mã phiếu kho không hợp lệ!");
            return _dal.Delete(id);
        }

        public List<PhieuKho> GetByType(string loai) => _dal.GetByType(loai);

        public object GetKpiSummary()
        {
            var data = _dal.GetKpiSummary();
            return data.Select(x => new { x.Loai, x.SoLuong });
        }

        public List<PhieuKho> GetTotalValue() => _dal.GetTotalValue();
    }
}
