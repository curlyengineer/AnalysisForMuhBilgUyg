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
    public class KampanyaController : Controller
    {
        MySqlConnection baglanti = new MySqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["bcum"].ConnectionString);
        // GET: Kampanya
        public ActionResult Index()
        {
            string query = "Select * from kampanya Order By kampanya_tarih DESC";
            baglanti.Open();
            var kampanya = new List<kampanya>();
            MySqlCommand cmd = new MySqlCommand(query, baglanti);
            MySqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                kampanya.Add(new kampanya()
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
        [HttpGet]
        public ActionResult Kaydet()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Kaydet(kampanya Kampanyalar)
        {
            if (Kampanyalar.id == 0)
            {

                baglanti.Open();
                MySqlCommand cmd = new MySqlCommand("Insert into kampanya (id,kampanya_adi,kampanya_minucret,kampanya_minadet,kampanya_tarih) values (Null," +
                                                    "'" + Kampanyalar.kampanya_adi + "'," + Kampanyalar.kampanya_minucret + "," + Kampanyalar.kampanya_minadet + "," +
                                                    "'" + Kampanyalar.kampanya_tarih + "')", baglanti);
                cmd.ExecuteNonQuery();
                baglanti.Close();
                return RedirectToAction("Index", "Kampanya");
            }
            else
            {

                baglanti.Open();
                MySqlCommand cmd = new MySqlCommand("Update kampanya set kampanya_adi='" + Kampanyalar.kampanya_adi + "',kampanya_minucret=" + Kampanyalar.kampanya_minucret + "," +
                    " kampanya_minadet=" + Kampanyalar.kampanya_minadet + ", kampanya_tarih='" + Kampanyalar.kampanya_tarih + "' where id=" + Kampanyalar.id + "", baglanti);
                MySqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    Kampanyalar.kampanya_adi = (rd["kampanya_adi"]).ToString();
                    Kampanyalar.kampanya_minucret = Convert.ToInt32(rd["kampanya_minucret"]);
                    Kampanyalar.kampanya_minadet = Convert.ToInt32(rd["kampanya_minadet"]);
                    Kampanyalar.kampanya_tarih = (rd["kampanya_tarih"]).ToString();
                }
                baglanti.Close();
            }
            return RedirectToAction("Index", "Kampanya");
        }
        
       
        public ActionResult KampanyaEdit(kampanya Kampanyalar)
        {
            baglanti.Open();
            MySqlCommand cmd = new MySqlCommand("Select kampanya_adi,kampanya_minucret,kampanya_minadet,kampanya_tarih from kampanya where id=" + Kampanyalar.id + "", baglanti);
            MySqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {

                Kampanyalar.kampanya_adi = (rd["kampanya_adi"]).ToString();
                Kampanyalar.kampanya_minucret = Convert.ToInt32(rd["kampanya_minucret"]);
                Kampanyalar.kampanya_minadet = Convert.ToInt32(rd["kampanya_minadet"]);
                Kampanyalar.kampanya_tarih = (rd["kampanya_tarih"]).ToString();
            }
            baglanti.Close();
            return View("Kaydet", Kampanyalar);
        }
        public ActionResult Delete(kampanya Kampanyalar)
        {
            string query = "Delete from kampanyalar where id=" + Kampanyalar.id + "";
            baglanti.Open();
            MySqlCommand cmd = new MySqlCommand(query, baglanti);
            cmd.ExecuteNonQuery();
            baglanti.Close();
            return RedirectToAction("Index", "Kampanya");
        }
    }
}