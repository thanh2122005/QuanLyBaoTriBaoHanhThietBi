using BaiMoiiii.DAL;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.BUS
{
    public class TaiSanBUS
    {
        private readonly TaiSanDAL _dal;

        // 🎯 Nhận đúng DAL thông qua DI
        public TaiSanBUS(TaiSanDAL dal)
        {
            _dal = dal;
        }

        public List<TaiSan> GetAll() => _dal.GetAll();

        public TaiSan? GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Mã tài sản không hợp lệ!");

            return _dal.GetById(id);
        }

        public List<TaiSan> Search(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return new List<TaiSan>();

            return _dal.Search(keyword);
        }

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
