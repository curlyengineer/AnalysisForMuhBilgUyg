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
    public class HomeController : Controller
    {
        MySqlConnection baglanti = new MySqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["bcum"].ConnectionString);

        // GET: Home
        
        public ActionResult Index()
        {
            string query = "Select * from  hesaplama Order By id DESC";
            baglanti.Open();
            var kampanya = new List<hesaplama>();
            MySqlCommand cmd = new MySqlCommand(query, baglanti);
            MySqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                kampanya.Add(new hesaplama()
                {
                    id = Convert.ToInt32(rd["id"]),
                    kisi_ismi=(rd["kisi_ismi"]).ToString(),
                    hesaplama_tur = (rd["hesaplama_tur"]).ToString(),
                    urun_fiyat = Convert.ToInt32(rd["urun_fiyat"]),
                    min_adet = Convert.ToInt32(rd["min_adet"]),
                    saatlik_ucret = Convert.ToInt32(rd["saatlik_ucret"]),
                    toplam_saat=Convert.ToInt32(rd["toplam_saat"]),
                    katlanan_fiyat = Convert.ToInt32(rd["katlanan_fiyat"]),
                    toplam_urun = Convert.ToInt32(rd["toplam_urun"]),
                    ekstra_ucret = Convert.ToInt32(rd["ekstra_ucret"]),
                    odeme = Convert.ToInt32(rd["odeme"]),
                    kampanya = (rd["kampanya"]).ToString(),
                    tarih = (rd["tarih"]).ToString()
                });
            }
            rd.Close();
            baglanti.Close();
            return View(kampanya);
        }
       
    }
}