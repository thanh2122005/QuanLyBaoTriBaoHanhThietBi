using BaiMoiiii.DAL;
using BaiMoiiii.MODEL;
using System.Text.Json;

namespace BaiMoiiii.BUS
{
    public class VaiTroBUS
    {
        private readonly VaiTroDAL _dal;

        public VaiTroBUS(IConfiguration config)
        {
            _dal = new VaiTroDAL(config);
        }

        public List<VaiTro> GetAll() => _dal.GetAll();
        public VaiTro? GetById(int id) => _dal.GetById(id);
        public VaiTro? GetByName(string name) => _dal.GetByName(name);

        public bool Add(VaiTro vt)
        {
            if (string.IsNullOrWhiteSpace(vt.TenVaiTro))
                throw new ArgumentException("Tên vai trò không được để trống!");
            return _dal.Add(vt);
        }

        public bool Update(VaiTro vt)
        {
            if (vt.VaiTroID <= 0)
                throw new ArgumentException("Mã vai trò không hợp lệ!");
            return _dal.Update(vt);
        }

        public bool Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Mã vai trò không hợp lệ!");
            return _dal.Delete(id);
        }

        public List<string> GetPermissions(int id)
        {
            var vt = _dal.GetById(id);
            if (vt == null || string.IsNullOrEmpty(vt.QuyenJSON))
                return new List<string>();

            try
            {
                return JsonSerializer.Deserialize<List<string>>(vt.QuyenJSON) ?? new List<string>();
            }
            catch
            {
                throw new Exception("Dữ liệu quyền (QuyenJSON) không hợp lệ!");
            }
        }
    }
}
