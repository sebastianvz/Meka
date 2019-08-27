using Kiosko.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kiosko.Services
{
    public class CubiQManagerService
    {
       


        public static void PostKioskoCashInventory(object Inventory)
        {
            string kiosko_pid = Helpers.Utilities.GetMachinePid();
            var paramameters = new
            {
                pid = kiosko_pid,
                cash_inventory = Inventory
            };

            string api = CubiQManagerModel.Resource.URL;
            string resource =  CubiQManagerModel.KioskoResource.POSTCASHINVENTORY;
            Helpers.Utilities.doRequest<KioskoModel>(CubiQManagerModel.Resource.URL, resource, paramameters, "post");
        }



    


    

 



    }
}