using System.Collections.Generic;
using BaiMoiiii.DAL;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.BUS
{
    public class TaiKhoanBUS
    {
        private readonly TaiKhoanDAL _dal;

        public TaiKhoanBUS(TaiKhoanDAL dal)
        {
            _dal = dal;
        }

        public List<TaiKhoan> GetAll() => _dal.GetAll();

        public TaiKhoan? GetById(int id) => _dal.GetById(id);

        public bool Add(TaiKhoan tk) => _dal.Insert(tk);

        public bool Update(TaiKhoan tk) => _dal.Update(tk);

        public bool Delete(int id) => _dal.Delete(id);

        public TaiKhoan? GetByEmail(string email)
{
    return _dal.GetAll().FirstOrDefault(x => x.Email == email);
}


    }
}
