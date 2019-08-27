using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TCCService.Models
{
    public class ShippingModel : Common
    {
        public string code { get; set; }
        public Origin origin { get; set; } = new Origin();
        public Receiver receiver { get; set; } = new Receiver();
        public ShippingType shippingType { get; set; } = new ShippingType();
        public Content content { get; set; } = new Content();
        public Payment payment { get; set; } = new Payment();
        public Error error { get; set; } = new Error();
        public Tracking tracking { get; set; } = new Tracking();
        public Guide guide { get; set; } = new Guide();

        public ShippingModel()
        {
            
        }

        public class Tracking
        {
            public string InitialDate { get; set; }
            public string FinalDate { get; set; }
        }

        public class Guide
        {
            public int Id { get; set; }
            public string Code { get; set; }
            public string PdfGuide { get; set; }
            public string Url { get; set; }

        }

        public class Origin
        {
            public string Identification { get; set; }
            public string IdentificationType { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public Location Location { get; set; }
        }

       

        public class Receiver
        {
            public string Identification { get; set; }
            public string IdentificationType { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public Location Location { get; set; }
        }

        public class ShippingType
        {
            public string Name { get; set; }
            public string KeyName { get; set; }
        }

        public class ShippingTypeKeyName
        {
            public static string PACKAGE = "package";
            public static string LABEL = "label";
        }
        public class Content
        {
            public string Description { get; set; }
            public double Value { get; set; }
            public int Quantity { get; set; }
            public List<Measure> Measures { get; set;}
        }
        public class Location
        {
            public string Country { get; set; }
            public string Department { get; set; }
            public string City { get; set; }
            public string CityCode { get; set; }
            public string Address { get; set; }
        }

        public class Payment
        {
            public string PaymentPlace { get; set; }
            public string PaymentMethod { get; set; }
            public string Invoice { get; set; }
            public string Receipt { get; set; }
            public string AuthorizationTransactionCode { get; set; }
            public Cost Cost { get; set; }
        }

        public class Cost
        {
            public float MainCost { get; set; }
            public float VariableCost { get; set; }
            public float OtherCost { get; set; }
            public double TotalCost { get; set; }

        }

        public class Measure
        {
            public string Id { get; set; }
            public string Code { get; set; }
            public double Width { get; set; }
            public double Length { get; set; }
            public double Height { get; set; }
            public double Weight { get; set; }
            public double VolumetricWeight { get; set; }
            public string ImageBase64 { get; set; }
            public int Units { get; set; }
        }

       
    }
}