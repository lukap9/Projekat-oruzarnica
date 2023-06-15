using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projekat.Controllers
{
   
    public class AdminController : Controller
    {
        // GET: Admin

        [Authorize(Roles = "Owner,Admin")]
        public ActionResult AdminLayoutPage()
        {
            return View();
        }
        [Authorize(Roles ="Owner,Admin")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Owner,Admin")]
        public ActionResult Oruzje()
        {

            return View();
        }
    }
}