using BaiMoiiii.DAL;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.BUS
{
    public class PhieuKho_ChiTietBUS
    {
        private readonly PhieuKho_ChiTietDAL _dal;

        public PhieuKho_ChiTietBUS(IConfiguration config)
        {
            _dal = new PhieuKho_ChiTietDAL(config);
        }

        public List<PhieuKho_ChiTiet> GetAll() => _dal.GetAll();
        public PhieuKho_ChiTiet? GetById(int id) => _dal.GetById(id);
        public List<PhieuKho_ChiTiet> GetByPhieu(int maPhieu) => _dal.GetByPhieu(maPhieu);

        public bool Add(PhieuKho_ChiTiet ct)
        {
            if (ct.MaPhieuKho <= 0)
                throw new ArgumentException("Mã phiếu kho không hợp lệ!");
            if (ct.MaLinhKien <= 0)
                throw new ArgumentException("Mã linh kiện không hợp lệ!");
            if (ct.SoLuong <= 0)
                throw new ArgumentException("Số lượng phải lớn hơn 0!");

            return _dal.Add(ct);
        }

        public bool Update(PhieuKho_ChiTiet ct)
        {
            if (ct.MaCT <= 0)
                throw new ArgumentException("Mã chi tiết không hợp lệ!");
            return _dal.Update(ct);
        }

        public bool Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Mã chi tiết không hợp lệ!");
            return _dal.Delete(id);
        }
    }
}
