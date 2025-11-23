using System.Collections.Generic;
using BaiMoiiii.MODEL;
using BaiMoiiii.DAL;

namespace BaiMoiiii.BUS
{
    public class NhatKyHeThongBUS
    {
        private readonly NhatKyHeThongDAL _dal;

        // BUS nhận DAL 
        public NhatKyHeThongBUS(NhatKyHeThongDAL dal)
        {
            _dal = dal;
        }

        public List<NhatKyHeThong> GetAll() => _dal.GetAll();

        public bool AddLog(NhatKyHeThong log)
        {
            return _dal.Insert(log);
        }

        public bool Delete(long id)
        {
            return _dal.Delete(id);
        }
        public List<NhatKyHeThong> GetPaged(int page, int size)
        {
            return _dal.GetPaged(page, size);
        }

        public int CountAll()
        {
            return _dal.CountAll();
        }

    }
}
