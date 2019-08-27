using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static Kiosko.Models.CubiQManagerModel;

namespace Kiosko.Models
{

    public class CubiQManagerShippingDataModel
    {
        public string code { get; set; }
        public string initial_date { get; set; }
        public string final_date { get; set; }
        public double declared_value { get; set; }
        public string shipping_type { get; set; }

        public string origin_id { get; set; }
        public string origin_name { get; set; }
        public string origin_department_name { get; set; }
        public string origin_city_name { get; set; }
        public string origin_phone { get; set; }
        public string origin_email { get; set; }
        public string origin_address { get; set; }
        
        public string receiver_id { get; set; }
        public string receiver_name { get; set; }
        public string receiver_department_name { get; set; }
        public string receiver_city_name { get; set; }
        public string receiver_phone { get; set; }
        public string receiver_email { get; set; }
        public string receiver_address { get; set; }


        public double total_payment { get; set; }
        public string payment_method { get; set; }
        public string vat_number { get; set; }
        public string authorization_transaction_code { get; set; }
        public string kiosko_pid { get; set; }
        public string content { get; set; }

        public List<CubiQManagerShippingMeasuresModel> Measures { get; set; }


    }

    public class CubiQManagerShippingModel
    {
        public CubiQManagerShippingDataModel shipping { get; set; }
        public List<CubiQManagerShippingMeasuresModel> measures { get; set; }
    }
    public class CubiQManagerShippingMeasuresModel
    {
        public double length { get; set; }
        public double width { get; set; }
        public double height { get; set; }
        public double weight { get; set; }
        public string image64 { get; set; }
    }
    public class KioskoServiceStatus
    {
        public KioskoServiceStatus(string Service)
        {
            Name = Service;
            Active = true;
        }
        public string Name;
        public bool Active { get; set; }
        public string Message { get; set; }
    }
    public class CubiQManagerModel
    {
        public static class Resource
        {
#if (DEBUG)
          //  public static string URL = "http://localhost:8080/CubiqManager/cubiqmanager/kiosko/";
            public static string URL = "http://cubiq.mekagroupcol.com/kiosko/";
#else
 public static string URL = "http://cubiq.mekagroupcol.com/kiosko/";
#endif


        }
        public static class KioskoService
        {
            public const string CUBIQSERVICE = "cubiqservice";

            public const string DANESERVICE = "daneservice";

            public const string CUSTOMERSERVICE = "customerservice";

            public const string POSTERMINALSERVICE = "posterminalservice";

            public const string PRINTERSERVICE = "printerservice";

            public const string CASHSERVICE = "cashservice";

            public const string LOCKERSERVICE = "lockerservice";

        }

        public static class KioskoResource
        {
            public const string GETCONFIGURATION = "getConfiguration";

            public const string POSTLOGSERVICE = "PostLogService";

            public const string SAVEKIOSKOSHIPPING = "SaveKioskoShipping";

            public const string GETPAYMENTCONFIGURATION = "getPaymentConfiguration";

            public const string GETSINGLEPAYMENTCONFIGURATION = "getSinglePaymentConfiguration";

            public const string POSTCASHINVENTORY = "UpdateInventory";

            public const string LOGINKIOSKOOPUSER = "loginKioskoOpUser";

            public const string REMOVEBOXES = "RemoveBoxes";


        }
        
        public class CubiQManagerResponse
        {
            public object Data { get; set; }
            public string Message { get; set; }
            public bool Success { get; set; }
        }

    }
}