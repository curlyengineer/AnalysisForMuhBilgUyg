using MuhBilUyg.Models.isci_analizdb;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MuhBilUyg.Controllers
{
  
    public class HesaplamaController : Controller
    {
        MySqlConnection baglanti = new MySqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["bcum"].ConnectionString);

        // GET: Hesaplama
        public ActionResult Index()
        {
            return View();
        }
        //PrimIndex Sayfasının Gösterilmesi
        [Authorize]
        public ActionResult PrimIndex()
        {
            return View();
        }
        // Girilen Değerlerin Hesaplanması için olan ActionResult
        [HttpPost]
        public ActionResult PrimHesapla(hesaplama hesaplama)
        {
            int odeme = 0;
            odeme = (hesaplama.urun_fiyat * hesaplama.min_adet) + hesaplama.ekstra_ucret + (hesaplama.saatlik_ucret * hesaplama.toplam_saat);
            string query = "Select kampanya_adi from kampanya where (kampanya_minucret<=" + hesaplama.odeme + " and kampanya_min" +
            "ucret>0) or (kampanya_minadet<=" + hesaplama.min_adet + " and kampanya_minadet>0 )";
            baglanti.Open();
            var kampanya = new List<kampanya>();
            MySqlCommand cmd = new MySqlCommand(query, baglanti);
            MySqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                kampanya.Add(new kampanya()
                {
                    kampanya_adi = (rd["kampanya_adi"]).ToString()
                });
            }
            if (rd.HasRows) // datareader dolumu boş mu hesaplanması 
            {
                TempData["testmsg"] = "Kazandığı Kampanya : " + (rd["kampanya_adi"]).ToString()
                    + ", Toplam ürün adedi:" + hesaplama.min_adet + ", Çalışılan Saat:" + hesaplama.toplam_saat + " yapılacak toplam ödeme :" + odeme;

                MySqlCommand cmmd = new MySqlCommand("Insert into hesaplama (id,kisi_ismi,hesaplama_tur,urun_fiyat,min_adet,saatlik_ucret," +
                    "toplam_saat,katlanan_fiyat,toplam_urun,ekstra_ucret,odeme,kampanya,tarih) values (Null," +
                    "'" + hesaplama.kisi_ismi + "','Prim Hesaplama'," + hesaplama.urun_fiyat + "," + hesaplama.min_adet + "," +
                    "" + hesaplama.saatlik_ucret + "," + hesaplama.toplam_saat + "," + 0 + "," + 0 + "," + hesaplama.ekstra_ucret + "," + odeme + "," +
                    "'" + (rd["kampanya_adi"]).ToString() + "','" + DateTime.Now + "')", baglanti);
                rd.Close(); // cmmd.ExecuteReader yapabilmemiz için önce önceki datareaderi kapatmak gerekiyor.

                cmmd.ExecuteReader();
            }
            else
            {
                TempData["testmsg"] = "Kazandığı Kampanya Yok.  Yapılacak toplam ödeme :" + odeme;
                MySqlCommand cmmd = new MySqlCommand("Insert into hesaplama (id,kisi_ismi,hesaplama_tur,urun_fiyat,min_adet,saatlik_ucret," +
                    "toplam_saat,katlanan_fiyat,toplam_urun,ekstra_ucret,odeme,kampanya,tarih) values (Null," +
                    "'" + hesaplama.kisi_ismi + "', 'Prim Hesaplama'," + hesaplama.urun_fiyat + "," + hesaplama.min_adet + "," +
                   "" + hesaplama.saatlik_ucret + "," + hesaplama.toplam_saat + "," + 0 + "," + 0 + "," + hesaplama.ekstra_ucret + "," +
                   "" + odeme + ",'" + "Kazandığı Kampanya Yok" + "','" + DateTime.Now + "')", baglanti);
                rd.Close();// cmmd.ExecuteReader yapabilmemiz için önce önceki datareaderi kapatmak gerekiyor.
                cmmd.ExecuteReader();
            }
            rd.Close();
            return RedirectToAction("PrimIndex", "Hesaplama");
        }
        [Authorize]
        public ActionResult AkortIndex()
        {
            return View();
        }
        //Akort hesaplaması yapılan ActionResult 
        [HttpPost]
        public ActionResult AkortHesapla(hesaplama hesaplama)
        {
            int odeme = 0;
            odeme = (hesaplama.urun_fiyat * hesaplama.min_adet) + (hesaplama.katlanan_fiyat * (hesaplama.toplam_urun - hesaplama.min_adet)) 
                + hesaplama.ekstra_ucret;
            string query = "Select kampanya_adi from kampanya where (kampanya_minucret<=" + odeme + " and kampanya_minucret>0)" +
                "or (kampanya_minadet<=" + hesaplama.toplam_urun + " and kampanya_minadet>0) ";
            baglanti.Open();
            var kampanya = new List<kampanya>();
            MySqlCommand cmd = new MySqlCommand(query, baglanti);
            MySqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                kampanya.Add(new kampanya()
                {
                    kampanya_adi = (rd["kampanya_adi"]).ToString()
                });
            }
            if (rd.HasRows)// datareader dolumu boş mu hesaplanması 
            {
                TempData["testmsg"] = "Kazandığı Kampanya : " + (rd["kampanya_adi"]).ToString() + " yapılacak toplam ödeme :" + odeme;

                MySqlCommand cmmd = new MySqlCommand("Insert into hesaplama (id,kisi_ismi,hesaplama_tur,urun_fiyat,min_adet,saatlik_ucret," +
                    "toplam_saat,katlanan_fiyat,toplam_urun,ekstra_ucret,odeme,kampanya,tarih) values (Null," +
                      "'" + hesaplama.kisi_ismi + "','Akort Hesaplama'," + hesaplama.urun_fiyat + "," + hesaplama.min_adet + "," +
                      "" + 0 + "," + 0 + "," + hesaplama.katlanan_fiyat + "," + hesaplama.toplam_urun + "," + hesaplama.ekstra_ucret + "," +
                      "" + odeme + ",'" + (rd["kampanya_adi"]).ToString() + "','" + DateTime.Now + "')", baglanti);
                rd.Close();// cmmd.ExecuteReader yapabilmemiz için önce önceki datareaderi kapatmak gerekiyor.
                cmmd.ExecuteReader();
            }
            else
            {
                TempData["testmsg"] = "Kazandığı Kampanya Yok.  Yapılacak toplam ödeme :" + odeme;
                MySqlCommand cmmd = new MySqlCommand("Insert into hesaplama (id,kisi_ismi,hesaplama_tur,urun_fiyat,min_adet,saatlik_ucret,toplam_saat,katlanan_fiyat,toplam_urun,ekstra_ucret,odeme,kampanya,tarih) values (Null," +
                                            "'" + hesaplama.kisi_ismi + "','Akort Hesaplama'," + hesaplama.urun_fiyat + "," + hesaplama.min_adet + "," + 0 + "," + 0 + "," + hesaplama.katlanan_fiyat + "," + hesaplama.toplam_urun + "," + hesaplama.ekstra_ucret + "," + odeme + ",'" + "Kazandığı Kampanya Yok" + "'," +
                                            "'" + DateTime.Now + "')", baglanti);
                rd.Close();// cmmd.ExecuteReader yapabilmemiz için önce önceki datareaderi kapatmak gerekiyor.
                cmmd.ExecuteReader();
            }
            return RedirectToAction("AkortIndex", "Hesaplama");
        }
    }
}