using System;
using Projekat.Context;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;

namespace Projekat.Controllers
{
    public class UserController : Controller

    {
        OruzarnicaEntities dbUser = new OruzarnicaEntities();

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-U72BIQ7\\SQLEXPRESS;Initial Catalog=Oruzarnica;Integrated Security=True");

        [Authorize(Roles ="User")]
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LogOut()
        {
            return View("LogOut");
        }
        public ActionResult UnosPodataka()
        {
            return View();
        }
        public ActionResult DodajUnosPodataka(KorisnickiPodaci model)
        {
            KorisnickiPodaci podaci = new KorisnickiPodaci();
            if (ModelState.IsValid)
            {
                podaci.KorisnickoIme = HttpContext.User.Identity.Name;
                podaci.Ime = model.Ime;
                podaci.Prezime = model.Prezime;
                podaci.BrojTelefona = model.BrojTelefona;
                podaci.JMBG = model.JMBG;
                podaci.Adresa = model.Adresa;
             
                {
                    con.Open();
                    using (con)
                    {

                        SqlCommand com = new SqlCommand("UPDATE KorisnickiPodaci SET Ime ='" + podaci.Ime + "', Prezime" +
                            "='" + podaci.Prezime + "',brojTelefona ='" + podaci.BrojTelefona + "',JMBG ='" + podaci.JMBG +"',Adresa='"+podaci.Adresa+ "'WHERE KorisnickoIme = '" + HttpContext.User.Identity.Name + "'", con);
                        com.ExecuteNonQuery();
                        Response.Write("<script>alert('Uspesno izmenjeni podaci!');</script>");
                    }

                }

            }
            return View("UnosPodataka");
        }
    }
}