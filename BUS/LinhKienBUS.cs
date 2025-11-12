using BaiMoiiii.DAL;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.BUS
{
    public class LinhKienBUS
    {
        private readonly LinhKienDAL _dal;

        public LinhKienBUS(IConfiguration config)
        {
            _dal = new LinhKienDAL(config);
        }

        public List<LinhKien> GetAll() => _dal.GetAll();

        public LinhKien? GetById(int id) => _dal.GetById(id);

        public bool Add(LinhKien lk)
        {
            if (string.IsNullOrWhiteSpace(lk.TenLinhKien))
                throw new ArgumentException("Tên linh kiện không được để trống.");
            return _dal.Add(lk);
        }

        public bool Update(LinhKien lk)
        {
            if (lk.MaLinhKien <= 0)
                throw new ArgumentException("Mã linh kiện không hợp lệ.");
            return _dal.Update(lk);
        }

        public bool Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Mã linh kiện không hợp lệ.");
            return _dal.Delete(id);
        }
    }
}
