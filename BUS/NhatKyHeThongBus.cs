using System.Collections.Generic;
using BaiMoiiii.MODEL;
using BaiMoiiii.DAL;

namespace BaiMoiiii.BUS
{
    public class NhatKyHeThongBUS
    {
        private readonly NhatKyHeThongDAL _dal;

        public NhatKyHeThongBUS(string connectionString)
        {
            _dal = new NhatKyHeThongDAL(connectionString);
        }

        public List<NhatKyHeThong> GetAll() => _dal.GetAll();

        public bool AddLog(NhatKyHeThong log) => _dal.Insert(log);

        public bool Delete(long id) => _dal.Delete(id); // ✅ Thêm hàm Delete
    }
}
