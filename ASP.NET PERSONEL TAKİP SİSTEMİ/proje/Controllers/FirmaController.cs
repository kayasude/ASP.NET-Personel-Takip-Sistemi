
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace proje.Controllers
{
    public class FirmaController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult firmabilgisi()
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
    }
}