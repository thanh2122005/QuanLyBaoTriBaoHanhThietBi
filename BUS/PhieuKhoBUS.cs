using System.Collections.Generic;
using BaiMoiiii.DAL;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.BUS
{
    public class PhieuKhoBUS
    {
        private readonly PhieuKhoDAL _dal;

        // 🎯 NHẬN DAL qua Dependency Injection (đúng chuẩn)
        public PhieuKhoBUS(PhieuKhoDAL dal)
        {
            _dal = dal;
        }

        public List<PhieuKho> GetAll() => _dal.GetAll();

        public PhieuKho? GetById(int id)
        {
            if (id <= 0) throw new ArgumentException("Mã phiếu kho không hợp lệ!");
            return _dal.GetById(id);
        }

        public bool Add(PhieuKho pk) => _dal.Insert(pk);

        public bool Update(PhieuKho pk)
        {
            if (pk.MaPhieuKho <= 0)
                throw new ArgumentException("Mã phiếu kho không hợp lệ!");
            return _dal.Update(pk);
        }

        public bool Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Mã phiếu kho không hợp lệ!");
            return _dal.Delete(id);
        }
    }
}
