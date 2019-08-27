using Kiosko.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kiosko.Library.CashPayment.F56
{
    public class F56Model
    {
        public string port { get; set; }
        public List<Bill> bills { get; set; }

    }


    public class Bill
    {
        public string id { get; set; }
        public string name { get; set; }
        public string value { get; set; }
        public string min_length { get; set; }
        public string max_length { get; set; }
        public string tickness { get; set; }
        public int slot { get; set; }
        public double max_money { get; set; }
        public double current_money { get; set; }

    }


    public class BillTransportCommand
    {
        public BillTransportCommand()
        {
        }
        public byte DH0 { get; set; }
        public byte DH1 { get; set; }
        public byte DH2 { get; set; }
        public byte ODR { get; set; }
        public byte[] N1 { get; set; }
        public byte[] N2 { get; set; }
        public byte[] N3 { get; set; }
        public byte[] N4 { get; set; }
        public byte[] R1 { get; set; }
        public byte[] R2 { get; set; }
        public byte[] R3 { get; set; }
        public byte[] R4 { get; set; }
        public byte P1 { get; set; }
        public byte P2 { get; set; }
        public byte P3 { get; set; }
        public byte P4 { get; set; }
        public byte FS { get; set; }
    }
}