
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using DataAccesLayer.Concrete;
using EntitiyLayer.Concrete.Entitiy;
namespace proje.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Index(kullanıcı p1)
        {
            personel_takip_otomasyonuEntities db = new personel_takip_otomasyonuEntities();

            var kullanici = db.kullanıcı.FirstOrDefault(p => p.kullanıcıAdı ==p1.kullanıcıAdı  && p.sifre == p1.sifre && p.yetkiSeviyesi == p1.yetkiSeviyesi);
            if (kullanici != null)
            {

                if (kullanici.yetkiSeviyesi == "YÖNETİCİ")
                {
                    TempData["kAdi"] = kullanici.kullanıcıAdı;
                    TempData["kID"] = kullanici.kullanıcıID;
                    TempData["tip"] = p1.yetkiSeviyesi;

                    ViewBag.AllowAllTables = true;
                    return View("firmabilgisi");
                }
                else if (kullanici.yetkiSeviyesi == "MÜDÜR")
                {
                    
                        var mudurler = db.kullanıcı.ToList(); // Tüm müdürleri al

                        foreach (var mudur in mudurler)
                        {
                            if (kullanici.kullanıcıAdı == mudur.kullanıcıAdı)
                            {
                                TempData["kAdi"] = kullanici.kullanıcıAdı;
                                TempData["kID"] = kullanici.kullanıcıID;
                                TempData["tip"] = p1.yetkiSeviyesi;
                            

                            if (kullanici.kullanıcıAdı == "sudekaya")
                            {
                               

                                ViewBag.AllowKonyaTable = true;
                                return View("firmabilgisi");

                            }
                            else if (kullanici.kullanıcıAdı == "oguzhankilic")
                            {
                           
                                ViewBag.AllowAnkaraTable = true;
                                return View("firmabilgisi");
                            }
                            else if (kullanici.kullanıcıAdı == "eraykor")
                            {
                                ViewBag.AllowIstanbulTable = true;

                                return View("firmabilgisi");
                            }
                            else if (kullanici.kullanıcıAdı == "zeynepaldatmaz")
                            {
                              

                                ViewBag.AllowNevsehirTable = true;
                                return View("firmabilgisi");
                            };
                        }
                        }
                    
                    // Diğer şartları kontrol et ve işlem yap
                }

            }




            return View();
        }
    }
}