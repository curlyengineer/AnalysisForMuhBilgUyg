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
        public ActionResult Hesapla()
        {
            return View();
        }
    }
}