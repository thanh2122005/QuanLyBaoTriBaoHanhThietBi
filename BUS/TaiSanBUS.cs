using BaiMoiiii.DAL;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.BUS
{
    public class TaiSanBUS
    {
        private readonly TaiSanDAL _dal;

        // ✅ Nhận string thay vì IConfiguration
        public TaiSanBUS(string connectionString)
        {
            _dal = new TaiSanDAL(connectionString);
        }

        public List<TaiSan> GetAll() => _dal.GetAll();
        public TaiSan? GetById(int id) => _dal.GetById(id);
        public List<TaiSan> Search(string keyword) => _dal.Search(keyword);

        public bool Add(TaiSan ts)
        {
            if (string.IsNullOrWhiteSpace(ts.TenTaiSan))
                throw new ArgumentException("Tên tài sản không được để trống!");
            return _dal.Add(ts);
        }

        public bool Update(TaiSan ts)
        {
            if (ts.MaTaiSan <= 0)
                throw new ArgumentException("Mã tài sản không hợp lệ!");
            return _dal.Update(ts);
        }

        public bool Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Mã tài sản không hợp lệ!");
            return _dal.Delete(id);
        }
    }
}
