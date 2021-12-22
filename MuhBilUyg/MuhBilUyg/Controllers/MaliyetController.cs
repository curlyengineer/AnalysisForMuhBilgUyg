using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MuhBilUyg.Controllers
{
    public class MaliyetController : Controller
    {
        // GET: Maliyet
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Hesapla(int ssaatlik_ucret,int scalisilan_saat, int sextra_ucret,int psaatlik_ucret,int pcalisilan_saat,int purun_ucret,
            int pextra_ucret,int purun_adet, int aurun_fiyat,int aurun_min,int akatlanan_fiyat,int aextra_ucret, int atoplam_urun)
        {
            int sabit_ucret = (ssaatlik_ucret * scalisilan_saat) + sextra_ucret;
            int prim_ucret = ((pcalisilan_saat * psaatlik_ucret) + (purun_adet * purun_ucret)) + pextra_ucret;
            int akort_ucret = (aurun_fiyat + aurun_min) + (akatlanan_fiyat * (atoplam_urun - aurun_min)) + aextra_ucret;
            TempData["sabitucret"] = "Toplam Sabit Ücret Ödemesi" + sabit_ucret;
            TempData["primucret"] = "Toplam Prim Ücret Ödemesi" + prim_ucret;
            TempData["akortucret"] = "Toplam Akort Ücret Ödemesi" + akort_ucret;

            return View("Index");
        }
    }
}