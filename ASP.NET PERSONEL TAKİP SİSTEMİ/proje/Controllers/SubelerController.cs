using EntitiyLayer.Concrete.Entitiy;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace proje.Controllers
{
    public class SubelerController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult subebilgisi()
        {
            string kAdi = TempData["kAdi"] as string;


            if (kAdi == "eraykor")
            {
                ViewBag.AllowIstanbulTable = true;
                TempData.Keep("kAdi");

            }
            else if (kAdi == "zeynepaldatmaz")
            {
                ViewBag.AllowNevsehirTable = true;
                TempData.Keep("kAdi");

            }
            else if (kAdi == "sudekaya")
            {
                ViewBag.AllowKonyaTable = true;
                TempData.Keep("kAdi");

            }
            else if (kAdi == "oguzhankilic")
            {
                ViewBag.AllowAnkaraTable = true;
                TempData.Keep("kAdi");

            }
            else if (kAdi == "yönetici")
            {
                ViewBag.AllowAllTables = true; 
                TempData.Keep("kAdi");

            }
            personel_takip_otomasyonuEntities db = new personel_takip_otomasyonuEntities();
            var degerler = db.sube.ToList();
            return View(degerler);
        }
        public ActionResult istanbulsubesi()
        {
            

            string kAdi = TempData["kAdi"] as string;
            if (kAdi == "eraykor")
            {
                ViewBag.AllowIstanbulTable = true;
                TempData.Keep("kAdi");

            }
            else if (kAdi == "yönetici")
            {
                ViewBag.AllowAllTables = true;
                TempData.Keep("kAdi");
            }
            personel_takip_otomasyonuEntities db = new personel_takip_otomasyonuEntities();
            var degerler = db.personel.ToList();
            return View(degerler);

        }
        public ActionResult ankarasubesi()
        {


            string kAdi = TempData["kAdi"] as string;
            if (kAdi == "oguzhankilic")
            {
                ViewBag.AllowIstanbulTable = true;
                TempData.Keep("kAdi");

            }
            else if (kAdi == "yönetici")
            {
                ViewBag.AllowAllTables = true;
                TempData.Keep("kAdi");
            }
            personel_takip_otomasyonuEntities db = new personel_takip_otomasyonuEntities();
            var degerler = db.personel.ToList();
            return View(degerler);

        }
        public ActionResult nevsehirsubesi()
        {


            string kAdi = TempData["kAdi"] as string;
            if (kAdi == "zeynepaldatmaz")
            {
                ViewBag.AllowIstanbulTable = true;
                TempData.Keep("kAdi");

            }
            else if (kAdi == "yönetici")
            {
                ViewBag.AllowAllTables = true;
                TempData.Keep("kAdi");
            }
            personel_takip_otomasyonuEntities db = new personel_takip_otomasyonuEntities();
            var degerler = db.personel.ToList();
            return View(degerler);

        }
        public ActionResult konyasubesi ()
        {


            string kAdi = TempData["kAdi"] as string;
            if (kAdi == "sudekaya")
            {
                ViewBag.AllowIstanbulTable = true;
                TempData.Keep("kAdi");

            }
            else if (kAdi == "yönetici")
            {
                ViewBag.AllowAllTables = true;
                TempData.Keep("kAdi");
            }
            personel_takip_otomasyonuEntities db = new personel_takip_otomasyonuEntities();
            var degerler = db.personel.ToList();
            return View(degerler);

        }

        public ActionResult subesil(int id)

        {
            string kAdi = TempData["kAdi"] as string;


            if (kAdi == "eraykor")
            {
                ViewBag.AllowIstanbulTable = true;
                TempData.Keep("kAdi");

            }
            else if (kAdi == "zeynepaldatmaz")
            {
                ViewBag.AllowNevsehirTable = true;
                TempData.Keep("kAdi");

            }
            else if (kAdi == "sudekaya")
            {
                ViewBag.AllowKonyaTable = true;
                TempData.Keep("kAdi");

            }
            else if (kAdi == "oguzhankilic")
            {
                ViewBag.AllowAnkaraTable = true;
                TempData.Keep("kAdi");

            }
            else if (kAdi == "yönetici")
            {
                ViewBag.AllowAllTables = true;
                TempData.Keep("kAdi");

            }
            personel_takip_otomasyonuEntities db = new personel_takip_otomasyonuEntities();


            var sube = db.sube.FirstOrDefault(p => p.subeID == id);
            var logMessage = $"{kAdi} tarafından personel silme işlemi gerçekleştirildi. " +
                $"Silinen subenin ID'si : {id} , " +
                $"Silinen subenin Adı : {sube.subeAdi}," +
                $"Silinen subenin Adresi : {sube.subeAdresi}," +
                $"Silinen subenin Sehri : {sube.subeSehri}," +
                $"Silinen subenin Telefonu : {sube.subeTelefonu}," +
                $"Silinen subenin Açılış / Kapanışı : {sube.subeacilis_kapanis},";

            int kID = Convert.ToInt32(TempData["kID"]);
            string tip = TempData["tip"] as string;

            var log = new Log
            {
                mesaj = logMessage,
                seviye = tip,
                zaman = DateTime.Now,
                kullanıcıID = kID
            };
            TempData.Keep("kAdi");
            TempData.Keep("tip");
            TempData.Keep("kID");

            db.Log.Add(log);
            db.SaveChanges();

            db.sube.Remove(sube);
            db.SaveChanges();
           


            return Json(new { success = true });






        }

    }
}