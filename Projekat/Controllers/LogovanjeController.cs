using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using Projekat.Models;
using System.Web.Security;
using Projekat.Context;

namespace Projekat.Controllers
{
    public class LogovanjeController : Controller
    {

        // GET: Logovanje

        HttpCookie myCookie = new HttpCookie("kolacic");

        db_testEntities dbtest = new db_testEntities();
        
    public ActionResult LoginStrana()
        {
            return View();
        }
        
        
        [HttpPost]
        public ActionResult Authorize(Logovanje model)
        {
            
            using (OruzarnicaEntities dbLogin = new OruzarnicaEntities())
            {
                var proveraa = dbLogin.Logovanjes.Where(x => x.KorisnickoIme == model.KorisnickoIme);
                
                if (String.IsNullOrEmpty(model.KorisnickoIme) || String.IsNullOrEmpty(model.Sifra))
                {
                    Response.Write("<script>alert('Niste uneli korisnicko ime ili sifru. Pokusajte ponovo.');</script>");
                    return View("LoginStrana");
                }
                
           

                var userDetails = dbLogin.Logovanjes.Where(x => x.KorisnickoIme == model.KorisnickoIme && x.Sifra == model.Sifra).FirstOrDefault();
                if (userDetails == null)
                {
                    Response.Write("<script>confirm('Neuspesan login. Pokusajte ponovo.');</script>");
                    return View("LoginStrana");
                }

                
                else
                {
                    if (Roles.IsUserInRole(model.KorisnickoIme, "User"))
                    {
                        Response.Write("<script>alert('Uspesan login!');</script>");
                        FormsAuthentication.SetAuthCookie(model.KorisnickoIme, false);
                        myCookie.Value = model.KorisnickoIme;
                        return RedirectToAction("Index", "User");
                    }
                    else if (Roles.IsUserInRole(model.KorisnickoIme, "Admin"))
                    {
                        Response.Write("<script>alert('Uspesan admin login!');</script>");
                        FormsAuthentication.SetAuthCookie(model.KorisnickoIme, false);
                        myCookie.Value = model.KorisnickoIme;
                        return RedirectToAction("AdminLayoutPage", "Admin");
                    }
                    else if (Roles.IsUserInRole(model.KorisnickoIme, "Owner"))
                    {
                        Response.Write("<script>alert('Dobrodosli. Ulogovani ste kao vlasnik.');</script>");
                        FormsAuthentication.SetAuthCookie(model.KorisnickoIme, false);
                        myCookie.Value = model.KorisnickoIme;
                        return RedirectToAction("OwnerLayoutPage","Owner");
                    }
                    else return RedirectToAction("OwnerLayoutPage","Owner");
                }
            }

        }

    }
   
      
}