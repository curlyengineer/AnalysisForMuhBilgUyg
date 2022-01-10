using MuhBilUyg.Models.isci_analizdb;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MuhBilUyg.Controllers
{
    public class LoginController : Controller
    { // GET: Login
        MySqlConnection baglanti = new MySqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["bcum"].ConnectionString);

        // GET: Giris
        public ActionResult Index()
        {
            return View();
        }
        private string Passget(string username)
        {
            string query = "Select * from login where login_name='" + username + "'";
            baglanti.Open();
            var sifre = new List<string>();
            MySqlCommand cmd = new MySqlCommand(query, baglanti);
            MySqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                sifre.Add(rd["login_password"].ToString());


            }
            rd.Close();
            baglanti.Close();
            return sifre.Last().ToString();
        }
        [HttpPost]
        public ActionResult Index(login Giris, string password)
        {
            var pass = Passget(Giris.login_name);
            string has = DecodeFrom64(pass);
            string hashpass;
            if (password == has)
                hashpass = pass;
            else
                return RedirectToAction("Index", "Login");
            baglanti.Open();
            string query1 = "Select login_name,login_password from login where login_name='" + Giris.login_name + "'and login_password='" + pass + "'";
            MySqlCommand cmmd = new MySqlCommand(query1, baglanti);
            MySqlDataReader red = cmmd.ExecuteReader();
            if (red.Read())
            {
                FormsAuthentication.SetAuthCookie(query1, false);
                return RedirectToAction("Index", "Home");
            }

            else
            {
                return View();
            }
        }
        private string EncodeTo64(string toEncode)
        {
            byte[] toEncodeAsBytes
             = Encoding.ASCII.GetBytes(toEncode);
            string returnValue
                  = Convert.ToBase64String(toEncodeAsBytes);
            return returnValue;
        }
        static public string DecodeFrom64(string encodedData)
        {
            byte[] encodedDataAsBytes
                = Convert.FromBase64String(encodedData);
            string returnValue =
               Encoding.ASCII.GetString(encodedDataAsBytes);
            return returnValue;
        }
        [HttpGet]
        public ActionResult SifreDegistirme()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SifreDegistirme(login Giris)
        {
            string password = EncodeTo64(Giris.login_name);
            baglanti.Open();
            MySqlCommand cmd = new MySqlCommand("Update login set login_name = '" + Giris.login_name + "', login_password = '" + password + "'"
                    + " where login_name='" + Giris.login_name + "'", baglanti);
            cmd.ExecuteNonQuery();
            baglanti.Close();
            return RedirectToAction("login", "Login");
        }
    }
}