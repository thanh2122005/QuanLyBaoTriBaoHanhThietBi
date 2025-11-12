using BaiMoiiii.DAL;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.BUS
{
    public class PCV_ChecklistBUS
    {
        private readonly PCV_ChecklistDAL _dal;

        public PCV_ChecklistBUS(IConfiguration config)
        {
            _dal = new PCV_ChecklistDAL(config);
        }


    }
}
