using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoordinadoraService.Models
{


    public class AlegraResult
    {
        public string id { get; set; }
        public string date { get; set; }
        public string dueDate { get; set; }
        public string observations { get; set; }
        public string anotation { get; set; }
        public string termsConditions { get; set; }
        public string status { get; set; }
        public Client client { get; set; }
        public NumberTemplate numberTemplate { get; set; }
        public int total { get; set; }
        public int totalPaid { get; set; }
        public int balance { get; set; }
        public string decimalPrecision { get; set; }
        public Warehouse warehouse { get; set; }
        public object seller { get; set; }
        public object priceList { get; set; }
        public List<Payment> payments { get; set; }
        public List<Item> items { get; set; }

        public class IdentificationObject
        {
            public string type { get; set; }
            public string number { get; set; }
        }

        public class Address
        {
            public object address { get; set; }
            public object department { get; set; }
            public object city { get; set; }
        }

        public class Client
        {
            public string id { get; set; }
            public string name { get; set; }
            public string identification { get; set; }
            public string phonePrimary { get; set; }
            public string phoneSecondary { get; set; }
            public string fax { get; set; }
            public string mobile { get; set; }
            public string email { get; set; }
            public string kindOfPerson { get; set; }
            public string regime { get; set; }
            public IdentificationObject identificationObject { get; set; }
            public Address address { get; set; }
        }

        public class NumberTemplate
        {
            public string id { get; set; }
            public object prefix { get; set; }
            public string number { get; set; }
            public string text { get; set; }
        }

        public class Warehouse
        {
            public string id { get; set; }
            public string name { get; set; }
        }

        public class Payment
        {
            public string id { get; set; }
            public string prefix { get; set; }
            public string number { get; set; }
            public string date { get; set; }
            public int amount { get; set; }
            public string paymentMethod { get; set; }
            public object observations { get; set; }
            public object anotation { get; set; }
            public string status { get; set; }
        }

        public class Tax
        {
            public string id { get; set; }
            public string name { get; set; }
            public string percentage { get; set; }
            public string description { get; set; }
            public string status { get; set; }
            public string type { get; set; }
            public int amount { get; set; }
        }

        public class Item
        {
            public string name { get; set; }
            public string description { get; set; }
            public int price { get; set; }
            public string discount { get; set; }
            public object reference { get; set; }
            public int quantity { get; set; }
            public int id { get; set; }
            public List<Tax> tax { get; set; }
            public int total { get; set; }
        }

       
          
       


    }



    public class Alegra
    {
        public class Client
        {
            public int id { get; set; }
        }

        public class NumberTemplate
        {
            public int id { get; set; }
            public string prefix { get; set; }
            public int number { get; set; }
        }

        public class Seller
        {
            public string id { get; set; }
        }

        public class Currency
        {
            public string code { get; set; }
            public int exchangeRate { get; set; }
        }

        public class Tax
        {
            public int id { get; set; }
        }

        public class Item
        {
            public int id { get; set; }
            public string description { get; set; }
            public string reference { get; set; }
            public int discount { get; set; }
            public List<Tax> tax { get; set; }
            public int price { get; set; }
            public int quantity { get; set; }
        }

        public class Account
        {
            public int id { get; set; }
        }

        public class Retention
        {
            public int id { get; set; }
            public int amount { get; set; }
        }

        public class Currency2
        {
            public string code { get; set; }
            public int exchangeRate { get; set; }
        }

        public class Payment
        {
            public string date { get; set; }
            public Account account { get; set; }
            public int amount { get; set; }
            public string paymentMethod { get; set; }
            public List<Retention> retentions { get; set; }
            public Currency2 currency { get; set; }
        }

        
            public string date { get; set; }
            public string dueDate { get; set; }
            public string observations { get; set; }
            public string anotation { get; set; }
            public string termsConditions { get; set; }
            public string status { get; set; }
            public Client client { get; set; }
            public NumberTemplate numberTemplate { get; set; }
            public Seller seller { get; set; }
            public Currency currency { get; set; }
            public List<Item> items { get; set; }
            public List<Payment> payments { get; set; }
        


    }
}