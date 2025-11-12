using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.DAL
{
    public class NhatKyHeThongDAL
    {
        private readonly string _conn;

        public NhatKyHeThongDAL(IConfiguration config)
        {
            _conn = config.GetConnectionString("DefaultConnection");
        }


    }
}
