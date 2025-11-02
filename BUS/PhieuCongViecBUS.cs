using BaiMoiiii.DAL;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.BUS
{
    public class PhieuCongViecBUS
    {
        private readonly PhieuCongViecDAL _dal;

        public PhieuCongViecBUS(IConfiguration config)
        {
            _dal = new PhieuCongViecDAL(config);
        }

        public List<PhieuCongViec> GetAll() => _dal.GetAll();
        public PhieuCongViec? GetById(int id) => _dal.GetById(id);

        public bool Add(PhieuCongViec p)
        {
            if (string.IsNullOrWhiteSpace(p.TieuDe))
                throw new ArgumentException("Tiêu đề không được để trống.");
            if (p.MaTaiSan <= 0)
                throw new ArgumentException("Mã tài sản không hợp lệ.");
            if (string.IsNullOrWhiteSpace(p.TrangThai))
                p.TrangThai = "Mới";
            if (string.IsNullOrWhiteSpace(p.MucUuTien))
                p.MucUuTien = "Trung bình";
            p.NgayTao = DateTime.UtcNow;
            return _dal.Add(p);
        }

        public bool Update(PhieuCongViec p)
        {
            if (p.MaPhieuCV <= 0)
                throw new ArgumentException("Mã phiếu công việc không hợp lệ.");
            return _dal.Update(p);
        }

        public bool Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Mã phiếu công việc không hợp lệ.");
            return _dal.Delete(id);
        }
    }
}
