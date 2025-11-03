using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.DAL
{
    public class NguoiDungDAL
    {
        private readonly string _conn;

        public NguoiDungDAL(IConfiguration config)
        {
            _conn = config.GetConnectionString("DefaultConnection");
        }

        
    }
}
