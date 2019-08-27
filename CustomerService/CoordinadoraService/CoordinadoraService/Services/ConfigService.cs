using CoordinadoraService.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace CoordinadoraService.Services
{
    public class ConfigService
    {

        public ConfigService()
        {
           
        }

        public static CoordinadoraConfigModel readCoordinadoraConfig()
        {

            if (!File.Exists(Properties.Settings.Default.PAYMENT_CONFIGURATION_PATH + "coordinadora.json"))
            {
                CreateAppConfigurationFile();
            }
            return Kiosko.Helpers.Utilities.ReadFile<CoordinadoraConfigModel>(Properties.Settings.Default.PAYMENT_CONFIGURATION_PATH + "coordinadora.json");


        }
        private static void CreateAppConfigurationFile()
        {
            CoordinadoraConfigModel cof = new CoordinadoraConfigModel()
            { BoxesInStorage = false
            };

            Kiosko.Helpers.Utilities.WriteJson(Properties.Settings.Default.PAYMENT_CONFIGURATION_PATH + "coordinadora.json", cof);

        }


    }
}