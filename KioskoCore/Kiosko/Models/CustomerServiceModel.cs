using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kiosko.Models
{
    public class CustomerServiceModel:Common
    {

        public class Resource{

            public static string GETCOST = "getCost";
            public static string GETGUIDE = "getGuide";
            public static string GENERATEGUIDE = "generateGuide";
            public static string GETDEPARTMENTLIST = "getDepartments";
            public static string GETCITIESLIST = "getCities";
            public static string GENERATEINVOICE = "generateInvoice";
            public static string PRINTINVOICE = "printInvoice";
        }
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


    }
}