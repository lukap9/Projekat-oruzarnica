using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using Projekat.Context;

namespace Projekat.Controllers

{
    public class IsporuciController : Controller
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-U72BIQ7\\SQLEXPRESS;Initial Catalog=Oruzarnica;Integrated Security=True");
        // GET: Isporuci
        OruzarnicaEntities entity = new OruzarnicaEntities();
        [Authorize(Roles = "Owner,Admin")]
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Owner,Admin")]
        public ActionResult IsporuciOruzje()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Owner,Admin")]
        public ActionResult UbaciIsporuciOruzje(IsporukaOruzja isp)
        {



            IsporukaOruzja isporukaOruzja = new IsporukaOruzja();
            con.Open();
            if ((isp.BrojFakture == null) || (isp.IDOruzja == null) || (isp.cena == null) || String.IsNullOrEmpty(isp.ImeKupca) || String.IsNullOrEmpty(isp.PrezimeKupca) || String.IsNullOrEmpty(isp.AdresaKupca) || (isp.BrojTelefonaKupca == null) || (isp.JMBG == null) || String.IsNullOrEmpty(isp.statusIsporuke))
            {
                Response.Write("<script>alert('Niste uneli sva polja!');</script>");
                return View("Oruzje");
            }
           else if (ModelState.IsValid)
            {
                isporukaOruzja.BrojFakture = isp.BrojFakture;
                isporukaOruzja.VremeIsporuke = DateTime.Now;
                isporukaOruzja.cena = isp.cena;
                isporukaOruzja.ImeKupca = isp.ImeKupca;
                isporukaOruzja.PrezimeKupca = isp.PrezimeKupca;
                isporukaOruzja.AdresaKupca = isp.AdresaKupca;
                isporukaOruzja.BrojTelefonaKupca = isp.BrojTelefonaKupca;
                isporukaOruzja.JMBG = isp.JMBG;
                isporukaOruzja.IDOruzja = isp.IDOruzja;
                isporukaOruzja.statusIsporuke = isp.statusIsporuke;


                var provera = entity.IsporukaOruzjas.Where(x => x.BrojFakture == isp.BrojFakture).FirstOrDefault();
                if (provera != null)
                {
                    using (con)
                    {

                        SqlCommand commun = new SqlCommand("IF EXISTS(Select * from IsporukaOruzja where BrojFakture='" + isporukaOruzja.BrojFakture + "') UPDATE IsporukaOruzja SET VremeIsporuke= '" + isporukaOruzja.VremeIsporuke + "', ImeKupca ='" + isporukaOruzja.ImeKupca+ "', PrezimeKupca" +
                            "='" + isporukaOruzja.PrezimeKupca + "', AdresaKupca ='" + isporukaOruzja.AdresaKupca + "',BrojTelefonaKupca='" + isporukaOruzja.BrojTelefonaKupca + "',JMBG='" + isporukaOruzja.JMBG + "',IDOruzja='" + isporukaOruzja.IDOruzja + "',statusIsporuke='"+isporukaOruzja.statusIsporuke +"' WHERE BrojFakture = '" + isporukaOruzja.BrojFakture + "'", con);
                        commun.ExecuteNonQuery();
                        Response.Write("<script>alert('Uspesno izmenjena isporuka!');</script>");
                    }
                }

                else
                {


                    entity.IsporukaOruzjas.Add(isporukaOruzja);
                    entity.SaveChanges();
                    Response.Write("<script>alert('Uspesno uneta isporuka!');</script>");
                    ModelState.Clear();
                }
                con.Close();

                return View("IsporuciOruzje");
            }
            return View("IsporuciOruzje");

        }
        [Authorize(Roles = "Owner,Admin")]
        public ActionResult UpdateIsporukaOruzja()
        {
            return View("IsporuciOruzje");
        }
        [Authorize(Roles = "Owner,Admin")]
        public ActionResult IsporuciOruzjeList()
        {
            var res = entity.IsporukaOruzjas.ToList();
            return View(res);
        }
        [Authorize(Roles = "Owner,Admin")]
        public ActionResult DeleteIsporukaOruzja(int BrojFakture)
        {
            var res = entity.IsporukaOruzjas.Where(x => x.BrojFakture == BrojFakture).First();
            entity.IsporukaOruzjas.Remove(res);
            entity.SaveChanges();
            var list = entity.IsporukaOruzjas.ToList();
            Response.Write("<script>alert('Uspesno obrisana isporuka!');</script>");
            return View("IsporuciOruzjeList", list);
        }
        [Authorize(Roles = "Owner,Admin")]
        public ActionResult IsporuciMuniciju()
        {
            return View();
        }
        [Authorize(Roles ="Owner,Admin")]
        public ActionResult UbaciIsporuciMuniciju(IsporukaMunicije isp1)
        {
            IsporukaMunicije isporukaMunicije = new IsporukaMunicije();
            con.Open();
            if ((isp1.BrojFakture == null) || (isp1.IDMunicije == null) || (isp1.cena == null) || String.IsNullOrEmpty(isp1.ImeKupca) || String.IsNullOrEmpty(isp1.PrezimeKupca) || String.IsNullOrEmpty(isp1.AdresaKupca)||(isp1.BrojTelefonaKupca==null)||(isp1.JMBG==null)||String.IsNullOrEmpty(isp1.statusIsporuke))
            {
                Response.Write("<script>alert('Niste uneli sva polja!');</script>");
                return View("Oruzje");
            }
            else if (ModelState.IsValid)
            {
                isporukaMunicije.BrojFakture = isp1.BrojFakture;
                isporukaMunicije.VremeIsporuke = DateTime.Now;
                isporukaMunicije.cena = isp1.cena;
                isporukaMunicije.ImeKupca = isp1.ImeKupca;
                isporukaMunicije.PrezimeKupca = isp1.PrezimeKupca;
                isporukaMunicije.AdresaKupca = isp1.AdresaKupca;
                isporukaMunicije.BrojTelefonaKupca = isp1.BrojTelefonaKupca;
                isporukaMunicije.JMBG = isp1.JMBG;
                isporukaMunicije.IDMunicije = isp1.IDMunicije;
                isporukaMunicije.statusIsporuke = isp1.statusIsporuke;


                var provera = entity.IsporukaMunicijes.Where(x => x.BrojFakture == isp1.BrojFakture).FirstOrDefault();
                if (provera != null)
                {
                    using (con)
                    {

                        SqlCommand commun = new SqlCommand("IF EXISTS(Select * from IsporukaMunicije where BrojFakture='" + isporukaMunicije.BrojFakture + "') UPDATE IsporukaMunicije SET VremeIsporuke= '" + isporukaMunicije.VremeIsporuke + "', ImeKupca ='" + isporukaMunicije.ImeKupca + "', PrezimeKupca" +
                            "='" + isporukaMunicije.PrezimeKupca + "', AdresaKupca ='" + isporukaMunicije.AdresaKupca + "',BrojTelefonaKupca='" + isporukaMunicije.BrojTelefonaKupca + "',JMBG='" + isporukaMunicije.JMBG + "',IDMunicije='" + isporukaMunicije.IDMunicije + "',statusIsporuke='" + isporukaMunicije.statusIsporuke + "' WHERE BrojFakture = '" + isporukaMunicije.BrojFakture + "'", con);
                        commun.ExecuteNonQuery();
                        Response.Write("<script>alert('Uspesno izmenjena isporuka!');</script>");
                    }
                }

                else
                {


                    entity.IsporukaMunicijes.Add(isporukaMunicije);
                    entity.SaveChanges();
                    Response.Write("<script>alert('Uspesno uneta isporuka!');</script>");
                    ModelState.Clear();
                }
                con.Close();

                return View("IsporuciMuniciju");
            }
            return View("IsporuciMuniciju");
        }
        [Authorize(Roles = "Owner,Admin")]
        public ActionResult IsporuciMunicijuList()
        {
            var res = entity.IsporukaMunicijes.ToList();
            return View(res);
        }
        [Authorize(Roles = "Owner,Admin")]
        public ActionResult UpdateIsporuciMuniciju(int BrojFakture)
        {
            var r = entity.IsporukaMunicijes.Where(x => x.BrojFakture == BrojFakture).First();
            ViewBag.VremeIsporuke = r.VremeIsporuke;
            ViewBag.cena = r.cena;
            ViewBag.ImeKupca = r.ImeKupca;
            ViewBag.PrezimeKupca = r.PrezimeKupca;
            ViewBag.AdresaKupca = r.AdresaKupca;
            ViewBag.BrojTelefonaKupca = r.BrojTelefonaKupca;
            ViewBag.JMBG = r.JMBG;
            ViewBag.IDMunicije = r.IDMunicije;
            ViewBag.statusIsporuke = r.statusIsporuke;
            return View();
        }
        [Authorize(Roles = "Owner,Admin")]
        public ActionResult UpdateIsporuciOruzje(int BrojFakture)
        {
            var r = entity.IsporukaOruzjas.Where(x => x.BrojFakture == BrojFakture).First();
            ViewBag.VremeIsporuke = r.VremeIsporuke;
            ViewBag.cena = r.cena;
            ViewBag.ImeKupca = r.ImeKupca;
            ViewBag.PrezimeKupca = r.PrezimeKupca;
            ViewBag.AdresaKupca = r.AdresaKupca;
            ViewBag.BrojTelefonaKupca = r.BrojTelefonaKupca;
            ViewBag.JMBG = r.JMBG;
            ViewBag.IDOruzja = r.IDOruzja;
            ViewBag.statusIsporuke = r.statusIsporuke;
            return View();
        }
        [Authorize(Roles = "Owner,Admin")]
        public ActionResult DeleteIsporukaMunicije(int BrojFakture)
        {
            var res = entity.IsporukaMunicijes.Where(x => x.BrojFakture == BrojFakture).First();
            entity.IsporukaMunicijes.Remove(res);
            entity.SaveChanges();
            var list = entity.IsporukaMunicijes.ToList();
            Response.Write("<script>alert('Uspesno obrisana isporuka!');</script>");
            return View("IsporuciMunicijuList", list);
        }
    }
}