using Fotomaya.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fotomaya.Controllers
{
    public class HomeController : Controller
    {
        public fotomayaEntities entities = new fotomayaEntities();
        // GET: Home
        public ActionResult Index()
        {
            return View(entities.Ayar.OrderByDescending(a => a.ayar_id).FirstOrDefault());
        }

        public ActionResult About()
        {
            return View(entities.Hakkimizda.ToList());
        }

        public ActionResult Services()
        {
            return View(entities.Hizmet.ToList());
        }

        public ActionResult Contact()
        {
            return View(entities.Ayar.OrderByDescending(x => x.ayar_id).FirstOrDefault());
        }

        public ActionResult KullaniciFoto(int id)
        {
            ViewBag.FotoDurum = entities.FotoDurum.ToList();
            return View(entities.Fotograf.ToList().Where(x => x.kullanici_id == id));
        }

        [HttpPost]
        public ActionResult KullaniciFoto(Fotograf foto, int id, int foto_id, string foto_durum)
        {

            entities.Entry(foto).State = EntityState.Modified;
            foto.FotoDurum.foto_durum = entities.FotoDurum.FirstOrDefault(x => x.foto_durum == foto_durum).foto_durum;
            entities.SaveChanges();
            Response.Redirect("/Home/KullaniciFoto/" + id);

            return View();
        }

        public ActionResult DurumDegistir(int foto_id)
        {
            ViewBag.FotoDurum = entities.FotoDurum.ToList();
            return View(entities.Fotograf.Where(x => x.foto_id == foto_id).First());
        }

        [HttpPost]
        public ActionResult DurumDegistir(Fotograf foto, int foto_id, int foto_durum_id, int kullanici_id)
        {
            entities.Entry(foto).State = EntityState.Modified;
            foto.foto_durum_id = entities.FotoDurum.FirstOrDefault(x => x.foto_durum_id == foto_durum_id).foto_durum_id;
            entities.SaveChanges();
            Response.Redirect("/Home/KullaniciFoto/" + kullanici_id);
            return View();
        }

        public ActionResult Hesabim(int id)
        {
            return View(entities.K_Foto.Where(x => x.Kullanici.kullanici_id == id).OrderByDescending(x => x.foto_id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Hesabim(Kullanici kullanici, HttpPostedFileBase imageUpload, int id)
        {
            entities.Entry(kullanici).State = EntityState.Modified;
            if (imageUpload != null)
            {
                K_Foto foto = new K_Foto();
                Image image = Image.FromStream(imageUpload.InputStream);
                Bitmap bitmap = new Bitmap(image);
                string uzanti = "/Images/AdminProfil/" + Guid.NewGuid() + Path.GetExtension(imageUpload.FileName);
                bitmap.Save(Server.MapPath(uzanti));
                id = Convert.ToInt32(Session["KullaniciID"]);
                foto.kullanici_id = id;
                foto.foto_uzanti = uzanti;
                entities.K_Foto.Add(foto);
            }
            entities.SaveChanges();
            Response.Redirect("/Home/Hesabim/" + id);
            return View();
        }
    }
}