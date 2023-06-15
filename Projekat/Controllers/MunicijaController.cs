using Projekat.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Data.SqlClient;

namespace Projekat.Controllers
{


    public class MunicijaController : Controller
    {
        // GET: Municija
        OruzarnicaEntities dbMunicija = new OruzarnicaEntities();
        // GET: Oruzje
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-U72BIQ7\\SQLEXPRESS;Initial Catalog=Oruzarnica;Integrated Security=True");
        [Authorize(Roles = "Admin,Owner")]
        public ActionResult Municija()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin,Owner")]
        public ActionResult DodajMuniciju(Municija model)
        {
            Municija municij = new Municija();
            if ((model.IDMunicije == null) || (model.godinaProizvodnje == null) || (model.kalibar == null) || (model.brojMetaka == null) || (model.cena == null))
            {
                Response.Write("<script>alert('Niste uneli sva polja!');</script>");
                return View("Municija");
            }
            else if (ModelState.IsValid)
            {
                municij.IDMunicije = model.IDMunicije;
                municij.godinaProizvodnje = model.godinaProizvodnje;
                municij.kalibar = model.kalibar;
                municij.brojMetaka = model.brojMetaka;
                municij.cena = model.cena;

                var provera = dbMunicija.Municijas.Where(x => x.IDMunicije == model.IDMunicije).FirstOrDefault();
                if (provera != null)
                {
                    con.Open();
                    using (con)
                    {

                        SqlCommand com = new SqlCommand("UPDATE Municija SET godinaProizvodnje ='" + municij.godinaProizvodnje + "', kalibar" +
                            "='" + municij.kalibar + "',brojMetaka ='" + municij.brojMetaka + "',cena ='" + municij.cena + "'WHERE IDMunicije = '" + municij.IDMunicije + "'", con);
                        com.ExecuteNonQuery();
                        Response.Write("<script>alert('Uspesno izmenjena municija!');</script>");
                    }
                   
                }
                else
                {
                    dbMunicija.Municijas.Add(municij);
                    dbMunicija.SaveChanges();
                    Response.Write("<script>alert('Uspesno uneta municija!');</script>");
                    ModelState.Clear();
                }

                con.Close();
            }


            return View("Municija");
        }
        
        [Authorize(Roles = "Admin,Owner")]
        public ActionResult MunicijaList()
        {

            var res1 = dbMunicija.Municijas.ToList();
            return View(res1);

        }
        [Authorize(Roles = "Admin,Owner")]
        public ActionResult Delete(int IDMunicije)
        {
            var res = dbMunicija.Municijas.Where(x => x.IDMunicije == IDMunicije).First();
            dbMunicija.Municijas.Remove(res);
            dbMunicija.SaveChanges();
            var list = dbMunicija.Municijas.ToList();
            Response.Write("<script>alert('Uspesno obrisana municija!');</script>");
            return View("MunicijaList", list);
        }
        [HttpPost]
        
        
        public ActionResult MunicijaListCustomer()
        {
            var res1 = dbMunicija.Municijas.ToList();
            return View(res1);
        }
        [Authorize(Roles ="Admin,User,Owner")]
        public ActionResult MunicijaListUser()
        {
            var res1 = dbMunicija.Municijas.ToList();
            return View(res1);
        }
        [Authorize(Roles = "Admin,Owner")]
        public ActionResult Update(Municija oru)
        {
            return View("Municija","Municija");
        }
        
        public ActionResult UpdateMunicija(int IDMunicije)
        {
            var r = dbMunicija.Municijas.Where(x => x.IDMunicije == IDMunicije).First();
            ViewBag.IDMunicije = r.IDMunicije;
            ViewBag.kalibar = r.kalibar;
            ViewBag.godinaProizvodnje = r.godinaProizvodnje;
            ViewBag.cena = r.cena;
            ViewBag.brojMetaka = r.brojMetaka;
            return View();
        }
    }
}
