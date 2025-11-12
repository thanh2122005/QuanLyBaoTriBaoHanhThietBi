using BaiMoiiii.DAL;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.BUS
{
    public class LichBaoTriBUS
    {
        private readonly LichBaoTriDAL _dal;

        public LichBaoTriBUS(IConfiguration config)
        {
            _dal = new LichBaoTriDAL(config);
        }

        public List<LichBaoTri> GetAll() => _dal.GetAll();

        public LichBaoTri? GetById(int id) => _dal.GetById(id);

        public bool Add(LichBaoTri l)
        {
            if (string.IsNullOrWhiteSpace(l.TanSuat))
                throw new ArgumentException("Tần suất không được để trống.");
            return _dal.Add(l);
        }

        public bool Update(LichBaoTri l)
        {
            if (l.MaLich <= 0)
                throw new ArgumentException("Mã lịch không hợp lệ.");
            return _dal.Update(l);
        }

        public bool Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Mã lịch không hợp lệ.");
            return _dal.Delete(id);
        }
    }
}
