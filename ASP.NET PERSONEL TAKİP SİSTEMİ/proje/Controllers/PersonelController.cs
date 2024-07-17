using EntitiyLayer.Concrete.Entitiy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace proje.Controllers
{
    public class PersonelController : Controller
    {
        // GET: Personel
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult personelbilgisi()
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
            var degerler = db.personel.ToList();
            return View(degerler);
        }
       
        public ActionResult personelgetir(int id)

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

           
                var personel = db.personel.FirstOrDefault(p => p.personelID == id);

                  
                    return View("personelgetir",personel); 
                
            
            



        }
        [HttpGet]
        public ActionResult personelekle()

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
            return View();
        }
        [HttpPost]
        public ActionResult personelekle(personel p1 )

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
            personel_takip_otomasyonuEntities db = new personel_takip_otomasyonuEntities();


            db.personel.Add(p1);
            db.SaveChanges();
            var logMessage = $"{kAdi} tarafından kayıt ekleme işlemi gerçekleştirildi. Eklenen kayıt ID: {p1.personelID}";

           
            var log = new Log
            {
                mesaj = logMessage,
                seviye = tip,
                zaman = DateTime.Now,
                kullanıcıID= kID
            };
            db.Log.Add(log);
            db.SaveChanges();
            TempData.Keep("kAdi");
            TempData.Keep("tip");
            TempData.Keep("kID");

            return View("firmabilgisi");






        }

        public ActionResult personelsil(int id)

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


            var personel = db.personel.FirstOrDefault(p => p.personelID == id);
            var logMessage = $"{kAdi} tarafından personel silme işlemi gerçekleştirildi. " +
              $"Silinen persolenin ID'si : {id} , " +
              $"Silinen personelin Adı : {personel.ad}," +
              $"Silinen personelin Soyadı : {personel.soyad}," +
              $"Silinen personelin T.C.'si : {personel.tc},";

            int kID = Convert.ToInt32(TempData["kID"]);
            string tip = TempData["tip"] as string;

            var log = new Log
            {
                mesaj = logMessage,
                seviye = tip,
                zaman = DateTime.Now,
                kullanıcıID = kID
            };
            db.Log.Add(log);
            db.SaveChanges();

            db.personel.Remove(personel);
            db.SaveChanges();
            TempData.Keep("kAdi");
            TempData.Keep("tip");
            TempData.Keep("kID");


            return Json(new { success = true });






        }




       
        // Yükleme formundan gelen veriyi işleyin ve veritabanına kaydedin
        
       
        [HttpGet]
        public ActionResult FotografYukle()
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


            return View();
        }

        [HttpPost]
        public ActionResult FotografYukle(int id, HttpPostedFileBase file)
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



            var foto = db.personel.Find(id);
            if (file != null && file.ContentLength > 0 && file.ContentType.StartsWith("image/"))
            {
                try
                {
                    // Dosyayı MemoryStream'e yükle
                    using (var ms = new MemoryStream())
                    {
                        file.InputStream.CopyTo(ms);
                        byte[] imageData = ms.ToArray();

                        // Base64'e dönüştür ve veritabanına kaydet
                        string base64Image = Convert.ToBase64String(imageData);

                        foto.fotograf = base64Image;
                        db.SaveChanges();

                    }
                    return RedirectToAction("DosyaGetir", "PersonelBilgi", new { id });

                }
                catch (Exception ex) { }

                return RedirectToAction("DosyaGetir", "PersonelBilgi", new { id });



            }
            return View();
        }


        public ActionResult FotografSil(int id)
        {

            personel_takip_otomasyonuEntities db = new personel_takip_otomasyonuEntities();


            var personel = db.personel.FirstOrDefault(p => p.personelID == id);
            personel.fotograf = null;
            return RedirectToAction("DosyaGetir", "PersonelBilgi", new { id });
        }
    }
}

