using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MVCWorkShop2.Models
{
    public class LBService
    {
        private string GetDBConnectionString()
        {
            return
                System.Configuration.ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString.ToString();
        }
        public int GetLibraryData()
        {
            int i;
            i = 0;
            //連線資料庫
            SqlConnection conn = new SqlConnection(this.GetDBConnectionString());
            return i;
        }
    }
}