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

        // Thêm checklist
        public bool Add(Checklist c)
        {
            if (string.IsNullOrWhiteSpace(c.Ten))
                throw new ArgumentException("Tên checklist không được để trống.");
            return _dal.Add(c);
        }

    }
}