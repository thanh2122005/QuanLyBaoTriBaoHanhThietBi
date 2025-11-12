using BaiMoiiii.DAL;
using BaiMoiiii.Models;

namespace BaiMoiiii.BUS
{
    public class KhachHangBUS
    {
        private readonly KhachHangDAL _dal;

        public KhachHangBUS(string connectionString)
        {
            _dal = new KhachHangDAL(connectionString);
        }

        public List<KhachHang> GetAll() => _dal.GetAll();

        public KhachHang? GetById(int id)
        {
            if (id <= 0) throw new ArgumentException("Mã khách hàng không hợp lệ!");
            return _dal.GetById(id);
        }

        public bool Add(KhachHang kh)
        {
            if (string.IsNullOrWhiteSpace(kh.TenKH))
                throw new ArgumentException("Tên khách hàng không được để trống!");
            return _dal.Add(kh);
        }

        public bool Update(KhachHang kh)
        {
            if (kh.MaKH <= 0)
                throw new ArgumentException("Mã khách hàng không hợp lệ!");
            return _dal.Update(kh);
        }

        public bool Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Mã khách hàng không hợp lệ!");
            return _dal.Delete(id);
        }
    }
}
