using MuhBilUyg.Models.isci_analizdb;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MuhBilUyg.Controllers
{
    [Authorize]
    public class MaliyetController : Controller
    {
        MySqlConnection baglanti = new MySqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["bcum"].ConnectionString);

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
            baglanti.Open();
            MySqlCommand cmmd = new MySqlCommand("Insert into maliyet (id,sabit_ucret,prim_urun_ucreti,prim_urun_adet,prim_ucret,akort_urun_fiyat," +
                "akort_min_urun,akort_artan_fiyat,akort_toplam_urun,akort_ucret) values (Null," +
                "" + sabit_ucret+ "," + purun_ucret+ "," + purun_adet + "," + prim_ucret + "," + aurun_fiyat + "," +aurun_min + "," + akatlanan_fiyat + ","+
                "" + atoplam_urun + "," + akort_ucret + ")", baglanti);
            cmmd.ExecuteReader();
            TempData["sabitucret"] = "Toplam Çalışılan Saat:"+scalisilan_saat+" Toplam Sabit Ücret Ödemesi" + sabit_ucret;
            TempData["primucret"] = "Toplam Çalışılan Saat: "+pcalisilan_saat+" Toplam Ürün Adedi : "+purun_adet+" Toplam Prim Ücret Ödemesi" + prim_ucret;
            TempData["akortucret"] = "Katlamasız Ürün Adedi :"+aurun_min+" Toplam Ürün Adedi :"+atoplam_urun+" Toplam Akort Ücret Ödemesi" + akort_ucret;

            return View("Index");
        }
        public ActionResult Grafik()
        {

            string query = "Select  * from maliyet Order By id DESC LIMIT 1";
            baglanti.Open();
            var maliyets = new List<maliyet>();
            MySqlCommand cmd = new MySqlCommand(query, baglanti);
            MySqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                maliyets.Add(new maliyet()
                {
                    id = Convert.ToInt32(rd["id"]),
                    sabit_ucret = Convert.ToInt32(rd["sabit_ucret"]),
                    prim_urun_ucreti = Convert.ToInt32(rd["prim_urun_ucreti"]),
                    prim_urun_adet = Convert.ToInt32(rd["prim_urun_adet"]),
                    prim_ucret = Convert.ToInt32(rd["prim_ucret"]),
                    akort_urun_fiyat = Convert.ToInt32(rd["akort_urun_fiyat"]),
                    akort_min_urun = Convert.ToInt32(rd["akort_min_urun"]),
                    akort_artan_fiyat = Convert.ToInt32(rd["akort_artan_fiyat"]),
                    akort_toplam_urun = Convert.ToInt32(rd["akort_toplam_urun"]),
                    akort_ucret = Convert.ToInt32(rd["akort_ucret"]),
                });
            }
            rd.Close();
            baglanti.Close();
            return View(maliyets);
        }
    }
}