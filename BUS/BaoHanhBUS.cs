using BaiMoiiii.DAL;
using BaiMoiiii.MODEL;
using System.Collections.Generic;

namespace BaiMoiiii.BUS
{
    public class BaoHanhBUS
    {
        private readonly BaoHanhDAL _dal;

        public BaoHanhBUS(BaoHanhDAL dal)
        {
            _dal = dal;
        }

        public List<BaoHanh> GetAll() => _dal.GetAll();

        public BaoHanh? GetById(int id) => _dal.GetById(id);

        public bool Create(BaoHanh bh)
        {
            if (string.IsNullOrEmpty(bh.NhaCungCap))
                return false;
            if (bh.NgayKetThuc < bh.NgayBatDau)
                return false;

            return _dal.Create(bh);
        }

        public bool Update(BaoHanh bh)
        {
            if (bh.MaBaoHanh <= 0)
                return false;
            if (bh.NgayKetThuc < bh.NgayBatDau)
                return false;

            return _dal.Update(bh);
        }

        public bool Delete(int id)
        {
            if (id <= 0)
                return false;
            return _dal.Delete(id);
        }
    }
}
