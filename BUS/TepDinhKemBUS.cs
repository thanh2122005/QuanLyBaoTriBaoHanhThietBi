using BaiMoiiii.DAL;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.BUS
{
    public class TepDinhKemBUS
    {
        private readonly TepDinhKemDAL _dal;

        public TepDinhKemBUS(IConfiguration config)
        {
            _dal = new TepDinhKemDAL(config);
        }

        public List<TepDinhKem> GetAll() => _dal.GetAll();
        public TepDinhKem? GetById(int id) => _dal.GetById(id);

        public bool Add(TepDinhKem t)
        {
            if (string.IsNullOrWhiteSpace(t.TenTep))
                throw new ArgumentException("Tên tệp không được để trống!");
            return _dal.Add(t);
        }

        public bool Update(TepDinhKem t)
        {
            if (t.MaTep <= 0)
                throw new ArgumentException("Mã tệp không hợp lệ!");
            return _dal.Update(t);
        }

        public bool Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Mã tệp không hợp lệ!");
            return _dal.Delete(id);
        }
    }
}
