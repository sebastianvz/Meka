using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kiosko.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Prism.Events;
using Kiosko.Events;
using static Kiosko.Models.Common;

namespace Kiosko.Services.Tests
{
    [TestClass()]
    public class PaymentServiceTests
    {
        IEventAggregator _eventAggregator;
        [TestMethod()]
        public void PaymentServiceTest()
        {
            PaymentService payments = new PaymentService();


        }

        [TestMethod()]
        public void CalcularDevueltaTest()
        {
            PaymentService payments = new PaymentService();
            payments.HandlePaymentCash(17000);

            _eventAggregator = payments.GetEvents();

            Thread.Sleep(500);
            //_eventAggregator.GetEvent<Cash_credited>().Publish(20000);


            while (true)
            {

                Thread.Sleep(100);

            }
            //  payments.CashReceived(24000);
            //payments.CalcularDevuelta(24000);
        }

        [TestMethod()]
        public void GetServiceStatusTest()
        {


            PaymentService ps = new PaymentService();
            ServiceStatus st = ps.GetServiceStatus(Models.ComClass.function.system_status);




        }

        [TestMethod()]
        public void HandlePaymentCashTest()
        {
            PaymentService ps = new PaymentService();
            ps.HandlePaymentCash(4000);
            ServiceStatus ts= ps.AllServicesHaveBeenFinishedOrTimeout(100000);




        }
    }
}