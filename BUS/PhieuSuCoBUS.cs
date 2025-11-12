using System.Collections.Generic;
using BaiMoiiii.DAL;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.BUS
{
    public class PhieuSuCoBUS
    {
        private readonly PhieuSuCoDAL _dal;

        public PhieuSuCoBUS(string connStr)
        {
            _dal = new PhieuSuCoDAL(connStr);
        }

        public List<PhieuSuCo> GetAll() => _dal.GetAll();

        public PhieuSuCo? GetById(int id) => _dal.GetById(id);

        public bool Add(PhieuSuCo psc) => _dal.Insert(psc);

        public bool Update(PhieuSuCo psc) => _dal.Update(psc);

        public bool Delete(int id) => _dal.Delete(id);
    }
}
