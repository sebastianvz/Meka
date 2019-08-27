using Kiosko.Models;
using Kiosko.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Kiosko.Controllers
{
    public class KioskoController
    {

        private static string KIOSKO_PATH = @"C:/Kiosko/";
        public KioskoController()
        {

        }

        public void Start()
        {
            CreateFolders();
            RequestKioskoConfiguration();
            RequestPaymentConfiguration();
        }


        public static KioskoAppConfiguration ReadAppConfiguration()
        {
            if(!File.Exists(Properties.Settings.Default.KIOSKO_PATH + "app_configuration.json"))
            {
                CreateAppConfigurationFile();
            }
            return Helpers.Utilities.ReadFile<KioskoAppConfiguration>(Properties.Settings.Default.KIOSKO_PATH + "app_configuration.json");
        }

       

        private void CreateFolders()
        {
            if (!Directory.Exists(KIOSKO_PATH))
            {
                Directory.CreateDirectory(KIOSKO_PATH);
            }
            List<string> paths = new List<string>
            {
                "temp/guide",
                "temp/invoice",
                "config"
            };

            foreach (string path in paths)
            {
                if (!Directory.Exists(KIOSKO_PATH + path))
                {
                    Directory.CreateDirectory(KIOSKO_PATH + path);
                }

            }

            if (!File.Exists(Properties.Settings.Default.KIOSKO_PATH + "app_configuration.json"))
            {
                CreateAppConfigurationFile();
            }


            CreateCommonFiles();
        }


        private void CreateCommonFiles()
        {
            if (!File.Exists(Properties.Settings.Default.DOOR_LOCKER_PATH))
            {
                List<KioskoHardware.DoorLocker> doorLockerList = new List<KioskoHardware.DoorLocker>()
                {
                    new KioskoHardware.DoorLocker(){Name = KioskoHardware.DoorLockerList.Screen, COMPort = "COM9" , Id = "0"},
                    new KioskoHardware.DoorLocker(){Name = KioskoHardware.DoorLockerList.Billetero1, COMPort = "COM9" , Id = "1"},
                    new KioskoHardware.DoorLocker(){Name = KioskoHardware.DoorLockerList.Billetero2, COMPort = "COM9"  , Id = "2"},
                    new KioskoHardware.DoorLocker(){Name = KioskoHardware.DoorLockerList.Bascula, COMPort = "COM9"  , Id = "3"},
                    new KioskoHardware.DoorLocker(){Name = KioskoHardware.DoorLockerList.Container, COMPort = "COM9"  , Id = "4"},
                    new KioskoHardware.DoorLocker(){Name = KioskoHardware.DoorLockerList.ContainerReceiver, COMPort = "COM9"  , Id = "5"},
                };

                Helpers.Utilities.WriteJson(Properties.Settings.Default.DOOR_LOCKER_PATH, doorLockerList);

            }

        }

        public void RequestKioskoConfiguration()
        {
            Helpers.Utilities.GetKioskoConfiguration(true);
        }

        public void RequestPaymentConfiguration()
        {
            Helpers.Utilities.GetPaymentConfiguration(true);
        }

        /**
         *  Creates the default app configuration
         * */
        private static void CreateAppConfigurationFile()
        {
            KioskoAppConfiguration cof = new KioskoAppConfiguration()
            {
                Mode = "development",
                Services = new KioskoAppService()
                { 
                    CubiQService = new KioskoAppServiceDefinition()
                    {
                        Development = "192.168.1.80",
                        Production = "192.168.1.80"
                    },
                    CustomerService = new KioskoAppServiceDefinition()
                    {
                        Development = "http://localhost:57682/api/customer/",
                        Production = "http://localhost/tccservice/api/customer/"
                    }
                }
            };

            Helpers.Utilities.WriteJson(Properties.Settings.Default.KIOSKO_PATH + "app_configuration.json", cof);
            
        }

        
        public static String GetCustomerService()
        {
            KioskoAppConfiguration cof = ReadAppConfiguration();
            string customerServiceUrl;

            if (cof.Mode.Equals("production")){
                customerServiceUrl = cof.Services.CustomerService.Production;
            }
            else
            {
                customerServiceUrl = cof.Services.CustomerService.Development;
            }
            return customerServiceUrl;
        }

        public static String GetCubiQService()
        {
            KioskoAppConfiguration cof = ReadAppConfiguration();
            string cubiQServiceUrl;

            if (cof.Mode.Equals("production")){
                cubiQServiceUrl = cof.Services.CubiQService.Production;
            }
            else
            {
                cubiQServiceUrl = cof.Services.CubiQService.Development;
            }
            return cubiQServiceUrl;
        }
    }
}