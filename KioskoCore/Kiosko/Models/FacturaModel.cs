using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static Kiosko.Models.CustomerServiceModel;

namespace Kiosko.Models
{
    public class FacturaModel
    {
        private Guide _guia;
        public Guide guia
        {
            get { return _guia; }
            set { _guia = value; }
        }
        private ShippingModel _shipping;
        public ShippingModel shipping_model
        {

            get { return _shipping; }
            set { _shipping = value; }


        }
        string _company_name;

        public string Company_name
        {
            get { return _company_name; }
            set { _company_name = value; }
        }
        string _company_nit;

        public string Company_nit
        {
            get { return _company_nit; }
            set { _company_nit = value; }
        }

        public string Payment_method;

        public DateTime Invoice_date;

        public DateTime Shipping_date;





    }
}