using BaiMoiiii.DAL;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.BUS
{
    public class NhatKyHeThongBUS
    {
        private readonly NhatKyHeThongDAL _dal;

        public NhatKyHeThongBUS(IConfiguration config)
        {
            _dal = new NhatKyHeThongDAL(config);
        }

        public List<NhatKyHeThong> GetAll() => _dal.GetAll();

        public NhatKyHeThong? GetById(long id) => _dal.GetById(id);

        public bool Add(NhatKyHeThong log)
        {
            if (string.IsNullOrWhiteSpace(log.TenBang))
                throw new ArgumentException("Tên bảng không được để trống.");
            if (string.IsNullOrWhiteSpace(log.HanhDong))
                throw new ArgumentException("Hành động không được để trống.");
            if (log.MaBanGhi <= 0)
                throw new ArgumentException("Mã bản ghi không hợp lệ.");
            return _dal.Add(log);
        }

    }
}
