using EntitiyLayer.Concrete.Entitiy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebGrease.Activities;

namespace proje.Controllers
{
    public class PersonelBilgiController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }



        //SİCİL RAPORU

        [HttpGet]
        public ActionResult sicilYukle()
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
        public ActionResult SicilYukle(int id, HttpPostedFileBase sicilDosyasi, string sicilDosyaAdi)
        {
            string kAdi = TempData["kAdi"] as string;
            string ID = TempData["Id"] as string;

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




            if (sicilDosyasi != null && sicilDosyasi.ContentLength > 0)
            {
                try
            {
                // ID'ye karşılık gelen kaydı bulun
                var personel = db.personel.Find(id);

                if (personel != null)
                {
                    // Dosya veritabanına eklenir
                    byte[] fileBytes;
                    using (var inputStream = sicilDosyasi.InputStream)
                    {
                        fileBytes = new byte[inputStream.Length];
                        inputStream.Read(fileBytes, 0, fileBytes.Length);
                    }

                    // Personel nesnesinin 'Dosya' özelliğine byte dizisini atayın
                    personel.sicilDosyasi = fileBytes;
                        personel.sicilDosyaAdi = sicilDosyaAdi;

                        // Değişiklikleri kaydet
                        db.SaveChanges();

                    ViewBag.Message = "Dosya başarıyla yüklendi.";
                }
                else
                {
                    ViewBag.Message = "ID'ye karşılık gelen kayıt bulunamadı.";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Dosya yüklenirken bir hata oluştu: " + ex.Message;
            }
            }
            else
            {
                ViewBag.Message = "Lütfen bir dosya seçin.";
            }


            TempData.Keep("ID");
            TempData.Keep("Id");

            var logMessage = $"{kAdi} tarafından personel dosya ekleme işlemi gerçekleştirildi. "+
                 $"Eklenen Dosya Adı : {sicilDosyaAdi},"+
                  $"Eklenen ID : {id}";

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
            TempData.Keep("tip");
            TempData.Keep("kID");
            return RedirectToAction("DosyaGetir", "PersonelBilgi", new { id });





        }

        public ActionResult DosyaGetir( int id)
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
            ViewBag.Id = id;

            return View("DosyaGetir", personel);




        }

        public ActionResult Dosyaİndir(int id)
        {
            personel_takip_otomasyonuEntities db = new personel_takip_otomasyonuEntities();

            // ID'ye karşılık gelen kaydı bulun
            var personel = db.personel.Find(id);

            if (personel != null && personel.sicilDosyasi != null)
            {
                // Dosya ismini alın (İsteğe bağlı)
                string fileName = personel.sicilDosyaAdi + ".pdf";
                // Dosyayı kullanıcıya sunmak için FileResult kullanın
                return File(personel.sicilDosyasi, "application/pdf", fileName);
            }
            else
            {
                // Eğer kayıt bulunamazsa veya dosya yoksa hata döndürün (İsteğe bağlı)
                return HttpNotFound("Dosya bulunamadı.");
            }
        }
        public ActionResult DosyaSil(int id)
        {
            personel_takip_otomasyonuEntities db = new personel_takip_otomasyonuEntities();
            var personel = db.personel.FirstOrDefault(p => p.personelID == id);
            string kAdi = TempData["kAdi"] as string;

            var logMessage = $"{kAdi} tarafından personel dosya silme işlemi gerçekleştirildi. " +
                $"Silinen Dosya Adi {personel.sicilDosyaAdi}. " +
                $"Silinen ID {id}. "
                ;

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
            TempData.Keep("tip");
            TempData.Keep("kID");

            personel.sicilDosyasi = null;
            personel.sicilDosyaAdi = null;
            db.SaveChanges();

            TempData.Keep("ID");
            TempData.Keep("Id");
           
            return RedirectToAction("DosyaGetir", "PersonelBilgi", new { id });
        }


        //İKAMET RAPORU



        [HttpGet]
        public ActionResult ikametYukle()
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
        public ActionResult ikametYukle(int id, HttpPostedFileBase ikametDosyasi, string ikametDosyaAdi)
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

            if (ikametDosyasi != null && ikametDosyasi.ContentLength > 0)
            {
                try
                {
                    // ID'ye karşılık gelen kaydı bulun
                    var personel = db.personel.Find(id);

                    if (personel != null)
                    {
                        // Dosya veritabanına eklenir
                        byte[] fileBytes;
                        using (var inputStream = ikametDosyasi.InputStream)
                        {
                            fileBytes = new byte[inputStream.Length];
                            inputStream.Read(fileBytes, 0, fileBytes.Length);
                        }

                        // Personel nesnesinin 'Dosya' özelliğine byte dizisini atayın
                        personel.ikametDosyasi = fileBytes;
                        personel.ikametDosyaAdi = ikametDosyaAdi;

                        // Değişiklikleri kaydet
                        db.SaveChanges();

                        ViewBag.Message = "Dosya başarıyla yüklendi.";
                    }
                    else
                    {
                        ViewBag.Message = "ID'ye karşılık gelen kayıt bulunamadı.";
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Dosya yüklenirken bir hata oluştu: " + ex.Message;
                }
            }
            else
            {
                ViewBag.Message = "Lütfen bir dosya seçin.";
            }

            var logMessage = $"{kAdi} tarafından personel dosya ekleme işlemi gerçekleştirildi. " +
                 $"Eklenen Dosya Adı : {ikametDosyaAdi}," +
                  $"Eklenen ID : {id}";

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
            TempData.Keep("tip");
            TempData.Keep("kID");


            return RedirectToAction("DosyaGetir", "PersonelBilgi", new { id });






        }

        public ActionResult DosyaIndirIkamet(int id)
        {
            personel_takip_otomasyonuEntities db = new personel_takip_otomasyonuEntities();

            // ID'ye karşılık gelen kaydı bulun
            var personel = db.personel.Find(id);

            if (personel != null && personel.ikametDosyasi != null)
            {
                // Dosya ismini alın (İsteğe bağlı)
                string fileName = personel.ikametDosyaAdi + ".pdf";
                // Dosyayı kullanıcıya sunmak için FileResult kullanın
                return File(personel.ikametDosyasi, "application/pdf", fileName);
            }
            else
            {
                // Eğer kayıt bulunamazsa veya dosya yoksa hata döndürün (İsteğe bağlı)
                return HttpNotFound("Dosya bulunamadı.");
            }
        }
        public ActionResult DosyaSilİkamet(int id)
        {
            personel_takip_otomasyonuEntities db = new personel_takip_otomasyonuEntities();
            var personel = db.personel.FirstOrDefault(p => p.personelID == id);

            string kAdi = TempData["kAdi"] as string;

            var logMessage = $"{kAdi} tarafından personel dosya silme işlemi gerçekleştirildi. " +
                $"Silinen Dosya Adi {personel.ikametDosyaAdi}. " +
                $"Silinen ID {id}. "
                ;

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
            TempData.Keep("tip");
            TempData.Keep("kID");

            personel.ikametDosyasi = null;
            personel.ikametDosyaAdi = null;
            db.SaveChanges();

            
            return RedirectToAction("DosyaGetir", "PersonelBilgi", new { id });
        }






        //SAGLIK RAPORU

        [HttpGet]
        public ActionResult saglikRaporYukle()
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
        public ActionResult saglikRaporYukle(int id, HttpPostedFileBase saglikRaporuDosyasi, string sagliKRaporuDosyasiAdi)
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

            if (saglikRaporuDosyasi != null && saglikRaporuDosyasi.ContentLength > 0)
            {
                try
                {
                    // ID'ye karşılık gelen kaydı bulun
                    var personel = db.personel.Find(id);

                    if (personel != null)
                    {
                        // Dosya veritabanına eklenir
                        byte[] fileBytes;
                        using (var inputStream = saglikRaporuDosyasi.InputStream)
                        {
                            fileBytes = new byte[inputStream.Length];
                            inputStream.Read(fileBytes, 0, fileBytes.Length);
                        }

                        // Personel nesnesinin 'Dosya' özelliğine byte dizisini atayın
                        personel.saglikRaporuDosyasi = fileBytes;
                        personel.sagliKRaporuDosyasiAdi = sagliKRaporuDosyasiAdi;

                        // Değişiklikleri kaydet
                        db.SaveChanges();

                        ViewBag.Message = "Dosya başarıyla yüklendi.";
                    }
                    else
                    {
                        ViewBag.Message = "ID'ye karşılık gelen kayıt bulunamadı.";
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Dosya yüklenirken bir hata oluştu: " + ex.Message;
                }
            }
            else
            {
                ViewBag.Message = "Lütfen bir dosya seçin.";
            }
            var logMessage = $"{kAdi} tarafından personel dosya ekleme işlemi gerçekleştirildi. " +
                 $"Eklenen Dosya Adı : {sagliKRaporuDosyasiAdi}," +
                  $"Eklenen ID : {id}";

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
            TempData.Keep("tip");
            TempData.Keep("kID");


            return RedirectToAction("DosyaGetir", "PersonelBilgi", new { id });






        }

        public ActionResult DosyaIndirSaglik(int id)
        {
            personel_takip_otomasyonuEntities db = new personel_takip_otomasyonuEntities();

            // ID'ye karşılık gelen kaydı bulun
            var personel = db.personel.Find(id);

            if (personel != null && personel.saglikRaporuDosyasi != null)
            {
                // Dosya ismini alın (İsteğe bağlı)
                string fileName = personel.sagliKRaporuDosyasiAdi + ".pdf";
                // Dosyayı kullanıcıya sunmak için FileResult kullanın
                return File(personel.saglikRaporuDosyasi, "application/pdf", fileName);
            }
            else
            {
                // Eğer kayıt bulunamazsa veya dosya yoksa hata döndürün (İsteğe bağlı)
                return HttpNotFound("Dosya bulunamadı.");
            }
        }
        public ActionResult DosyaSilSaglik(int id)
        {
            personel_takip_otomasyonuEntities db = new personel_takip_otomasyonuEntities();
            var personel = db.personel.FirstOrDefault(p => p.personelID == id);
            string kAdi = TempData["kAdi"] as string;

            var logMessage = $"{kAdi} tarafından personel dosya silme işlemi gerçekleştirildi. " +
                $"Silinen Dosya Adi {personel.sagliKRaporuDosyasiAdi}. " +
                $"Silinen ID {id}. "
                ;

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
            TempData.Keep("tip");
            TempData.Keep("kID");
            personel.sagliKRaporuDosyasiAdi = null;
            personel.saglikRaporuDosyasi = null;
            db.SaveChanges();
           
            return RedirectToAction("DosyaGetir", "PersonelBilgi", new { id });
        }



        //MEZUN DOSYASİ

        [HttpGet]
        public ActionResult mezunDosyasiYukle()
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
        public ActionResult mezunDosyasiYukle(int id, HttpPostedFileBase mezunDosyasi, string mezunDosyasiAdi)
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

            if (mezunDosyasi != null && mezunDosyasi.ContentLength > 0)
            {
                try
                {
                    // ID'ye karşılık gelen kaydı bulun
                    var personel = db.personel.Find(id);

                    if (personel != null)
                    {
                        // Dosya veritabanına eklenir
                        byte[] fileBytes;
                        using (var inputStream = mezunDosyasi.InputStream)
                        {
                            fileBytes = new byte[inputStream.Length];
                            inputStream.Read(fileBytes, 0, fileBytes.Length);
                        }

                        // Personel nesnesinin 'Dosya' özelliğine byte dizisini atayın
                        personel.mezunDosyasi = fileBytes;
                        personel.mezunDosyasiAdi = mezunDosyasiAdi;

                        // Değişiklikleri kaydet
                        db.SaveChanges();

                        ViewBag.Message = "Dosya başarıyla yüklendi.";
                    }
                    else
                    {
                        ViewBag.Message = "ID'ye karşılık gelen kayıt bulunamadı.";
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Dosya yüklenirken bir hata oluştu: " + ex.Message;
                }
            }
            else
            {
                ViewBag.Message = "Lütfen bir dosya seçin.";
            }
            var logMessage = $"{kAdi} tarafından personel dosya ekleme işlemi gerçekleştirildi. " +
                $"Eklenen Dosya Adı : {mezunDosyasiAdi}," +
                 $"Eklenen ID : {id}";

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
            TempData.Keep("tip");
            TempData.Keep("kID");



            return RedirectToAction("DosyaGetir", "PersonelBilgi", new { id });






        }

        public ActionResult DosyaIndirMezun(int id)
        {
            personel_takip_otomasyonuEntities db = new personel_takip_otomasyonuEntities();

            // ID'ye karşılık gelen kaydı bulun
            var personel = db.personel.Find(id);

            if (personel != null && personel.mezunDosyasi != null)
            {
                // Dosya ismini alın (İsteğe bağlı)
                string fileName = personel.mezunDosyasiAdi + ".pdf";
                // Dosyayı kullanıcıya sunmak için FileResult kullanın
                return File(personel.mezunDosyasi, "application/pdf", fileName);
            }
            else
            {
                // Eğer kayıt bulunamazsa veya dosya yoksa hata döndürün (İsteğe bağlı)
                return HttpNotFound("Dosya bulunamadı.");
            }
        }
        public ActionResult DosyaSilMezun(int id)
        {
            personel_takip_otomasyonuEntities db = new personel_takip_otomasyonuEntities();
            var personel = db.personel.FirstOrDefault(p => p.personelID == id);
            string kAdi = TempData["kAdi"] as string;

            var logMessage = $"{kAdi} tarafından personel dosya silme işlemi gerçekleştirildi. " +
                $"Silinen Dosya Adi {personel.mezunDosyasiAdi}. " +
                $"Silinen ID {id}. "
                ;

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
            TempData.Keep("tip");
            TempData.Keep("kID");
            personel.mezunDosyasi = null;
            personel.mezunDosyasiAdi = null;
            db.SaveChanges();
           
            return RedirectToAction("DosyaGetir", "PersonelBilgi", new { id });
        }



        //KİMLİK DOSYASİ

        [HttpGet]
        public ActionResult kimlikDosyasiYukle()
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
        public ActionResult kimlikDosyasiYukle(int id, HttpPostedFileBase kimlikDosyasi, string kimlikDosyasiAdi)
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

            if (kimlikDosyasi != null && kimlikDosyasi.ContentLength > 0)
            {
                try
                {
                    // ID'ye karşılık gelen kaydı bulun
                    var personel = db.personel.Find(id);

                    if (personel != null)
                    {
                        // Dosya veritabanına eklenir
                        byte[] fileBytes;
                        using (var inputStream = kimlikDosyasi.InputStream)
                        {
                            fileBytes = new byte[inputStream.Length];
                            inputStream.Read(fileBytes, 0, fileBytes.Length);
                        }

                        // Personel nesnesinin 'Dosya' özelliğine byte dizisini atayın
                        personel.kimlikDosyasi = fileBytes;
                        personel.kimlikDosyasiAdi = kimlikDosyasiAdi;

                        // Değişiklikleri kaydet
                        db.SaveChanges();

                        ViewBag.Message = "Dosya başarıyla yüklendi.";
                    }
                    else
                    {
                        ViewBag.Message = "ID'ye karşılık gelen kayıt bulunamadı.";
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Dosya yüklenirken bir hata oluştu: " + ex.Message;
                }
            }
            else
            {
                ViewBag.Message = "Lütfen bir dosya seçin.";
            }
            var logMessage = $"{kAdi} tarafından personel dosya ekleme işlemi gerçekleştirildi. " +
                $"Eklenen Dosya Adı : {kimlikDosyasiAdi}," +
                 $"Eklenen ID : {id}";

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
            TempData.Keep("tip");
            TempData.Keep("kID");




            return RedirectToAction("DosyaGetir", "PersonelBilgi", new { id });






        }

        public ActionResult DosyaIndirKimlik(int id)
        {
            personel_takip_otomasyonuEntities db = new personel_takip_otomasyonuEntities();

            // ID'ye karşılık gelen kaydı bulun
            var personel = db.personel.Find(id);

            if (personel != null && personel.kimlikDosyasi != null)
            {
                // Dosya ismini alın (İsteğe bağlı)
                string fileName = personel.kimlikDosyasiAdi + ".pdf";
                // Dosyayı kullanıcıya sunmak için FileResult kullanın
                return File(personel.kimlikDosyasi, "application/pdf", fileName);
            }
            else
            {
                // Eğer kayıt bulunamazsa veya dosya yoksa hata döndürün (İsteğe bağlı)
                return HttpNotFound("Dosya bulunamadı.");
            }
        }
        public ActionResult DosyaSilKimlik(int id)
        {
            personel_takip_otomasyonuEntities db = new personel_takip_otomasyonuEntities();
            var personel = db.personel.FirstOrDefault(p => p.personelID == id);
            string kAdi = TempData["kAdi"] as string;

            var logMessage = $"{kAdi} tarafından personel dosya silme işlemi gerçekleştirildi. " +
                $"Silinen Dosya Adi {personel.kimlikDosyasiAdi}. " +
                $"Silinen ID {id}. "
                ;

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
            TempData.Keep("tip");
            TempData.Keep("kID");
            personel.kimlikDosyasi = null;
            personel.kimlikDosyasiAdi = null;
            db.SaveChanges();
           
            return RedirectToAction("DosyaGetir", "PersonelBilgi", new { id });
        }
    }
}