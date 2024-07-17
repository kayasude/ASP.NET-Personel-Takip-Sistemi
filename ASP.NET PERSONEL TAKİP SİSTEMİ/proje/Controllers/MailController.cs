using EntitiyLayer.Concrete.Entitiy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace proje.Controllers
{
    public class MailController : Controller
    {
        // GET: Mail
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult mailgonder(string adsoyad,string email,string konu,string mesaj)
        {
          
            personel_takip_otomasyonuEntities db = new personel_takip_otomasyonuEntities();
            string kAdi = TempData["kAdi"] as string;
            if (kAdi == "yönetici")
            {
                ViewBag.AllowAllTables = true;
                TempData.Keep("kAdi");

            }
            if (adsoyad!=null && email != null && konu != null && mesaj != null)
            {
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.EnableSsl = true;
                WebMail.UserName = "foodeepdks@gmail.com";
                WebMail.Password = "tvbl awdg wlip hnnr";
                WebMail.SmtpPort = 587;

                WebMail.Send(email, konu, mesaj);



                var logMessage = $"{kAdi} tarafından Mail Gönderme işlemi gerçekleştirildi. " +
                $"Gönderilen Email  Adı : {email} , " +
                $"Gönderilen Email'in Konusu : {konu} " +
                $"Gönderilen Email'in Mesajı  : {mesaj} , ";

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

                return RedirectToAction("firmabilgisi", "Firma");

            }
            else
            {
                return View();
            }
            

        }
    }
}