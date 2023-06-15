using Projekat.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Projekat.Controllers
{
    public class OwnerController : Controller
    {
        // GET: Owner
        // GET: Admin

        OruzarnicaEntities dbOwner = new OruzarnicaEntities();
        OruzarnicaEntities dbLogin = new OruzarnicaEntities();
        [Authorize(Roles = "Owner")]
        public ActionResult OwnerLayoutPage()
        {
            return View();
        }
        [Authorize(Roles = "Owner")]
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Owner")]
        public ActionResult ObrisiAdmina()
        {
                return View();
            
        }
        
        public ActionResult ObrisiAdminaDelete(string KorisnickoIme)
        {
            string imekorisnika = KorisnickoIme;
            var res = dbOwner.Logovanjes.Where(x => x.KorisnickoIme == imekorisnika).FirstOrDefault();
            if (res != null)
            {
                dbOwner.Logovanjes.Remove(res);
                dbOwner.SaveChanges();

                Response.Write("<script>alert('Uspesno obrisan administrator!');</script>");
                return View("ObrisiAdmina");
            }
            else
            {

                Response.Write("<script>alert('Administator sa unetim imenom ne postoji!');</script>");
            }
            return View("ObrisiAdmina");
        }
        public ActionResult AdminList()
        {
            List<Logovanje> p = new List<Logovanje>();
            foreach (Logovanje v in dbLogin.Logovanjes)
            {
                
                if (Roles.IsUserInRole(v.KorisnickoIme, "Admin"))
                {

                    p.Add(v);
                }
            }
          
            return View(p);
        }
    }
}