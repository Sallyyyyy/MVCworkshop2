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
        public List<LBBooks> SearchBook(LBSearchArg viewresult)
        {
            DataTable dt = new DataTable();
            //SQL
            string sql = @"Select BOOK_CLASS_NAME,BOOK_NAME,BOOK_BOUGHT_DATE,BOOK_STATUS,BOOK_KEEPER 
                    FROM dbo.BOOK_DATA as e 
	                  LEFT JOIN dbo.BOOK_CLASS as ctj
	                   ON (e.BOOK_CLASS_ID = ctj.BOOK_CLASS_ID)
                    Where (e.BOOK_NAME LIKE ('%'+@BOOK_NAME+'%') OR @BOOK_NAME='')
                    AND (ctj.BOOK_CLASS_NAME LIKE ('%'+@BOOK_CLASS_NAME+'%') OR @BOOK_CLASS_NAME='')
                    AND (e.BOOK_KEEPER LIKE ('%'+@BOOK_KEEPER+'%') OR @BOOK_KEEPER='')
                    AND (e.BOOK_STATUS LIKE ('%'+@BOOK_STATUS+'%') OR @BOOK_STATUS='')";
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
            return this.MapBookDataToList(dt);
        }
        public List<LBBooks> GetLibraryData(LBSearchArg viewresult)
        {
            DataTable dt = new DataTable();
            //SQL
            string sql = @"Select BOOK_CLASS_NAME,BOOK_NAME,BOOK_BOUGHT_DATE,BOOK_STATUS,BOOK_KEEPER 
                    FROM dbo.BOOK_DATA as e 
	                  LEFT JOIN dbo.BOOK_CLASS as ctj
	                   ON (e.BOOK_CLASS_ID = ctj.BOOK_CLASS_ID)";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapBookDataToList(dt);
        }
        private List<LBBooks> MapBookDataToList(DataTable employeeData)
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