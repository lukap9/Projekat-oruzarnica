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
    public class SkladisteMunicijeController : Controller
    {
        OruzarnicaEntities dbSkladisteMunicije = new OruzarnicaEntities();
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-U72BIQ7\\SQLEXPRESS;Initial Catalog=Oruzarnica;Integrated Security=True");
        // GET: SkladisteMunicije
        [Authorize(Roles = "Owner,Admin")]
        public ActionResult SkladisteMunicije()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Owner,Admin")]
        public ActionResult DodajSkladisteMunicije(SkladisteMunicije model)
        {
            SkladisteMunicije oruzj = new SkladisteMunicije();
            con.Open();
            if ((model.BrojPriznanice == null) ||(model.IDMunicije==null)|| (model.godinaProizvodnje == null) || String.IsNullOrEmpty(model.kalibar) || (model.brojMetaka==null) || String.IsNullOrEmpty(model.Ime)||String.IsNullOrEmpty(model.Prezime)||(model.JMBG==null)||String.IsNullOrEmpty(model.statusSkladistenja))
            {
                Response.Write("<script>alert('Niste uneli sva polja!');</script>");
                return View("SkladisteMunicije");
            }
            else if (ModelState.IsValid)
            {

                oruzj.BrojPriznanice = model.BrojPriznanice;
                oruzj.VremePriznanice = DateTime.Now;
                oruzj.IDMunicije = model.IDMunicije;
                oruzj.godinaProizvodnje = model.godinaProizvodnje;
                oruzj.kalibar = model.kalibar;
                oruzj.brojMetaka = model.brojMetaka;
                oruzj.Ime = model.Ime;
                oruzj.Prezime = model.Prezime;
                oruzj.JMBG = model.JMBG;
                oruzj.statusSkladistenja = model.statusSkladistenja;


                var provera = dbSkladisteMunicije.SkladisteMunicijes.Where(x => x.BrojPriznanice == model.BrojPriznanice).FirstOrDefault();
                if (provera != null)
                {
                    using (con)
                    {

                        SqlCommand com = new SqlCommand("IF EXISTS(Select * from SkladisteMunicije where BrojPriznanice='" + oruzj.BrojPriznanice + "') UPDATE SkladisteMunicije SET VremePriznanice='" + oruzj.VremePriznanice + "',IDMunicije= '" + oruzj.IDMunicije + "', godinaProizvodnje ='" + oruzj.godinaProizvodnje + "', kalibar" +
                        "='" + oruzj.kalibar + "',brojmetaka ='" + oruzj.brojMetaka + "',Ime = '" + oruzj.Ime + "',Prezime='" + oruzj.Prezime + "',JMBG='" + oruzj.JMBG + "',statusSkladistenja='" + oruzj.statusSkladistenja + "'WHERE BrojPriznanice = '" + oruzj.BrojPriznanice + "'", con);
                        com.ExecuteNonQuery();
                        Response.Write("<script>alert('Uspesno izmenjeno skladistenje!');</script>");
                    }
                }

                else
                {


                    dbSkladisteMunicije.SkladisteMunicijes.Add(oruzj);
                    dbSkladisteMunicije.SaveChanges();
                    Response.Write("<script>alert('Uspesno uneto skladistenje!');</script>");
                    
                }
                con.Close();
                ModelState.Clear();
                return View("Skladistemunicije");
            }

            return View();

        }
        [Authorize(Roles = "Owner,Admin")]
        public ActionResult ListaSkladistaMunicije()
        {

            var res = dbSkladisteMunicije.SkladisteMunicijes.ToList();
            return View(res);

        }
        [Authorize(Roles = "Owner,Admin")]
        public ActionResult Update()
        {

            return View("SkladisteMunicije", "SkladisteMunicije");

        }
        [Authorize(Roles = "Owner,Admin")]
        public ActionResult UpdateSkladisteMunicije(int BrojPriznanice)
        {
            var r = dbSkladisteMunicije.SkladisteMunicijes.Where(x => x.BrojPriznanice == BrojPriznanice).First();
            ViewBag.BrojPriznanice= r.BrojPriznanice  ;
            ViewBag.VremePriznanice = r.VremePriznanice;
            ViewBag.IDMunicije = r.IDMunicije;

            ViewBag.godinaProizvodnje = r.godinaProizvodnje;
           ViewBag.kalibar = r.kalibar;
            ViewBag.brojMetaka = r.brojMetaka;
            ViewBag.Ime = r.Ime;
            ViewBag.Prezime = r.Prezime;
            ViewBag.JMBG = r.JMBG;
             ViewBag.statusSkladistenja = r.statusSkladistenja;
            return View();

        }
    }
}