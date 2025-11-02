using BaiMoiiii.DAL;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.BUS
{
    public class NguoiDungBUS
    {
        private readonly NguoiDungDAL _dal;

        public NguoiDungBUS(IConfiguration config)
        {
            _dal = new NguoiDungDAL(config);
        }

        public List<NguoiDung> GetAll() => _dal.GetAll();
        public NguoiDung? GetById(int id) => _dal.GetById(id);
        public List<NguoiDung> GetByRole(int roleId) => _dal.GetByRole(roleId);
        public NguoiDung? GetByUsername(string username) => _dal.GetByUsername(username);

        public bool Add(NguoiDung nd)
        {
            if (string.IsNullOrWhiteSpace(nd.TenDangNhap))
                throw new ArgumentException("Tên đăng nhập không được để trống.");
            if (string.IsNullOrWhiteSpace(nd.MatKhauHash))
                throw new ArgumentException("Mật khẩu không được để trống.");
            return _dal.Add(nd);
        }

        public bool Update(NguoiDung nd)
        {
            if (nd.MaNguoiDung <= 0)
                throw new ArgumentException("Mã người dùng không hợp lệ.");
            return _dal.Update(nd);
        }

        public bool Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Mã người dùng không hợp lệ.");
            return _dal.Delete(id);
        }
    }
}
