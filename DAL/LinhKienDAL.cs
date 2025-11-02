using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.DAL
{
    public class LinhKienDAL
    {
        private readonly string _conn;

        public LinhKienDAL(IConfiguration config)
        {
            _conn = config.GetConnectionString("DefaultConnection");
        }


    }
}
