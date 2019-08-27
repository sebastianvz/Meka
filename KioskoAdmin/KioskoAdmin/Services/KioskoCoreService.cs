using Kiosko.Models;
using KioskoAdmin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using static Kiosko.Models.Common;
using static Kiosko.Models.InventarioCash;

namespace KioskoAdmin.Services
{
    public class KioskoCoreService
    {
    //#if (!DEBUG)


   //     private static string url = "http://localhost/kioskocore/api/";

        // private static string url = "http://localhost/kioskocore/api/";
       // private static string url = "http://localhost:51904/api/";

    //        private static string url = "http://localhost/kioskocore/api";
    //#else

           
    //#endif

        private static string url = "http://localhost/kioskocore/api/";


        public User GetUser(User user)
        {
            var userResponse = Helpers.Utilities.doRequest<User>(url, "kioskoadmin/getUser", user , "post",20000);
            return userResponse;
        }

        public bool SetMoneyOperation(EfectivoViewModel vm)
        {
            var userResponse = Helpers.Utilities.doRequest<bool>(url, "SetMoneyOperation", vm, "post", 20000);
            return userResponse;


        }

        public List<CubiQManagerShippingDataModel> RemoveBoxes(User user)
        {

          

            if(File.Exists("C:/Kiosko/config/" + "coordinadora.json"))
            {
              CoordinadoraConfigModel config=  Helpers.Utilities.ReadFile<CoordinadoraConfigModel>("C:/Kiosko/config/" + "coordinadora.json");
                config.BoxesInStorage = false;
                Helpers.Utilities.WriteJson("C:/Kiosko/config/" + "coordinadora.json", config);
            }
            return Helpers.Utilities.doRequest<List<CubiQManagerShippingDataModel>>(url, "RemoveBoxes", user, "post", 20000);

        }


        public List<ServiceStatus> getDevicesStatus()
        {
            return Helpers.Utilities.doRequest<List<ServiceStatus>>(url, "getDevicesStatus/",null, "get");


        }
        public List<Efectivo> getInventory()
        {
            return Helpers.Utilities.doRequest<List<Efectivo>>(url , "getInventory/", null, "get");


        }


        internal void OpenDoor(string door)
        {
            var response = Helpers.Utilities.doRequest<User>(url, "openDoor/"+door);

        }

        internal void OpenBilleteros()
        {

            var response = Helpers.Utilities.doRequest<User>(url, "openBilleteros/");

        }
    }
}