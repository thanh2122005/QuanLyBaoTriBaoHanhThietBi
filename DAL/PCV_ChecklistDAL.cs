using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.DAL
{
    public class PCV_ChecklistDAL
    {
        private readonly string _conn;

        public PCV_ChecklistDAL(IConfiguration config)
        {
            _conn = config.GetConnectionString("DefaultConnection");
        }


    }
}
