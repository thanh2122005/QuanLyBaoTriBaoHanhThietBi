using BaiMoiiii.DAL;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.BUS
{
    public class QuyenBUS
    {
        private readonly QuyenDAL _dal;

        public QuyenBUS(IConfiguration config)
        {
            _dal = new QuyenDAL(config);
        }

        public List<Quyen> GetAll() => _dal.GetAll();
        public Quyen? GetById(int id) => _dal.GetById(id);
        public List<Quyen> GetByGroup(string nhom) => _dal.GetByGroup(nhom);
        public List<Quyen> Search(string keyword) => _dal.Search(keyword);

        public bool Add(Quyen q)
        {
            if (string.IsNullOrWhiteSpace(q.TenQuyen))
                throw new ArgumentException("Tên quyền không được để trống!");
            return _dal.Add(q);
        }

        public bool Update(Quyen q)
        {
            if (q.QuyenID <= 0)
                throw new ArgumentException("Mã quyền không hợp lệ!");
            return _dal.Update(q);
        }

        public bool Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Mã quyền không hợp lệ!");
            return _dal.Delete(id);
        }
    }
}
