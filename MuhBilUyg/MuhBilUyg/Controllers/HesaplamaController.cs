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
        public ActionResult PrimIndex()
        {
            return View();
        }
        // Girilen Değerlerin Hesaplanması için olan ActionResult
        [HttpPost]
        public ActionResult PrimHesapla(int urun_adetucret, int min_adet, int saat_ucret, int ekstra_ucret)
        {

            int odeme = 0;
            odeme = (urun_adetucret * min_adet) + ekstra_ucret + saat_ucret;

            string query = "Select kampanya_adi from kampanyalar where (kampanya_minucret<=" + odeme + " and kampanya_min" +
            "ucret>0) or (kampanya_minadet<=" + min_adet + " and kampanya_minadet>0 )";
            baglanti.Open();
            var kampanya = new List<kampanyalar>();
            MySqlCommand cmd = new MySqlCommand(query, baglanti);
            MySqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                kampanya.Add(new kampanyalar()
                {
                    kampanya_adi = (rd["kampanya_adi"]).ToString()
                });


            }
            if (rd.HasRows) // datareader dolumu boş mu hesaplanması 
            {
                TempData["testmsg"] = "Kazandığı Kampanya : " + (rd["kampanya_adi"]).ToString() + " yapılacak toplam ödeme :" + odeme;

                MySqlCommand cmmd = new MySqlCommand("Insert into hesaplamalar (id,hesaplama_tur,urun_fiyat,min_adet,saatlik_ucret,katlanan_fiyat,toplam_urun,ekstra_ucret,odeme,kampanya,tarih) values (Null," +
                                            "'Prim Hesaplama'," + urun_adetucret + "," + min_adet + "," + saat_ucret + "," + 0 + "," + 0 + "," + ekstra_ucret + "," + odeme + ",'" + (rd["kampanya_adi"]).ToString() + "'," +
                                            "'" + DateTime.Now + "')", baglanti);
                rd.Close(); // cmmd.ExecuteReader yapabilmemiz için önce önceki datareaderi kapatmak gerekiyor.
                
                cmmd.ExecuteReader();
            }
               else
            {
                TempData["testmsg"] = "Kazandığı Kampanya Yok.  Yapılacak toplam ödeme :" + odeme;
                MySqlCommand cmmd = new MySqlCommand("Insert into hesaplamalar (id,hesaplama_tur,urun_fiyat,min_adet,saatlik_ucret,katlanan_fiyat,toplam_urun,ekstra_ucret,odeme,kampanya,tarih) values (Null," +
                                            "'Prim Hesaplama'," + urun_adetucret + "," + min_adet + "," + saat_ucret + "," + 0 + "," + 0 + "," + ekstra_ucret + "," + odeme + ",'Kazandığı Kampanya Yok'," +
                                            "'" + DateTime.Now + "')", baglanti);
                rd.Close();// cmmd.ExecuteReader yapabilmemiz için önce önceki datareaderi kapatmak gerekiyor.
                cmmd.ExecuteReader();
            }


            rd.Close();
            return RedirectToAction("PrimIndex", "Hesaplama");
        }
        public ActionResult AkortIndex()
        {
            return View();
        }
        //Akort hesaplaması yapılan ActionResult 
        [HttpPost]
        public ActionResult AkortHesapla(int urun_ucret, int urun_adet, int katlanan_fiyat, int toplam_adet, int ekstra_ucret)
        {

            int odeme = 0;
            odeme = (urun_ucret * urun_adet) + (katlanan_fiyat * (toplam_adet - urun_adet)) + ekstra_ucret;

            string query = "Select kampanya_adi from kampanyalar where (kampanya_minucret<=" + odeme + " and kampanya_minucret>0)" +
                "or (kampanya_minadet<=" + toplam_adet + " and kampanya_minadet>0) ";
            baglanti.Open();
            var kampanya = new List<kampanyalar>();
            MySqlCommand cmd = new MySqlCommand(query, baglanti);
            MySqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                kampanya.Add(new kampanyalar()
                {
                    kampanya_adi = (rd["kampanya_adi"]).ToString()
                });
            }

            if (rd.HasRows)// datareader dolumu boş mu hesaplanması 
            {
                TempData["testmsg"] = "Kazandığı Kampanya : " + (rd["kampanya_adi"]).ToString() + " yapılacak toplam ödeme :" + odeme;

                MySqlCommand cmmd = new MySqlCommand("Insert into hesaplamalar (id,hesaplama_tur,urun_fiyat,min_adet,saatlik_ucret,katlanan_fiyat,toplam_urun,ekstra_ucret,odeme,kampanya,tarih) values (Null," +
                                            "'Akort Hesaplama'," + urun_ucret + "," + urun_adet + "," + 0 + "," + katlanan_fiyat + "," + toplam_adet + "," + ekstra_ucret + "," + odeme + ",'" + (rd["kampanya_adi"]).ToString() + "'," +
                                            "'" + DateTime.Now + "')", baglanti);
                rd.Close();// cmmd.ExecuteReader yapabilmemiz için önce önceki datareaderi kapatmak gerekiyor.
                cmmd.ExecuteReader();
            }
            else
            {
                TempData["testmsg"] = "Kazandığı Kampanya Yok.  Yapılacak toplam ödeme :" + odeme;
                MySqlCommand cmmd = new MySqlCommand("Insert into hesaplamalar (id,hesaplama_tur,urun_fiyat,min_adet,saatlik_ucret,katlanan_fiyat,toplam_urun,ekstra_ucret,odeme,kampanya,tarih) values (Null," +
                                            "'Akort Hesaplama'," + urun_ucret + "," + urun_adet + "," + 0 + "," + katlanan_fiyat + "," + toplam_adet + "," + ekstra_ucret + "," + odeme + ",'Kazandığı Kampanya Yok'," +
                                            "'" + DateTime.Now + "')", baglanti);
                rd.Close();// cmmd.ExecuteReader yapabilmemiz için önce önceki datareaderi kapatmak gerekiyor.
                cmmd.ExecuteReader();
            }

            return RedirectToAction("AkortIndex", "Hesaplama");
        }
    }
}