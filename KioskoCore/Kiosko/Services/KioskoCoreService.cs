using Kiosko.Controllers;
using Kiosko.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kiosko.Services
{
    public class KioskoCoreService
    {
       
      
        public List<KioskoServiceStatus> CheckServices()
        {
            List<KioskoServiceStatus> servicesStatus = new List<KioskoServiceStatus>
            {
                CheckCashService(),
                CheckPOSService(),
                CheckPrinterService(),
                CheckCubiQService()
            };
           


            return null;
        }

        private KioskoServiceStatus CheckCubiQService()
        {
          
            throw new NotImplementedException();
        }

        private KioskoServiceStatus CheckPOSService()
        {
            throw new NotImplementedException();
        }

        private KioskoServiceStatus CheckPrinterService()
        {
            throw new NotImplementedException();
        }

        private KioskoServiceStatus CheckCashService()
        {
            throw new NotImplementedException();
        }
    }
}