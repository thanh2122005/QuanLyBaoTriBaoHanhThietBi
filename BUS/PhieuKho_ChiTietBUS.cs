using BaiMoiiii.DAL;
using BaiMoiiii.MODEL;
using System.Collections.Generic;

namespace BaiMoiiii.BUS
{
    public class PhieuKho_ChiTietBUS
    {
        private readonly PhieuKho_ChiTietDAL _dal;

        public PhieuKho_ChiTietBUS(PhieuKho_ChiTietDAL dal)
        {
            _dal = dal;
        }

        public List<PhieuKho_ChiTiet> GetAll() => _dal.GetAll();

        public PhieuKho_ChiTiet? GetById(int id) => _dal.GetById(id);  // ✅ Thêm dòng này

        public List<PhieuKho_ChiTiet> GetByPhieu(int id) => _dal.GetByPhieu(id);

        public bool Add(PhieuKho_ChiTiet model) => _dal.Add(model);

        public bool Update(PhieuKho_ChiTiet model) => _dal.Update(model);

        public bool Delete(int id) => _dal.Delete(id);
    }
}
