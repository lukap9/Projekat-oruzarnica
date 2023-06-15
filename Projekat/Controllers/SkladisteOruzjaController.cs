using Projekat.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.SqlClient;

namespace Projekat.Controllers
{
    public class SkladisteOruzjaController : Controller
    {
        OruzarnicaEntities dbSkladisteOruzja = new OruzarnicaEntities();
        // GET: Oruzje
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-U72BIQ7\\SQLEXPRESS;Initial Catalog=Oruzarnica;Integrated Security=True");
        [Authorize(Roles = "Owner,Admin")]
        public ActionResult SkladisteOruzja()
        { 
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Owner,Admin")]
        public ActionResult DodajSkladisteOruzje(SkladisteOruzja model)
        {
            SkladisteOruzja oruzj = new SkladisteOruzja();
            con.Open();
            if ((model.BrojPriznanice == null) || (model.IDOruzja == null) || (model.godinaProizvodnje == null) || String.IsNullOrEmpty(model.naziv) || String.IsNullOrEmpty(model.tip) || String.IsNullOrEmpty(model.Ime) || String.IsNullOrEmpty(model.Prezime) || (model.JMBG == null) || String.IsNullOrEmpty(model.statusSkladistenja))
            {
                Response.Write("<script>alert('Niste uneli sva polja!');</script>");
                return View("SkladisteOruzja");
            }
            if (ModelState.IsValid)
            {

                oruzj.BrojPriznanice = model.BrojPriznanice;
                oruzj.VremePriznanice = DateTime.Now;
                oruzj.IDOruzja = model.IDOruzja;
                oruzj.naziv = model.naziv;
                oruzj.tip = model.tip;
                oruzj.godinaProizvodnje = model.godinaProizvodnje;
                oruzj.Ime = model.Ime;
                oruzj.Prezime = model.Prezime;
                oruzj.JMBG = model.JMBG;
                oruzj.statusSkladistenja = model.statusSkladistenja;
        
      
                var provera = dbSkladisteOruzja.SkladisteOruzjas.Where(x => x.BrojPriznanice == model.BrojPriznanice).FirstOrDefault();
                if (provera != null)
                {
                    using (con)
                    {

                        SqlCommand com = new SqlCommand("IF EXISTS(Select * from SkladisteOruzja where BrojPriznanice='" + oruzj.BrojPriznanice + "') UPDATE SkladisteOruzja SET VremePriznanice='"+oruzj.VremePriznanice+"',IDOruzja= '" + oruzj.IDOruzja + "', naziv ='" + oruzj.naziv + "', tip" +
                        "='" + oruzj.tip + "',godinaProizvodnje ='" + oruzj.godinaProizvodnje + "',Ime = '"+ oruzj.Ime+"',Prezime='"+oruzj.Prezime+"',JMBG='"+oruzj.JMBG+"',statusSkladistenja='"+oruzj.statusSkladistenja+"'WHERE BrojPriznanice = '" + oruzj.BrojPriznanice + "'", con);
                        com.ExecuteNonQuery();
                        Response.Write("<script>alert('Uspesno izmenjeno skladistenje!');</script>");
                    }
                }

                else
                {


                    dbSkladisteOruzja.SkladisteOruzjas.Add(oruzj);
                    dbSkladisteOruzja.SaveChanges();
                    Response.Write("<script>alert('Uspesno uneto skladistenje!');</script>");
                    ModelState.Clear();
                }
                con.Close();

                return View("SkladisteOruzja");
            }

            return View();

        }
        [Authorize(Roles = "Owner,Admin")]
        public ActionResult ListaSkladistaOruzja()
        {
           
                var res = dbSkladisteOruzja.SkladisteOruzjas.ToList();
                return View(res);
            
        }
        [Authorize(Roles = "Owner,Admin")]
        public ActionResult Update()
        {

            return View("SkladisteOruzja", "SkladisteOruzja");

        }
        [Authorize(Roles = "Owner,Admin")]
        public ActionResult UpdateSkladisteOruzja(int BrojPriznanice)
        {
            var r = dbSkladisteOruzja.SkladisteOruzjas.Where(x => x.BrojPriznanice == BrojPriznanice).First();
            ViewBag.BrojPriznanice = r.BrojPriznanice;
            ViewBag.VremePriznanice = r.VremePriznanice;
            ViewBag.IDOruzja = r.IDOruzja;

            ViewBag.godinaProizvodnje = r.godinaProizvodnje;
            ViewBag.tip = r.tip;
            ViewBag.naziv = r.naziv;
            ViewBag.Ime = r.Ime;
            ViewBag.Prezime = r.Prezime;
            ViewBag.JMBG = r.JMBG;
            ViewBag.statusSkladistenja = r.statusSkladistenja;
            return View();

        }
        [Authorize(Roles = "Owner,Admin")]
        public ActionResult Delete(int BrojPriznanice)
        {
            var res = dbSkladisteOruzja.SkladisteOruzjas.Where(x => x.BrojPriznanice == BrojPriznanice).First();
            dbSkladisteOruzja.SkladisteOruzjas.Remove(res);
            dbSkladisteOruzja.SaveChanges();
            var list = dbSkladisteOruzja.SkladisteOruzjas.ToList();
            Response.Write("<script>alert('Uspesno obrisano oruzje!');</script>");
            return View("ListaSkladistaOruzja", list);
        }
    }
}