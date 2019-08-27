using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kiosko.Models
{

    public class KioskoModel
    {
        public string id { get; set; }
        public string pid { get; set; }
        public Location location { get; set; }

        public KioskoConfiguration configuration { get; set; }

    }

    public class Location
    {
        public string id { get; set; }
        public string name { get; set; }
        public string city_code { get; set; }
        public string city_name { get; set; }
        public string department_name { get; set; }
        public string address { get; set; }
    }

    public class KioskoConfiguration
    {
        public string cubiq_ip_address { get; set; }
        public string max_declared_value { get; set; }
        public string max_package_weight_value { get; set; }
        public string max_package_width_value { get; set; }
        public string max_package_height_value { get; set; }
        public string max_package_length_value { get; set; }
        public string max_packages_quantity { get; set; }
        public string max_envelope_length_value { get; set; }
        public string max_envelope_width_value { get; set; }
        public string max_envelope_weight_value { get; set; }
        public string min_package_weight_value { get; set; }

    }

    public class KioskoAppConfiguration
    {
        public String Mode { get; set; }
        public KioskoAppService Services = new KioskoAppService();
    }

    public class KioskoAppService
    {
        public KioskoAppServiceDefinition CustomerService = new KioskoAppServiceDefinition();
        public KioskoAppServiceDefinition CubiQService = new KioskoAppServiceDefinition();

    }

    public class KioskoAppServiceDefinition
    {
        public string Production { get; set; }
        public string Development { get; set; }
    }
    public class KioskoPaymentConfiguration
    {
        public bool active { get; set; }
        public string kiosko_payment_type_name { get; set; }
        public string kiosko_payment_type_keyname { get; set; }
    }

    public class CashInventory
    {

    }

  
}