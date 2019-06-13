using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCWorkShop2.Models;

namespace MVCWorkShop2.Controllers
{
    public class LibraryController : Controller
    {
        // GET: Library
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Index(LBSearchArg viewresult)
        {
            LBService lbService = new LBService();
            List<LBBooks> bookList = lbService.GetLibraryData(viewresult);
            return this.Json(bookList);
        }
        //查詢書籍
        [HttpPost]
        public JsonResult Search(LBSearchArg viewresult)
        {
            LBService lbService = new LBService();
            List<LBBooks> bookList = lbService.SearchBook(viewresult);
            return this.Json(bookList);
        }
        //類別下拉式選單
        [HttpPost]
        public JsonResult ClassDropDown()
        {
            LBService lbService = new LBService();
            List<LBBooks> bookClassList = lbService.BookClassDrop();
            return this.Json(bookClassList);
        }
        //類別下拉式選單
        [HttpPost]
        public JsonResult StatusDropDown()
        {
            LBService lbService = new LBService();
            List<LBBooks> bookClassList = lbService.BookStatusDrop();
            return this.Json(bookClassList);
        }
        [HttpPost]
        public JsonResult KeeperDropDown()
        {
            LBService lbService = new LBService();
            List<LBBooks> bookClassList = lbService.BookKeeperDrop();
            return this.Json(bookClassList);
        }
        

    }
}