using Fotomaya.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Fotomaya.Controllers
{
    public class AdminLoginController : Controller
    {
        public fotomayaEntities Entities = new fotomayaEntities();
        // GET: AdminLogin
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Admin login)
        {
            if (ModelState.IsValid)
            {
                using (fotomayaEntities entities = new fotomayaEntities())
                {
                    var user = entities.Admin.Where(x => x.admin_email == login.admin_email && x.admin_sifre == login.admin_sifre).SingleOrDefault();
                    if (user != null)
                    {
                        Session["AdminID"] = user.admin_id.ToString();
                        Session["AdminAd"] = user.admin_ad.ToString();
                        Session["AdminEmail"] = user.admin_email.ToString();
                        Session["AdminSifre"] = user.admin_sifre.ToString();

                        Response.Redirect("/Admin/Index");
                    }
                    Session["error"] = "Hatalı kullanıcı adı veya şifre girdiniz! Kontrol edip tekrar deneyin.";
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
            Response.Redirect("~/AdminLogin/Index");

            return View();
        }
    }
}