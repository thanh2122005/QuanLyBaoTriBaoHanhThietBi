using BaiMoiiii.DAL;
using BaiMoiiii.Models;

namespace BaiMoiiii.BUS
{
    public class ChecklistBUS
    {
        private readonly ChecklistDAL _dal;

        public ChecklistBUS(IConfiguration config)
        {
            _dal = new ChecklistDAL(config);
        }

        // Lấy danh sách toàn bộ Checklist
        public List<Checklist> GetAll() => _dal.GetAll();

        }
    }
}
