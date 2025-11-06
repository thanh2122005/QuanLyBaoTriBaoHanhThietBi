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

        
    }
}
