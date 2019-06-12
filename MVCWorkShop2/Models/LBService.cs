using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MVCWorkShop2.Models
{
    public class LBService
    {
        //取得連線字串(Web.config)
        private string GetDBConnectionString()
        {
            return
                System.Configuration.ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString.ToString();
        }
        public List<LBBooks> GetLibraryData(LBSearchArg viewresult)
        {
            DataTable dt = new DataTable();
            //SQL
            string sql = @"Select BOOK_CLASS_NAME,BOOK_NAME,BOOK_BOUGHT_DATE,BOOK_STATUS,BOOK_KEEPER 
                    From dbo.BOOK_DATA,dbo.BOOK_CLASS 
                    Where BOOK_DATA.BOOK_CLASS_ID = BOOK_CLASS.BOOK_CLASS_ID";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@BOOK_NAME", viewresult.BookName == null ? string.Empty : viewresult.BookName));
                cmd.Parameters.Add(new SqlParameter("@BOOK_CLASS_NAME", viewresult.BookClassName == null ? string.Empty : viewresult.BookClassName));
                cmd.Parameters.Add(new SqlParameter("@BOOK_KEEPER", viewresult.BookKeeper == null ? string.Empty : viewresult.BookKeeper));
                cmd.Parameters.Add(new SqlParameter("@BOOK_STATUS", viewresult.BookStatus == null ? string.Empty : viewresult.BookStatus));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapEmployeeDataToList(dt);
        }
        private List<LBBooks> MapEmployeeDataToList(DataTable employeeData)
        {
            List<LBBooks> result = new List<LBBooks>();
            foreach (DataRow row in employeeData.Rows)
            {
                result.Add(new LBBooks()
                {
                    BookClassName = row["BOOK_CLASS_NAME"].ToString(),
                    BookName = row["BOOK_NAME"].ToString(),
                    BoughtDate = row["BOOK_BOUGHT_DATE"].ToString(),
                    BookStatus = row["BOOK_STATUS"].ToString(),
                    BookKeeper = row["BOOK_KEEPER"].ToString()
                });
            }
            return result;
        }
    }
}