using System.Collections.Generic;
using BaiMoiiii.DAL;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.BUS
{
    public class PhieuKhoBUS
    {
        private readonly PhieuKhoDAL _dal;

        public PhieuKhoBUS(string connStr)
        {
            _dal = new PhieuKhoDAL(connStr);
        }

        public List<PhieuKho> GetAll() => _dal.GetAll();

        public PhieuKho? GetById(int id) => _dal.GetById(id);

        public bool Add(PhieuKho pk) => _dal.Insert(pk);

        public bool Update(PhieuKho pk) => _dal.Update(pk);

        public bool Delete(int id) => _dal.Delete(id);
    }
}
