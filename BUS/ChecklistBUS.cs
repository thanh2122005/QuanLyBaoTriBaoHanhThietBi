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

        // Cập nhật checklist
        public bool Update(Checklist c)
        {
            if (c.ChecklistID <= 0)
                throw new ArgumentException("Mã checklist không hợp lệ.");
            return _dal.Update(c);
        }

        // Xóa checklist
        public bool Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Mã checklist không hợp lệ.");
            return _dal.Delete(id);
        }
    }
}
