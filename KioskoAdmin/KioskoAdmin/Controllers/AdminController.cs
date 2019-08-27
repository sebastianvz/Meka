using KioskoAdmin.Models;
using KioskoAdmin.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Security;
using static Kiosko.Models.Common;
using static Kiosko.Models.InventarioCash;

namespace KioskoAdmin.Controllers
{
    public class AdminController : Controller
    {
        KioskoCoreService _kioskoCoreService = new KioskoCoreService();

        public ActionResult Index(User user)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            List<ServiceStatus>devices= _kioskoCoreService.getDevicesStatus();

            ViewData["Inventario"] = _kioskoCoreService.getInventory();

            ViewData["Devices"] = devices;

            ViewData["AdminCards"] = this.GetAdminViewCards();

            return View();
        }
        public ActionResult Cash(EfectivoViewModel vm)
        {
            vm.user = new Models.User();
            vm.user.id = (int)Session["UserId"];
            vm.user.email = (string)Session["UserName"];

            if (_kioskoCoreService.SetMoneyOperation(vm))
            {


            }else
            {


            }
           

            

            return RedirectToAction("Index", "Admin");
        }

        
      


        public ActionResult Login(User user)
        {
            user = _kioskoCoreService.GetUser(user);
            if(user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if(user.email == "" || user.password == "" || !(user.id > 0))
            {
                return RedirectToAction("Index", "Home");
            }
            Session["user"]= user;
            Session["UserId"] = user.id;
            Session["UserName"] = user.email;
            Session["SessionKey"] = Helpers.Utilities.GetSessionKey(user);

            return RedirectToAction("Index", "Admin");
        }

        public ActionResult logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }



        public ActionResult RemoveBoxes()
        {
            User user = new Models.User();
            user.id = (int)Session["UserId"];
            user.email = (string)Session["UserName"];
           
           



            var result= _kioskoCoreService.RemoveBoxes(user);
            return PartialView(result);


        }

        public ActionResult Details(string device,string operation) {

           
            var res = _kioskoCoreService.getInventory().Where(c => c.Location == device).ToList();
            //objects[0] = new List<Kiosko.Models.InventarioCash.Efectivo>(); ;

            EfectivoViewModel Model = new EfectivoViewModel();
            Model.efectivo = res;
            Model.operacion = operation;

            return PartialView(Model);
        }

      



        private List<AdminViewModel.Cards> GetAdminViewCards()
        {
            List<AdminViewModel.Cards> list = new List<AdminViewModel.Cards>()
            {
               
                new AdminViewModel.Cards
                {
                    Title = "Abrir gabinete",
                    Action = "open_box",
                    Class = "kiosko-admin-action"
                },
                 new AdminViewModel.Cards
                {
                    Title = "Retirar Cajas",
                    Action = "take_boxes",
                    Class="kiosko-boxes-action modal-link",
                  
                }
            };

            return list;
        }

      

    }
}