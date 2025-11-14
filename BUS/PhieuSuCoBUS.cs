using System.Collections.Generic;
using BaiMoiiii.DAL;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.BUS
{
    public class PhieuSuCoBUS
    {
        private readonly PhieuSuCoDAL _dal;

        // 🎯 Nhận DAL qua DI
        public PhieuSuCoBUS(PhieuSuCoDAL dal)
        {
            _dal = dal;
        }

        public List<PhieuSuCo> GetAll() => _dal.GetAll();

        public PhieuSuCo? GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Mã phiếu sự cố không hợp lệ!");
            return _dal.GetById(id);
        }

        public bool Add(PhieuSuCo psc) => _dal.Insert(psc);

        public bool Update(PhieuSuCo psc) => _dal.Update(psc);
        public bool Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Mã phiếu sự cố không hợp lệ!");
            return _dal.Delete(id);
        }
    }
}
