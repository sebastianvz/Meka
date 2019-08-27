using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KioskoAdmin.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Start()
        {
            ViewBag.Title = "Kiosko";

            return View();

            string source = Request.QueryString["source"];

            if (!string.IsNullOrEmpty(source))
                if (source.Equals("welcome"))
                    return View();

            return RedirectToAction("Index", "Home");

        }
    }
}