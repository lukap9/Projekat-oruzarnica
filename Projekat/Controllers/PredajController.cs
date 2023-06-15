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
    public class PredajController : Controller
    {

       
        // GET: Predaj
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-U72BIQ7\\SQLEXPRESS;Initial Catalog=Oruzarnica;Integrated Security=True");

        [Authorize(Roles = "User,Admin")]
        public ActionResult PredajOruzje()
        {
            return View();
        }
        public ActionResult UbaciPredajOruzje(SkladisteOruzja modeel)
        {
            OruzarnicaEntities dbpredajaOruzja = new OruzarnicaEntities();
            SkladisteOruzja sklad = new SkladisteOruzja();
            con.Open();
            if (ModelState.IsValid)
            {
                Random rnd = new Random();
                int random = rnd.Next(0, 10000);
                sklad.BrojPriznanice = random;
                sklad.VremePriznanice = DateTime.Now;
                sklad.IDOruzja = modeel.IDOruzja;
                sklad.godinaProizvodnje = modeel.godinaProizvodnje;
                sklad.naziv = modeel.naziv;
                sklad.tip = modeel.tip;
                sklad.Ime = modeel.Ime;
                sklad.Prezime = modeel.Prezime;
                sklad.JMBG = modeel.JMBG;
                sklad.statusSkladistenja = "nije skladisten";
            }
            dbpredajaOruzja.SkladisteOruzjas.Add(sklad);
            dbpredajaOruzja.SaveChanges();
            Response.Write("<script>alert('Uspesno popunjen formular!');</script>");


            con.Close();
            ModelState.Clear();
            return View("PredajOruzje");
        }

        [Authorize(Roles = "User,Admin")]
        public ActionResult PredajMuniciju()
        {
            return View();
        }
        [Authorize(Roles = "User,Admin")]
        public ActionResult UbaciPredajMuniciju(SkladisteMunicije model)

        {
            OruzarnicaEntities dbpredajaMunicije = new OruzarnicaEntities();
            SkladisteMunicije oruzj = new SkladisteMunicije();
            con.Open();
            if (ModelState.IsValid)
            {
                Random rnd = new Random();
                int random = rnd.Next(0, 10000);
                oruzj.BrojPriznanice = random;
                oruzj.VremePriznanice = DateTime.Now;
                oruzj.IDMunicije = model.IDMunicije;
                oruzj.godinaProizvodnje = model.godinaProizvodnje;
                oruzj.kalibar = model.kalibar;
                oruzj.brojMetaka = model.brojMetaka;
                oruzj.Ime = model.Ime;
                oruzj.Prezime = model.Prezime;
                oruzj.JMBG = model.JMBG;
                oruzj.statusSkladistenja = "nije skladisten";
            }
                dbpredajaMunicije.SkladisteMunicijes.Add(oruzj);
                dbpredajaMunicije.SaveChanges();
                Response.Write("<script>alert('Uspesno popunjen formular!');</script>");
            
                
                con.Close();
                ModelState.Clear();
                return View("PredajMuniciju");
        }
    }
}