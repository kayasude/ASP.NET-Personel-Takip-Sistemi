using EntitiyLayer.Concrete.Entitiy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace proje.Controllers
{
    public class LogController : Controller
    {
        // GET: Log
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult logkayit()
        {
            string kAdi = TempData["kAdi"] as string;

            if (kAdi == "yönetici")
            {
                ViewBag.AllowAllTables = true;
                TempData.Keep("kAdi");

            }
            personel_takip_otomasyonuEntities db = new personel_takip_otomasyonuEntities();
            var degerler = db.Log.ToList();
            return View(degerler);
        }
    }
}