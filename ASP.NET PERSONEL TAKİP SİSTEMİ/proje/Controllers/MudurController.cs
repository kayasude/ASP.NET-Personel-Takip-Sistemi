using EntitiyLayer.Concrete.Entitiy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace proje.Controllers
{
    public class MudurController : Controller
    {
        // GET: Mudur
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult mudurbilgisi()
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
            var degerler = db.kullanıcı.ToList();
            return View(degerler);
        }

        [HttpGet]
        public ActionResult mudurekle()
        {
            string kAdi = TempData["kAdi"] as string;
            int kID = Convert.ToInt32(TempData["kID"]);
            string tip = TempData["tip"] as string;



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
            
            return View();
        }


        [HttpPost]
        public ActionResult mudurekle(kullanıcı p1)
        {
            int kID = Convert.ToInt32(TempData["kID"]);
            string tip = TempData["tip"] as string;
            string kAdi = TempData["kAdi"] as string;
          



            
             if (kAdi == "yönetici")
            {
                ViewBag.AllowAllTables = true;
                TempData.Keep("kAdi");


            }
            personel_takip_otomasyonuEntities db = new personel_takip_otomasyonuEntities();


            db.kullanıcı.Add(p1);
            db.SaveChanges();
            var logMessage = $"{kAdi} tarafından kayıt ekleme işlemi gerçekleştirildi. Eklenen kayıt ID: {p1.kullanıcıID}";


            var log = new Log
            {
                mesaj = logMessage,
                seviye = tip,
                zaman = DateTime.Now,
                kullanıcıID = kID
            };
            db.Log.Add(log);
            db.SaveChanges();
            TempData.Keep("kAdi");
            TempData.Keep("tip");
            TempData.Keep("kID");

            return View("firmabilgisi");
        }

        public ActionResult mudurgetir(int id)

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


            var personel = db.kullanıcı.FirstOrDefault(p => p.kullanıcıID == id);


            return View("mudurgetir", personel);






        }


    }

}
