using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Projekat.Controllers
{
    public class LogOutController : Controller
    {
        // GET: LogOut
        [Authorize(Roles = "Owner,Admin")]
        public ActionResult LogOut()
        {

            return View("LogOut");

        }
        [Authorize(Roles = "Owner,Admin,User")]
        public ActionResult UserLogOut()
        {

            return View("UserLogOut");

        }
        public ActionResult Izlogujse()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        
    }
}