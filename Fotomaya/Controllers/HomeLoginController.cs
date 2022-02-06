using Fotomaya.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Fotomaya.Controllers
{
    public class HomeLoginController : Controller
    {
        public fotomayaEntities Entities = new fotomayaEntities();
        // GET: HomeLogin
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Kullanici login)
        {
            if (ModelState.IsValid)
            {
                using (fotomayaEntities entities = new fotomayaEntities())
                {
                    var user = entities.Kullanici.Where(x => x.kullanici_email == login.kullanici_email && x.kullanici_sifre == login.kullanici_sifre).SingleOrDefault();
                    if (user != null)
                    {
                        Session["KullaniciID"] = user.kullanici_id.ToString();
                        Session["KullaniciAd"] = user.kullanici_ad.ToString();
                        Session["KullaniciEmail"] = user.kullanici_email.ToString();
                        Session["KullaniciSifre"] = user.kullanici_sifre.ToString();

                        Response.Redirect("/Home/Index");
                    }
                    Session["error"] = "Kullanıcı adı veya şifre hatalı!";
                }
            }
            return View();
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
            FormsAuthentication.SignOut();
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now);
            Response.Redirect("~/HomeLogin/Index");

            return View();
        }

        public ActionResult Kaydol()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Kaydol(Kullanici kullanici)
        {
            using (fotomayaEntities entities = new fotomayaEntities())
            {
                if (kullanici.kullanici_sifre == kullanici.kullanici_sifre && kullanici.kullanici_sifre_tekrar == kullanici.kullanici_sifre_tekrar)
                {
                    entities.Kullanici.Add(kullanici);
                    K_Foto foto = new K_Foto();
                    foto.kullanici_id = kullanici.kullanici_id;
                    entities.K_Foto.Add(foto);
                    entities.SaveChanges();
                    Session["Name"] = kullanici.kullanici_ad;
                    Response.Redirect("/HomeLogin/Index");
                }
                else
                {
                    Session["error"] = "Şifreler eşleşmiyor";
                }
            }
            return View();
        }
    }
}