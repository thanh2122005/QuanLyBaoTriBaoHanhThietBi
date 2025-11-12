using BaiMoiiii.DAL;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.BUS
{
    public class NhatKyHeThongBUS
    {
        private readonly NhatKyHeThongDAL _dal;

        public NhatKyHeThongBUS(IConfiguration config)
        {
            _dal = new NhatKyHeThongDAL(config);
        }

        public List<NhatKyHeThong> GetAll() => _dal.GetAll();

        public NhatKyHeThong? GetById(long id) => _dal.GetById(id);


    }
}
