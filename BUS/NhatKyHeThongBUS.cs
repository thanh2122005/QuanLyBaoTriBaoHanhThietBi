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

    }
}
