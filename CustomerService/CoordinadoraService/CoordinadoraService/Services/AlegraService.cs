using CoordinadoraService.Models;
using Kiosko.Helpers;
using Kiosko.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoordinadoraService.Services
{
    public class AlegraService
    {


        public AlegraResult CreateDummyInvoice(ShippingModel _shipping)
        {
            AlegraResult alegra = new AlegraResult();
            alegra.client = new AlegraResult.Client();
            alegra.client.address = new AlegraResult.Address();
            alegra.client.address.address = _shipping.origin.Location.Address;
            alegra.client.address.city = _shipping.origin.Location.City;
            alegra.client.email = _shipping.origin.Email;
            alegra.numberTemplate = new AlegraResult.NumberTemplate();
            alegra.numberTemplate.number = "1242323";
            return alegra;


        }

      
        public AlegraResult CreateInvoice(ShippingModel _shipping)
        {
            var shipping = MapShippingData(_shipping);
            string url = "https://app.alegra.com";
            string resource = "/api/v1/invoices";
            string base64 =Utilities.Base64Encode("camilo.garcia@mekagroupcol.com:26227c0b2a984dfb01a7");
            var response = Kiosko.Helpers.Utilities.doRequest<AlegraResult>(url, resource, shipping, "POST", 5000, base64);
            return response;
        }


        private static Alegra MapShippingData(ShippingModel shipping)
        {
            Alegra mapped = new Alegra();
            List<Alegra.Tax> taxes = new List<Alegra.Tax>();
            taxes.Add(new Alegra.Tax() { id = 1 });

            List<Alegra.Payment> payment = new List<Alegra.Payment>();
            payment.Add(new Alegra.Payment() {
                amount = Convert.ToInt32(shipping.payment.Cost.TotalCost),
                date = Utilities.GetDate(),
                paymentMethod = "cash",
                account = new Alegra.Account() { id=3}
                


            });

            List<Alegra.Item> items = new List<Alegra.Item>();
            Alegra.Item item = new Alegra.Item()
            {
                description = "Envio por kiosko",
                discount = 0,
                price = Convert.ToInt32(shipping.payment.Cost.TotalCost),
                quantity = 1,
                tax = taxes,
                id=45


            };
            items.Add(item);

          
            mapped.date = Utilities.GetDate();
            mapped.dueDate = Utilities.GetDate();
            mapped.observations = "Factura generada por kiosko";
            mapped.anotation = shipping.code;
            mapped.status = "open";
            mapped.client = new Alegra.Client() { id = 128 };
            mapped.numberTemplate = new Alegra.NumberTemplate() { id = 1 };

            mapped.items = items;
            mapped.payments = payment;

            return mapped;

        }


    }
}