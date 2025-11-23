using BaiMoiiii.DAL;
using BaiMoiiii.Models;

namespace BaiMoiiii.BUS
{
    public class KhachHangBUS
    {
        private readonly KhachHangDAL _dal;

        public KhachHangBUS(KhachHangDAL dal)
        {
            _dal = dal;
        }

        public List<KhachHang> GetAll() => _dal.GetAll();

        public KhachHang? GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Mã khách hàng không hợp lệ!");
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

        // 🔹 Thêm Paging
        public List<KhachHang> GetPaged(int page, int pageSize)
        {
            if (page < 1 || pageSize < 1)
                throw new ArgumentException("Số trang hoặc số dòng không hợp lệ!");

            return _dal.GetPaged(page, pageSize);
        }

        // 🔹 Đếm tổng số KH
        public int CountAll() => _dal.CountAll();
    }
}
