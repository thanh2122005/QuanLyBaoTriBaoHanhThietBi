using BaiMoiiii.DAL;
using BaiMoiiii.Models;

namespace BaiMoiiii.BUS
{
    public class ChecklistItemBUS
    {
        private readonly ChecklistItemDAL _dal;

        public ChecklistItemBUS(IConfiguration config)
        {
            _dal = new ChecklistItemDAL(config);
        }

        public List<ChecklistItem> GetByChecklist(int checklistId)
        {
            if (checklistId <= 0)
                throw new ArgumentException("ChecklistID không hợp lệ.");
            return _dal.GetByChecklist(checklistId);
        }

        public bool Add(ChecklistItem item)
        {
            if (string.IsNullOrWhiteSpace(item.NoiDung))
                throw new ArgumentException("Nội dung không được để trống.");
            return _dal.Add(item);
        }

        public bool Update(ChecklistItem item)
        {
            if (item.ItemID <= 0)
                throw new ArgumentException("Mã item không hợp lệ.");
            return _dal.Update(item);
        }

        public bool Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Mã item không hợp lệ.");
            return _dal.Delete(id);
        }
    }
}
