using MuhBilUyg.Models.isci_analizdb;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MuhBilUyg.Controllers
{
    public class HomeController : Controller
    {
        MySqlConnection baglanti = new MySqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["bcum"].ConnectionString);

        // GET: Home
        public ActionResult Index()
        {
            string query = "Select * from kampanyalar  Order By kampanya_tarih DESC";
            baglanti.Open();
            var kampanya = new List<kampanyalar>();
            MySqlCommand cmd = new MySqlCommand(query, baglanti);
            MySqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                kampanya.Add(new kampanyalar()
                {
                    id = Convert.ToInt32(rd["id"]),
                    kampanya_adi = (rd["kampanya_adi"]).ToString(),
                    kampanya_minadet = Convert.ToInt32(rd["kampanya_minadet"]),
                    kampanya_minucret = Convert.ToInt32(rd["kampanya_minucret"]),
                    kampanya_tarih = (rd["kampanya_tarih"]).ToString()
                });
            }
            rd.Close();
            baglanti.Close();
            return View(kampanya);
        }
    }
}