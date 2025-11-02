using BaiMoiiii.DAL;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.BUS
{
    public class LinhKienBUS
    {
        private readonly LinhKienDAL _dal;

        public LinhKienBUS(IConfiguration config)
        {
            _dal = new LinhKienDAL(config);
        }
        public List<LinhKien> GetAll() => _dal.GetAll();

        public LinhKien? GetById(int id) => _dal.GetById(id);


    }
}
