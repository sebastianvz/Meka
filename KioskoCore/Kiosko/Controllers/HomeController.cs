using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kiosko.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            KioskoController kiosko = new KioskoController();

            kiosko.Start();
            return View();
        }

        public ActionResult Start()
        {
            ViewBag.Title = Helpers.Utilities.GetMachinePid();             
            string source = Request.QueryString["source"];



            if (!string.IsNullOrEmpty(source))
                if (source.Equals("welcome"))
                    return View();

            return RedirectToAction("Index", "Home");
            
        }

        
    }
}
