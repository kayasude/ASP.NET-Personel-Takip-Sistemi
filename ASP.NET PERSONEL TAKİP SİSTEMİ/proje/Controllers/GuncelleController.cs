using EntitiyLayer.Concrete.Entitiy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace proje.Controllers
{
    public class GuncelleController : Controller
    {
        // GET: Guncelle
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult personelguncelle(personel p1)
        {
            personel_takip_otomasyonuEntities db = new personel_takip_otomasyonuEntities();

            var per = db.personel.Find(p1.personelID);
            string kAdi = TempData["kAdi"] as string;
            var logMessage = $"{kAdi} tarafından personel güncelleme işlemi gerçekleştirildi. " +
                $"Güncellenen persolenin ID'si : {p1.personelID} , " +
                $"Güncellenen personelin İlk Adı : {per.ad} " +
                $"Güncellenen personelin Sonraki Adı : {p1.ad} , " +
                $"Güncellenen personelin İlk Soyadı : {per.soyad}  " +
                $"Güncellenen personelin Sonraki Soyadı: {p1.soyad} , " +
                $"Güncellenen personelin İlk T.C.'si : {per.tc}  " +
                $"Güncellenen personelin Sonraki T.C.'si : {p1.tc} , "+
                $"Güncellenen personelin İlk Adresi : {per.adres}  " +
                $"Güncellenen personelin Sonraki Adresi : {p1.adres} , " +
                $"Güncellenen personelin İlk Telefonu : {per.telefon} " +
                $"Güncellenen personelin Sonraki Telefonu: {p1.telefon} , " +
                $"Güncellenen personelin İlk Pozisyonu : {per.pozisyon}  " +
                $"Güncellenen personelin Sonraki Pozisyonu : {p1.pozisyon} , "+
                $"Güncellenen personelin İlk Maaşı : {per.maas}  " +
                $"Güncellenen personelin Sonraki Maaşı : {p1.maas} , ";

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
            TempData.Keep("kAdi");
            TempData.Keep("tip");
            TempData.Keep("kID");



            per.ad = p1.ad;
            per.soyad = p1.soyad;
            per.tc = p1.tc;
            per.adres = p1.adres;
            per.cinsiyet = p1.cinsiyet;
            per.telefon = p1.telefon;
            per.pozisyon = p1.pozisyon;
            per.maas = p1.maas;
            db.SaveChanges();

            

            if (kAdi == "eraykor")
            {
                return RedirectToAction("istanbulsubesi", "Subeler");


            }
            else if (kAdi == "zeynepaldatmaz")
            {
                return RedirectToAction("nevsehirsubesi", "Subeler");


            }
            else if (kAdi == "sudekaya")
            {
                return RedirectToAction("konyasubesi", "Subeler");


            }
            else if (kAdi == "oguzhankilic")
            {
                return RedirectToAction("ankarasubesi", "Subeler");


            }
            else if (kAdi == "yönetici")
            {
                return RedirectToAction("personelbilgisi", "Personel");


            }
            return View();
        }
        public ActionResult subeguncelle(sube p1)
        {
            personel_takip_otomasyonuEntities db = new personel_takip_otomasyonuEntities();

            var per = db.sube.Find(p1.subeID);

            string kAdi = TempData["kAdi"] as string;
            var logMessage = $"{kAdi} tarafından şubenin güncelleme işlemi gerçekleştirildi. " +
                $"Güncellenen şubenin ID'si : {p1.subeID} , " +
                $"Güncellenen şubenin İlk Adı : {per.subeAdi} " +
                $"Güncellenen şubenin Sonraki Adı : {p1.subeAdi} , " +
                $"Güncellenen şubenin İlk Adresi : {per.subeAdresi}  " +
                $"Güncellenen şubenin Sonraki Adresi: {p1.subeAdresi} , " +
                $"Güncellenen şubenin İlk Telefonu : {per.subeTelefonu}  " +
                $"Güncellenen şubenin Sonraki Telefonu : {p1.subeTelefonu} , " +
                $"Güncellenen şubenin İlk Açılış / Kapanış : {per.subeacilis_kapanis}  " +
                $"Güncellenen şubenin Sonraki Açılış / Kapanış : {p1.subeacilis_kapanis} , ";

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
            TempData.Keep("kAdi");
            TempData.Keep("tip");
            TempData.Keep("kID");

            per.subeSehri = p1.subeSehri;
            per.subeAdresi = p1.subeAdresi;
            per.subeAdi= p1.subeAdi;
            per.subeTelefonu= p1.subeTelefonu;
            per.subeacilis_kapanis = p1.subeacilis_kapanis;
            
            db.SaveChanges();

            if (kAdi == "eraykor")
            {
                return RedirectToAction("istanbulsubesi", "Subeler");


            }
            else if (kAdi == "zeynepaldatmaz")
            {
                return RedirectToAction("nevsehirsubesi", "Subeler");


            }
            else if (kAdi == "sudekaya")
            {
                return RedirectToAction("konyasubesi", "Subeler");


            }
            else if (kAdi == "oguzhankilic")
            {
                return RedirectToAction("ankarasubesi", "Subeler");


            }
            else if (kAdi == "yönetici")
            {
                return RedirectToAction("personelbilgisi", "Personel");


            }
            return View();
        }
        public ActionResult subegetir(int id)

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


            var personel = db.sube.FirstOrDefault(p => p.subeID == id);


            return View("subegetir", personel);






        }

        [HttpGet]
        public ActionResult subeekle()

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
        public ActionResult subeekle(sube p1)

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


            db.sube.Add(p1);
            db.SaveChanges();
            var logMessage = $"{kAdi} tarafından sube ekleme işlemi gerçekleştirildi. Eklenen sube ID: {p1.subeID}";


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
        public ActionResult mudurguncelle(kullanıcı p1)
        {
            personel_takip_otomasyonuEntities db = new personel_takip_otomasyonuEntities();

            var per = db.kullanıcı.Find(p1.kullanıcıID);
            string kAdi = TempData["kAdi"] as string;
            var logMessage = $"{kAdi} tarafından personel güncelleme işlemi gerçekleştirildi. " +
                $"Güncellenen Müdürün ID'si : {p1.kullanıcıID} , " +
                $"Güncellenen personelin İlk Kullanıcı Adı : {per.kullanıcıAdı} " +
                $"Güncellenen personelin Sonraki Kullanıcı Adı : {p1.kullanıcıAdı} , " +
                $"Güncellenen personelin İlk Şifresi : {per.sifre}  " +
                $"Güncellenen personelin Sonraki Şifresi: {p1.sifre} , " +
                $"Güncellenen personelin İlk Adı : {per.ad}  " +
                $"Güncellenen personelin Sonraki Adı : {p1.ad} , " +
                $"Güncellenen personelin İlk Soyadı : {per.soyad}  " +
                $"Güncellenen personelin Sonraki Soyadı : {p1.soyad} , " +
                $"Güncellenen personelin İlk Epostası : {per.eposta} " +
                $"Güncellenen personelin Sonraki Epostası: {p1.eposta} , " +
                $"Güncellenen personelin İlk Yetki Seviyesi : {per.yetkiSeviyesi}  " +
                $"Güncellenen personelin Sonraki Yetki Seviyesi : {p1.yetkiSeviyesi} , " ;

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
            TempData.Keep("kAdi");
            TempData.Keep("tip");
            TempData.Keep("kID");



            per.kullanıcıAdı = p1.kullanıcıAdı;
            per.sifre = p1.sifre;
            per.ad = p1.ad;
            per.soyad = p1.soyad;
            per.eposta = p1.eposta;
            per.yetkiSeviyesi = p1.yetkiSeviyesi;
            db.SaveChanges();



            
           return RedirectToAction("mudurbilgisi", "Mudur");


            
        }


    }
}