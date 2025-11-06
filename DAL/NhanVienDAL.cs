using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.DAL
{
    public class NhanVienDAL
    {
        private readonly string _conn;

        public NhanVienDAL(IConfiguration config)
        {
            _conn = config.GetConnectionString("DefaultConnection");
        }

      
    }
}
