using System.Text.Json;
using BaiMoiiii.MODEL;
using BaiMoiiii.DAL;
using Microsoft.Extensions.Configuration;

namespace BaiMoiiii.BUS
{
    public class LogHelper
    {
        private readonly string _connection;

        public LogHelper(IConfiguration config)
        {
            _connection = config.GetConnectionString("DefaultConnection");
        }

        public void WriteLog(string table, int id, string action, object oldData, object newData, string username)
        {
            var log = new NhatKyHeThong
            {
                TenBang = table,
                MaBanGhi = id,
                HanhDong = action,
                GiaTriCu = oldData != null ? JsonSerializer.Serialize(oldData) : null,
                GiaTriMoi = newData != null ? JsonSerializer.Serialize(newData) : null,
                ThayDoiBoi = username
            };

            new NhatKyHeThongDAL(_connection).Insert(log);
        }
    }
}
