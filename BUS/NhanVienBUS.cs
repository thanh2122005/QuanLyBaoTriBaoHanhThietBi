using BaiMoiiii.DAL;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.BUS
{
    public class NhanVienBUS
    {
        private readonly NhanVienDAL _dal;

        public NhanVienBUS(IConfiguration config)
        {
            _dal = new NhanVienDAL(config);
        }

        public List<NhanVien> GetAll() => _dal.GetAll();
        public NhanVien? GetById(int id) => _dal.GetById(id);

        public bool Add(NhanVien nv)
        {
            if (string.IsNullOrWhiteSpace(nv.HoTen))
                throw new ArgumentException("Họ tên không được để trống.");
            if (string.IsNullOrWhiteSpace(nv.TrangThai))
                nv.TrangThai = "Hoạt động";
            return _dal.Add(nv);
        }

        public bool Update(NhanVien nv)
        {
            if (nv.MaNV <= 0)
                throw new ArgumentException("Mã nhân viên không hợp lệ.");
            return _dal.Update(nv);
        }

        public bool Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Mã nhân viên không hợp lệ.");
            return _dal.Delete(id);
        }

        public int CountActive() => _dal.CountActive();
    }
}
