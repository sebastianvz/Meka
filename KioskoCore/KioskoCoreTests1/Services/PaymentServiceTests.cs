using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kiosko.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiosko.Services.Tests
{
    [TestClass()]
    public class PaymentServiceTests
    {
        [TestMethod()]
        public void HandlePaymentCashTest()
        {
            PaymentService pymnt = new PaymentService();
            pymnt.DoPayment(Models.PaymentModel.PaymentType.CASH, 4000);



            



        }
    }
}