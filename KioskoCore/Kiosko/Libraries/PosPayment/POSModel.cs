using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kiosko.Libraries.PosPayment
{
    public class POSModel
    {

        public class POSService
        {
            public Response Response { get; set; }
            public Purchase Purchase { get; set; }
        }

        public class Response
        {

            public string ResponseCode { get; set; }
            public string ResponseMessage { get; set; }
            public string Authorization { get; set; }
            public Card Card { get; set; }
            public string Amount { get; set; }
            public string Vat { get; set; }
            public string BasDev { get; set; }
            public string Receipt { get; set; }
            public bool Success { get; set; }
            public string Message { get; set; }

        }

        public class Purchase
        {
            public string OperationCode { get; set; }
            public string Amount { get; set; }
            public string Vat { get; set; }
            public string Invoice { get; set; }
            public string BasDev { get; set; }
            public string ConsumptionTax { get; set; }
            public string ATMCode { get; set; }

            public override string ToString()
            {
                return String.Format("{0},{1},{2},{3},{4},{5},{6}", OperationCode, Amount, Vat, Invoice, BasDev, ConsumptionTax, ATMCode);
            }
        }

        public class Card
        {
            public string Number { get; set; }
            public string Type { get; set; }
            public string Franchise { get; set; }

        }
    }
}