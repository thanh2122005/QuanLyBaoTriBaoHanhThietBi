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

    }
}
