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



    }
}
