using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZenderBoxServiceModels
{
    public class CustomerServiceModel : Common
    {
        public class Cost
        {
            public float MainCost { get; set; }
            public float VariableCost { get; set; }
            public float OtherCost { get; set; }
            public double TotalCost { get; set; }
            public Error error = new Error();
        }
       
        public class Guide
        {
            public int Id { get; set; }
            public string Code { get; set; }
            public string PdfGuide { get; set; }
            public string Url { get; set; }
            public Error error = new Error();
        }

        public enum ShippingType
        {
            Package = 1,
            Label
        }

        //public class ShippingServiceType
        //{
        //    public const string  
        //}
    }
}