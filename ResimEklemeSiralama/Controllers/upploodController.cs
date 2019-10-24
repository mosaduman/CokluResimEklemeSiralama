using ResimEklemeSiralama.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ResimEklemeSiralama.Controllers
{
    public class upploodController : Controller
    {
        Context db = new Context();
        // GET: upplood
        public ActionResult Index()
        {
            ViewBag.car_id = Request.QueryString["carid"];
            return View();
        }
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase[] files, int carid)
        {
            ViewBag.car_id = carid;
            if (files.Count() > 25)
            {
                ViewBag.Message = "En fazla 25 Resim Seçebilirsiniz.";
                return View();
            }
            else if (files.Count() >= 1 && files.Count() <= 25)
            {
                try
                {
                    int i = 1;
                    foreach (HttpPostedFileBase file in files)
                    {
                        Models.Image img = new Models.Image();

                        Bitmap Resim = new Bitmap(file.InputStream);
                        Bitmap BoyutlandirilmisResim = new Bitmap(Resim, 1024, 800);
                        string resimAdi = (Guid.NewGuid().ToString("N")) + (Path.GetFileName(file.FileName));
                        BoyutlandirilmisResim.Save(Server.MapPath("~/dosya/" + resimAdi));
                        Resim.Dispose();
                        BoyutlandirilmisResim.Dispose();
                        img.imgCarId = carid;
                        img.imgMain = 2;
                        img.imgAddress = resimAdi;
                        img.imgList = i++;
                        db.Images.Add(img);
                    }
                    db.SaveChanges();
                    ViewBag.Message = "Resimler Yüklendi.";
                    return RedirectToAction("Resim", new { id = carid });


                }
                catch
                {
                    ViewBag.Message = "Bir Hata Oluştu. Resimler Yüklenemedi.";
                }
            }
            else
            {
                ViewBag.Message = "Lütfen Bir Resim Seçiniz";
                return View();
            }
            return View();

        }
        public ActionResult Resim(int id)
        {
            var resim = db.Images.Where(a => a.imgCarId == id).OrderBy(b => b.imgList).ToList();
            return View(resim);
        }
        [HttpPost]
        public JsonResult Resim(List<int> dizi)
        {
            if (dizi != null)
            {
                int i = 1;
                foreach (var item in dizi)
                {
                    var resim = db.Images.Where(a => a.Id == item).SingleOrDefault();
                    resim.imgList = i++;
                }
                db.SaveChanges();
                return Json("Sıralama Başarılı Bir Şekilde Kaydedildi.", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Sıralama Kaydedilemedi.", JsonRequestBehavior.AllowGet);
            }


        }

    }
}