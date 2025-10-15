using BaiMoiiii.DAL;
using BaiMoiiii.MODEL;

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
        public bool Create(BaoHanh bh) => _dal.Create(bh);
        public bool Update(BaoHanh bh) => _dal.Update(bh);
        public bool Delete(int id) => _dal.Delete(id);
    }
}
