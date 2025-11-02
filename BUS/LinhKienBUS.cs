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


    }
}
