using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZenderBoxServiceModels
{
    public class ShippingModel : Common
    {
        public string code { get; set; }
        public Origin origin { get; set; }
        public Receiver receiver { get; set; }
        public ShippingType shippingType { get; set; }
        public Content content { get; set; }
        public Payment payment { get; set; }
        public CustomerServiceModel.Guide guide = new CustomerServiceModel.Guide();

        public Error error {get;set;}

        public ShippingModel()
        {
            
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
        public class Content
        {
            public string Description { get; set; }
            public double Value { get; set; }
            public int Quantity { get; set; }
            public List<Measure> Measures { get; set; }
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