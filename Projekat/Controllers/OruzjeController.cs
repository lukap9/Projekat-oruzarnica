using Projekat.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Web.Security;

namespace Projekat.Controllers
{
    public class OruzjeController : Controller
    {
        OruzarnicaEntities dbOruzje = new OruzarnicaEntities();
        // GET: Oruzje
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-U72BIQ7\\SQLEXPRESS;Initial Catalog=Oruzarnica;Integrated Security=True");
        [Authorize(Roles = "Owner,Admin")]
        public ActionResult Oruzje(Oruzjee oruzj)
        {
            if (oruzj!=null)         
                return View(oruzj);
            else
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Owner,Admin")]
        public ActionResult DodajOruzje(Oruzjee model)
        {
            Oruzjee oruzj = new Oruzjee();
            con.Open();
            if ((model.IDOruzja == null) || (model.godinaProizvodnje==null) || String.IsNullOrEmpty(model.naziv) || String.IsNullOrEmpty(model.tip) || (model.cena == null))
            {
                Response.Write("<script>alert('Niste uneli sva polja!');</script>");
                return View("Oruzje");
            }
            else if (ModelState.IsValid)
                {

                    oruzj.IDOruzja = model.IDOruzja;
                    oruzj.naziv = model.naziv;
                    oruzj.tip = model.tip;
                    oruzj.godinaProizvodnje = model.godinaProizvodnje;
                    oruzj.cena = model.cena;

                var provera = dbOruzje.Oruzjees.Where(x => x.IDOruzja == model.IDOruzja).FirstOrDefault();
                if (provera != null)
                {
                    using (con)
                    {

                        SqlCommand com = new SqlCommand("IF EXISTS(Select * from Oruzjee where IDOruzja='" + oruzj.IDOruzja + "') UPDATE Oruzjee SET naziv= '" + oruzj.naziv + "', tip ='" + oruzj.tip + "', godina" +
                            "Proizvodnje ='" + oruzj.godinaProizvodnje + "',cena ='" + oruzj.cena + "' WHERE IDOruzja = '" + oruzj.IDOruzja + "'", con);
                        com.ExecuteNonQuery();
                        Response.Write("<script>alert('Uspesno izmenjeno oruzje!');</script>");
                    }
                }

                else
                {


                    dbOruzje.Oruzjees.Add(oruzj);
                    dbOruzje.SaveChanges();
                    Response.Write("<script>alert('Uspesno uneto oruzje!');</script>");
                    ModelState.Clear();
                }
                con.Close();
                
                return View("Oruzje");
                }
                
            return View();
            
        }
        [Authorize(Roles = "Owner,Admin")]
        public ActionResult OruzjeList()
        {

            var res = dbOruzje.Oruzjees.ToList();
            return View(res);
        }
        public ActionResult OruzjeListCustomer()
        {
          
            var res = dbOruzje.Oruzjees.ToList();
            return View(res);
        }
        [Authorize(Roles= "Owner,Admin,User")]
        public ActionResult OruzjeListUser()
        {

            var res = dbOruzje.Oruzjees.ToList();
            return View(res);
        }
        [Authorize(Roles = "Owner,Admin,User")]
        public ActionResult Delete(int IDOruzja)
        {
            var res = dbOruzje.Oruzjees.Where(x => x.IDOruzja == IDOruzja).First();
            dbOruzje.Oruzjees.Remove(res);
            dbOruzje.SaveChanges();
            var list = dbOruzje.Oruzjees.ToList();
            Response.Write("<script>alert('Uspesno obrisano oruzje!');</script>");
            return View("OruzjeList",list);
        }
        [Authorize(Roles= "Owner,Admin")]
        public ActionResult Update(Oruzjee oru)
        {
            return View("Oruzje","Oruzje");
        }
        public ActionResult UpdateOruzje(int IDOruzja)
        {
            var r = dbOruzje.Oruzjees.Where(x => x.IDOruzja==IDOruzja).First();
            ViewBag.IDMunicije = r.IDOruzja;
            ViewBag.naziv = r.naziv;
            ViewBag.godinaProizvodnje = r.godinaProizvodnje;
            ViewBag.cena = r.cena;
            ViewBag.tip = r.tip;
            return View();
        }
    }
 }
