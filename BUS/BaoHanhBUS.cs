using System.Collections.Generic;
using BaiMoiiii.DAL;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.BUS
{
    public class BaoHanhBUS
    {
        private readonly BaoHanhDAL _dal;

        public BaoHanhBUS(string connStr)
        {
            _dal = new BaoHanhDAL(connStr);
        }

        public List<BaoHanh> GetAll() => _dal.GetAll();

        public BaoHanh? GetById(int id) => _dal.GetById(id);

        public bool Add(BaoHanh bh) => _dal.Insert(bh);

        public bool Update(BaoHanh bh) => _dal.Update(bh);

        public bool Delete(int id) => _dal.Delete(id);
    }
}
