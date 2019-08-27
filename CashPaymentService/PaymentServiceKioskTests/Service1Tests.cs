using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentServiceKiosk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentServiceKiosk.Tests
{
    [TestClass()]
    public class Service1Tests
    {
        [TestMethod()]
        public void CashReceivedTest()
        {

            Service1 serv = new Service1();

            serv.PaymentExpected = 4000;


            serv.Test();

            serv.CashReceived(20000);

        }
    }
}