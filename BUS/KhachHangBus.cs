using BaiMoiiii.DAL;
using BaiMoiiii.Models;

namespace BaiMoiiii.BUS
{
    public class KhachHangBus
    {
        private readonly KhachHangDAL _dal;

        public KhachHangBus(IConfiguration config)
        {
            _dal = new KhachHangDAL(config);
        }

        // =====================================================
        // 🔹 LẤY DANH SÁCH KHÁCH HÀNG
        // =====================================================
        public List<KhachHang> GetAll() => _dal.GetAll();

        // =====================================================
        // 🔹 LẤY THEO ID
        // =====================================================
        public KhachHang? GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Mã khách hàng không hợp lệ.");
            return _dal.GetById(id);
        }

        // =====================================================
        // 🔹 THÊM KHÁCH HÀNG MỚI
        // =====================================================
        public bool Add(KhachHang kh)
        {
            if (string.IsNullOrWhiteSpace(kh.TenKH))
                throw new ArgumentException("Tên khách hàng không được để trống.");

            return _dal.Add(kh);
        }

        // =====================================================
        // 🔹 CẬP NHẬT KHÁCH HÀNG
        // =====================================================
        public bool Update(KhachHang kh)
        {
            if (kh.MaKH <= 0)
                throw new ArgumentException("Mã khách hàng không hợp lệ.");

            if (string.IsNullOrWhiteSpace(kh.TenKH))
                throw new ArgumentException("Tên khách hàng không được để trống.");

            return _dal.Update(kh);
        }

        // =====================================================
        // 🔹 XÓA KHÁCH HÀNG
        // =====================================================
        public bool Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Mã khách hàng không hợp lệ.");
            return _dal.Delete(id);
        }
    }
}
