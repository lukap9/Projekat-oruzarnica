using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using Projekat.Context;

namespace Projekat.Controllers
{
    public class KupiController : Controller
    {
        // GET: Kupi
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-U72BIQ7\\SQLEXPRESS;Initial Catalog=Oruzarnica;Integrated Security=True");
        [Authorize(Roles = "User,Admin,Owner")]
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "User,Admin,Owner")]
        public ActionResult KupiOruzje()
        {
            string imekorisnika = HttpContext.User.Identity.Name.ToString();
           
                var r = dbKupi.KorisnickiPodacis.Where(x => x.KorisnickoIme == imekorisnika).First();
                ViewBag.KorisnickoIme = r.KorisnickoIme;
            ViewBag.Ime = r.Ime;
            ViewBag.Prezime = r.Prezime;
            ViewBag.BrojTelefona=r.BrojTelefona;
            ViewBag.JMBG = r.JMBG;
            ViewBag.Adresa = r.Adresa;
                return View();
            
            
        }
        OruzarnicaEntities dbKupi = new OruzarnicaEntities();
        [Authorize(Roles = "User,Admin,Owner")]
        public ActionResult UbaciKupiOruzje(KupovinaOruzja sklad)
        {
            
            KupovinaOruzja kuporuzj = new KupovinaOruzja();
            con.Open();
            if (String.IsNullOrEmpty(sklad.Ime) || String.IsNullOrEmpty(sklad.Prezime) || String.IsNullOrEmpty(sklad.Adresa) || (sklad.BrojTelefona == null) || (sklad.BrojKartice == null) || (sklad.PINKartice == null) || (sklad.JMBG == null) || (sklad.IDOruzja == null))
            {
                Response.Write("<script>alert('Niste uneli sva polja!');</script>");
                return View("KupiMuniciju");
            }
            
                kuporuzj.Ime = sklad.Ime;
                kuporuzj.Prezime = sklad.Prezime;
                kuporuzj.Adresa = sklad.Adresa;
                kuporuzj.BrojTelefona = sklad.BrojTelefona;
                kuporuzj.BrojKartice = sklad.BrojKartice;
                kuporuzj.PINKartice = sklad.PINKartice;
                kuporuzj.JMBG = sklad.JMBG;
                kuporuzj.IDOruzja = sklad.IDOruzja;

               

                var provera = dbKupi.KupovinaOruzjas.Where(x => x.IDOruzja == sklad.IDOruzja).FirstOrDefault();
                if (provera != null)
                {
                    using (con)
                    {

                        SqlCommand comoruzje = new SqlCommand("IF EXISTS(Select * from KupovinaOruzja where IDOruzja='" + kuporuzj.IDOruzja + "') UPDATE KupovinaOruzja SET Ime= '" + kuporuzj.Ime + "', Prezime ='" + kuporuzj.Prezime + "', Adresa" +
                            "='" + kuporuzj.Adresa + "', BrojTelefona ='" + kuporuzj.BrojTelefona + "',BrojKartice='"+kuporuzj.BrojKartice+"',PINKartice='"+kuporuzj.PINKartice+"',JMBG='"+kuporuzj.JMBG+"' WHERE IDOruzja = '" + kuporuzj.IDOruzja + "'", con);
                        comoruzje.ExecuteNonQuery();
                        Response.Write("<script>alert('Uspesno izmenjeno oruzje!');</script>");
                    }
                }

                else
                {


                   dbKupi.KupovinaOruzjas.Add(kuporuzj);
                    dbKupi.SaveChanges();
                    Response.Write("<script>alert('Uspesno uneto oruzje!');</script>");
                    ModelState.Clear();
                }
                con.Close();
                
                return View("KupiOruzje");
                
            return View("KupiOruzje");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult KupiOruzjeList()
        {
            var res = dbKupi.KupovinaOruzjas.ToList();
            return View(res);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteOruzje(int IDOruzja)
        {
            var res = dbKupi.KupovinaOruzjas.Where(x => x.IDOruzja == IDOruzja).First();
            dbKupi.KupovinaOruzjas.Remove(res);
            dbKupi.SaveChanges();
            var list = dbKupi.KupovinaOruzjas.ToList();
            Response.Write("<script>alert('Uspesno obrisano oruzje!');</script>");
            return View("KupiOruzjeList", list);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult UpdateOruzje()
        {
       
            return View("KupiOruzje");
        }
        [Authorize(Roles = "User,Admin")]
        public ActionResult KupiMuniciju()
        {
            string imekorisnika = HttpContext.User.Identity.Name.ToString();

            var r = dbKupi.KorisnickiPodacis.Where(x => x.KorisnickoIme == imekorisnika).First();
            ViewBag.KorisnickoIme = r.KorisnickoIme;
            ViewBag.Ime = r.Ime;
            ViewBag.Prezime = r.Prezime;
            ViewBag.BrojTelefona = r.BrojTelefona;
            ViewBag.JMBG = r.JMBG;
            ViewBag.Adresa = r.Adresa;
           
            return View();
        }
        [Authorize(Roles ="User,Admin")]
        public ActionResult UbaciKupiMuniciju(KupovinaMunicije sklad)
        {
            
            

                KupovinaMunicije kupmun = new KupovinaMunicije();
                con.Open();
            if (String.IsNullOrEmpty(sklad.Ime) || String.IsNullOrEmpty(sklad.Prezime)||String.IsNullOrEmpty(sklad.Adresa)||(sklad.BrojTelefona==null)||(sklad.BrojKartice==null)||(sklad.PINKartice==null)||(sklad.JMBG==null)||(sklad.IDMunicije==null))
            {
                Response.Write("<script>alert('Niste uneli sva polja!');</script>");
                return View("KupiMuniciju");
            }
                else if (ModelState.IsValid)
                {
                    kupmun.Ime = sklad.Ime;
                    kupmun.Prezime = sklad.Prezime;
                    kupmun.Adresa = sklad.Adresa;
                    kupmun.BrojTelefona = sklad.BrojTelefona;
                    kupmun.BrojKartice = sklad.BrojKartice;
                    kupmun.PINKartice = sklad.PINKartice;
                    kupmun.JMBG = sklad.JMBG;
                    kupmun.IDMunicije=sklad.IDMunicije;


                    var provera = dbKupi.KupovinaMunicijes.Where(x => x.IDMunicije == sklad.IDMunicije).FirstOrDefault();
                    if (provera != null)
                    {
                        using (con)
                        {

                            SqlCommand commun = new SqlCommand("IF EXISTS(Select * from KupovinaMunicije where IDOruzja='" + kupmun.IDMunicije + "') UPDATE Oruzjee SET Ime= '" + kupmun.Ime + "', Prezime ='" + kupmun.Prezime + "', Adresa" +
                                "='" + kupmun.Adresa + "', BrojTelefona ='" + kupmun.BrojTelefona + "',BrojKartice='" + kupmun.BrojKartice + "',PINKartice='" + kupmun.PINKartice + "',JMBG='" + kupmun.JMBG + "' WHERE IDMunicije = '" + kupmun.IDMunicije + "'", con);
                            commun.ExecuteNonQuery();
                            Response.Write("<script>alert('Uspesno izmenjena kupovina!');</script>");
                        }
                    }

                    else
                    {


                        dbKupi.KupovinaMunicijes.Add(kupmun);
                        dbKupi.SaveChanges();
                        Response.Write("<script>alert('Uspesno kupljena municija!');</script>");
                        ModelState.Clear();
                    }
                    con.Close();

                    return View("KupiMuniciju");
                }
                return View("KupiMuniciju");
            
        }
        [Authorize(Roles ="Admin")]
        public ActionResult KupiMunicijuList()
        {
            var res = dbKupi.KupovinaMunicijes.ToList();
            return View(res);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteMunicija(int IDMunicije)
        {
            var res = dbKupi.KupovinaMunicijes.Where(x => x.IDMunicije == IDMunicije).First();
            dbKupi.KupovinaMunicijes.Remove(res);
            dbKupi.SaveChanges();
            var list = dbKupi.KupovinaMunicijes.ToList();
            Response.Write("<script>alert('Uspesno obrisana municija!');</script>");
            return View("KupiMunicijuList", list);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult UpdateMunicija()
        {

            return View("KupiMuniciju");
        }
    }
}