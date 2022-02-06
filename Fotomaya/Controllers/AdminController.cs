using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fotomaya.Models;

namespace Fotomaya.Controllers
{
    public class AdminController : Controller
    {
        public fotomayaEntities entities = new fotomayaEntities();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Users()
        {
            return View(entities.Kullanici.ToList());
        }

        public ActionResult Admins()
        {
            return View(entities.Admin.ToList());
        }

        public ActionResult SiteAyar()
        {
            return View(entities.Ayar.OrderByDescending(a => a.ayar_id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult SiteAyar(Ayar ayar, HttpPostedFileBase imageUpload)
        {
            if (imageUpload != null)
            {
                Image image = Image.FromStream(imageUpload.InputStream);
                Bitmap bitmap = new Bitmap(image);
                string extension = "/Images/Slider/" + Guid.NewGuid() + Path.GetExtension(imageUpload.FileName);
                bitmap.Save(Server.MapPath(extension));

                ayar.site_arkaplan = extension;
            }

            entities.Ayar.Add(ayar);
            entities.SaveChanges();
            return RedirectToAction("SiteAyar");
        }

        public ActionResult Hizmetler()
        {
            return View(entities.Hizmet.ToList());
        }

        public ActionResult HizmetEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult HizmetEkle(Hizmet hizmet)
        {
            entities.Hizmet.Add(hizmet);
            entities.SaveChanges();
            return RedirectToAction("Hizmetler");
        }

        public ActionResult HizmetDuzenle(int id)
        {
            return View(entities.Hizmet.Where(x => x.hizmet_id == id).First());
        }

        [HttpPost]
        public ActionResult HizmetDuzenle(int id, Hizmet hizmet)
        {
            try
            {
                entities.Entry(hizmet).State = EntityState.Modified;
                entities.SaveChanges();
                Response.Redirect("/Admin/Hizmetler/");
                return View();
            }
            catch
            {
                return View();
            }
        }

        public ActionResult HizmetSil(int id, Hizmet hizmet)
        {
            hizmet = entities.Hizmet.Where(x => x.hizmet_id == id).First();
            entities.Hizmet.Remove(hizmet);
            entities.SaveChanges();
            return RedirectToAction("Hizmetler");
        }

        public ActionResult Hakkimizda()
        {
            return View(entities.Hakkimizda.ToList());
        }

        public ActionResult HakkimizdaEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult HakkimizdaEkle(Hakkimizda hakkimizda)
        {
            entities.Hakkimizda.Add(hakkimizda);
            entities.SaveChanges();
            return RedirectToAction("Hakkimizda");
        }

        public ActionResult HakkimizdaDuzenle(int id)
        {
            return View(entities.Hakkimizda.Where(x => x.hakkimizda_id == id).First());
        }

        [HttpPost]
        public ActionResult HakkimizdaDuzenle(Hakkimizda hakkimizda, int id)
        {
            entities.Entry(hakkimizda).State = EntityState.Modified;
            entities.SaveChanges();
            return RedirectToAction("Hakkimizda");
        }

        public ActionResult HakkimizdaSil(Hakkimizda hakkimizda, int id)
        {
            hakkimizda = entities.Hakkimizda.Where(x => x.hakkimizda_id == id).First();
            entities.Hakkimizda.Remove(hakkimizda);
            entities.SaveChanges();
            return RedirectToAction("Hakkimizda");
        }

        public ActionResult AdminEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdminEkle(Admin admin)
        {
            entities.Admin.Add(admin);
            A_Foto foto = new A_Foto();
            foto.admin_id = admin.admin_id;
            entities.A_Foto.Add(foto);
            entities.SaveChanges();
            return RedirectToAction("Admins");
        }

        public ActionResult AdminDuzenle(int admin_id)
        {
            return View(entities.Admin.Where(x => x.admin_id == admin_id).First());
        }

        [HttpPost]
        public ActionResult AdminDuzenle(int admin_id, Admin admin)
        {
            try
            {
                entities.Entry(admin).State = EntityState.Modified;
                entities.SaveChanges();
                Response.Redirect("/Admin/Admins/");
                return View();
            }
            catch
            {
                return View();
            }
        }

        public ActionResult AdminSil(int id)
        {
            Admin admin = entities.Admin.FirstOrDefault(x => x.admin_id == id);
            entities.Admin.Remove(admin);
            entities.SaveChanges();
            return RedirectToAction("Admins");
        }

        public ActionResult KullaniciEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult KullaniciEkle(Kullanici kullanici)
        {
            entities.Kullanici.Add(kullanici);
            K_Foto foto = new K_Foto();
            foto.kullanici_id = kullanici.kullanici_id;
            entities.K_Foto.Add(foto);
            entities.SaveChanges();
            return RedirectToAction("Users");
        }

        public ActionResult KullaniciDuzenle(int id)
        {
            return View(entities.Kullanici.Where(x => x.kullanici_id == id).First());
        }

        [HttpPost]
        public ActionResult KullaniciDuzenle(int id, Kullanici kullanici)
        {
            try
            {
                entities.Entry(kullanici).State = EntityState.Modified;
                entities.SaveChanges();
                Response.Redirect("/Admin/Users/");
                return View();
            }
            catch
            {
                return View();
            }
        }

        public ActionResult KullaniciSil(int id)
        {
            Kullanici kullanici = entities.Kullanici.FirstOrDefault(x => x.kullanici_id == id);
            entities.Kullanici.Remove(kullanici);
            entities.SaveChanges();
            return RedirectToAction("Users");
        }

        public ActionResult Vesikaliklar()
        {
            return View(entities.Fotograf.ToList().Where(x => x.kategori_id == 1));
        }

        public ActionResult KullaniciVesikalikEkle()
        {
            return View(entities.Kullanici.ToList());
        }

        [HttpPost]
        public ActionResult KullaniciVesikalikEkle(Fotograf foto, HttpPostedFileBase imageUpload)
        {
            if (imageUpload != null)
            {
                Image image = Image.FromStream(imageUpload.InputStream);
                Bitmap bitmap = new Bitmap(image);
                string uzanti = "/Images/Fotograflar/Vesikalik/" + Guid.NewGuid() + Path.GetExtension(imageUpload.FileName);
                bitmap.Save(Server.MapPath(uzanti));

                foto.foto_uzanti = uzanti;
                entities.Fotograf.Add(foto);
                entities.SaveChanges();
            }

            return RedirectToAction("Vesikaliklar");
        }

        public ActionResult Dugun()
        {
            return View(entities.Fotograf.ToList().Where(x => x.kategori_id == 2));
        }

        public ActionResult KullaniciDugunFotoEkle()
        {
            return View(entities.Kullanici.ToList());
        }

        [HttpPost]
        public ActionResult KullaniciDugunFotoEkle(Fotograf foto, HttpPostedFileBase imageUpload)
        {
            if (imageUpload != null)
            {
                Image image = Image.FromStream(imageUpload.InputStream);
                Bitmap bitmap = new Bitmap(image);
                string uzanti = "/Images/Fotograflar/Dugun/" + Guid.NewGuid() + Path.GetExtension(imageUpload.FileName);
                bitmap.Save(Server.MapPath(uzanti));

                foto.foto_uzanti = uzanti;
                entities.Fotograf.Add(foto);
                entities.SaveChanges();
            }

            return RedirectToAction("Dugun");
        }

        public ActionResult Diger()
        {
            return View(entities.Fotograf.ToList().Where(x => x.kategori_id == 3));
        }

        public ActionResult KullaniciDigerFotoEkle()
        {
            return View(entities.Kullanici.ToList());
        }

        [HttpPost]
        public ActionResult KullaniciDigerFotoEkle(Fotograf foto, HttpPostedFileBase imageUpload)
        {
            if (imageUpload != null)
            {
                Image image = Image.FromStream(imageUpload.InputStream);
                Bitmap bitmap = new Bitmap(image);
                string uzanti = "/Images/Fotograflar/Diger/" + Guid.NewGuid() + Path.GetExtension(imageUpload.FileName);
                bitmap.Save(Server.MapPath(uzanti));

                foto.foto_uzanti = uzanti;
                entities.Fotograf.Add(foto);
                entities.SaveChanges();
            }

            return RedirectToAction("Diger");
        }

        public ActionResult HesapAyarlari(int id)
        {
            return View(entities.A_Foto.Where(x => x.Admin.admin_id == id).OrderByDescending(x => x.foto_id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult HesapAyarlari(Admin admin, int id, HttpPostedFileBase imageUpload)
        {
            entities.Entry(admin).State = EntityState.Modified;
            if (imageUpload != null)
            {
                A_Foto foto = new A_Foto();
                Image image = Image.FromStream(imageUpload.InputStream);
                Bitmap bitmap = new Bitmap(image);
                string uzanti = "/Images/AdminProfil/" + Guid.NewGuid() + Path.GetExtension(imageUpload.FileName);
                bitmap.Save(Server.MapPath(uzanti));
                id = Convert.ToInt32(Session["AdminID"]);
                foto.admin_id = id;
                foto.foto_uzanti = uzanti;
                entities.A_Foto.Add(foto);
            }
            entities.SaveChanges();
            Response.Redirect("/Admin/HesapAyarlari/" + id);
            return View();
        }
    }
}