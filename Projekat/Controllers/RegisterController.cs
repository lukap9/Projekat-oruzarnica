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
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult RegisterStrana()
        {
            return View();
        }
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-U72BIQ7\\SQLEXPRESS;Initial Catalog=Oruzarnica;Integrated Security=True");
        [HttpPost]
        public ActionResult Register(Logovanje model)
        {
            OruzarnicaEntities dbRegister = new OruzarnicaEntities();
            
                Logovanje reg = new Logovanje();
             
            if (ModelState.IsValid) 
            {
                reg.KorisnickoIme = model.KorisnickoIme;
                reg.Sifra = model.Sifra;
                var proveraa = dbRegister.Logovanjes.Where(x => x.KorisnickoIme == model.KorisnickoIme).FirstOrDefault();


                 if (String.IsNullOrEmpty(reg.KorisnickoIme) || String.IsNullOrEmpty(reg.Sifra))
                {
                    Response.Write("<script>alert('Niste uneli korisnicko ime ili sifru. Pokusajte ponovo.');</script>");
                    return View("RegisterStrana");
                }
                else if (proveraa != null)
                {
                    Response.Write("<script>alert('Korisnik sa datim imenom vec postoji. Unesite drugo ime.');</script>");
                    return View("RegisterStrana");
                }
               
                else
                {
                    dbRegister.Logovanjes.Add(reg);
                    dbRegister.SaveChanges();
                    var provera = dbRegister.KorisnickiPodacis.Where(x => x.KorisnickoIme == HttpContext.User.Identity.Name).FirstOrDefault();
                    if (provera == null)
                    {
                        string ime = "";
                        string prezime = "";
                        char brojtelefona = '0';
                        char JMBG = '0';
                        string adresa = " ";
                        con.Open();
                        using (con)
                        {

                            SqlCommand com = new SqlCommand("INSERT INTO KorisnickiPodaci VALUES ('" + model.KorisnickoIme + "','"+ime+"','"+prezime+"','"+brojtelefona+"', '"+JMBG+"','"+adresa+"')"  , con);
                            com.ExecuteNonQuery();
                            
                        }
                        con.Close();
                    }
                    Response.Write("<script>alert('Uspesna registracija!');</script>");
                   
                    Roles.AddUserToRole(model.KorisnickoIme, "User");
                    return View("RegisterStrana");
                }
               
            }
            return View();

        }
        [Authorize(Roles="Owner")]
        public ActionResult AdminRegister()
        {
            return View();
        }
        
        [Authorize(Roles = "Owner")]
        public ActionResult UbaciAdminRegister(Logovanje model)
        {
            
            OruzarnicaEntities dbRegister1 = new OruzarnicaEntities();

            Logovanje reg = new Logovanje();

            if (ModelState.IsValid)
            {
                reg.KorisnickoIme = model.KorisnickoIme;
                reg.Sifra = model.Sifra;
                var proveraa = dbRegister1.Logovanjes.Where(x => x.KorisnickoIme == model.KorisnickoIme).FirstOrDefault();

                if (proveraa != null)
                {
                    Response.Write("<script>alert('Korisnik sa datim imenom vec postoji. Unesite drugo ime.');</script>");
                    return View("RegisterStrana");
                }
                else
                {
                    dbRegister1.Logovanjes.Add(reg);
                    dbRegister1.SaveChanges();
                    Response.Write("<script>alert('Uspesno dodavanje novog admina!');</script>");

                    Roles.AddUserToRole(model.KorisnickoIme, "Admin");
                    return View("RegisterStrana");
                    
                }
                
            }
            return View("RegisterStrana");

        }
    }
}