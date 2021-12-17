using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MuhBilUyg.Controllers
{
    public class LoginController : Controller
    { // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string pass)
        {
            if (username == "admin" && pass == "admin")
            {
                return RedirectToAction("Index", "Home");
            }

            else
                return View();
        }
    }
}