using BaiMoiiii.DAL;
using BaiMoiiii.MODEL;
using System;
using System.Collections.Generic;

namespace BaiMoiiii.BUS
{
    public class PhieuKho_ChiTietBUS
    {
        private readonly PhieuKho_ChiTietDAL _dal;

        // 🎯 Nhận DAL qua Dependency Injection
        public PhieuKho_ChiTietBUS(PhieuKho_ChiTietDAL dal)
        {
            _dal = dal;
        }

        // ======================== GET ALL ========================
        public List<PhieuKho_ChiTiet> GetAll() => _dal.GetAll();

        // ======================== GET BY ID =======================
        public PhieuKho_ChiTiet? GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Mã chi tiết phiếu kho không hợp lệ!");
            return _dal.GetById(id);
        }

        // ======================== GET BY PHIẾU ======================
        public List<PhieuKho_ChiTiet> GetByPhieu(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Mã phiếu kho không hợp lệ!");
            return _dal.GetByPhieu(id);
        }

        // ======================== CREATE ========================
        public bool Add(PhieuKho_ChiTiet model)
        {
            if (model.MaPhieuKho <= 0)
                throw new ArgumentException("Mã phiếu kho không hợp lệ!");
            if (model.MaLinhKien <= 0)
                throw new ArgumentException("Mã linh kiện không hợp lệ!");
            if (model.SoLuong <= 0)
                throw new ArgumentException("Số lượng phải lớn hơn 0!");

            return _dal.Add(model);
        }

        // ======================== UPDATE ========================
        public bool Update(PhieuKho_ChiTiet model)
        {
            if (model.MaCT <= 0)
                throw new ArgumentException("Mã chi tiết phiếu kho không hợp lệ!");

            return _dal.Update(model);
        }

        // ======================== DELETE ========================
        public bool Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Mã chi tiết phiếu kho không hợp lệ!");
            return _dal.Delete(id);
        }
    }
}
