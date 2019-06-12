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
    }
}