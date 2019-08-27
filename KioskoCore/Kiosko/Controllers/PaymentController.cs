using Kiosko.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Kiosko.Libraries;
using System.Threading;

namespace Kiosko.Controllers
{
    public class PaymentController
    {

        public ShippingModel RequestPayment(ShippingModel shipping)
        {
                        
            switch (shipping.payment.PaymentMethod)
            {
                case "pos":
                    shipping = HandlePosPayment(shipping);
                    break;
                case "cash":
                    shipping = HandleCashPayment(shipping);
                    break;

            }

            return shipping;
        }

        private ShippingModel HandlePosPayment(ShippingModel shipping)
        {

            Libraries.PosPayment.POSController posService = new Libraries.PosPayment.POSController();

            var serviceResponse = posService.RequestPayment(shipping.payment.Cost.TotalCost.ToString());

            if(!serviceResponse.Success)
            {
                shipping.error.HasError = true;
                shipping.error.Message = serviceResponse.Message;
            }

            shipping.payment.AuthorizationTransactionCode = serviceResponse.Authorization;
            shipping.payment.Invoice = serviceResponse.Vat;
            shipping.payment.Receipt = serviceResponse.Receipt;

            return shipping;
            
        }



        private ShippingModel HandleCashPayment(ShippingModel shipping)
        {
            bool requestDone = false;

            Kiosko.Services.PaymentService paymentService = new Kiosko.Services.PaymentService();

            var serviceStatus = paymentService.GetServiceStatus(ComClass.function.system_status);

            if (serviceStatus.error.HasError)
            {
                shipping.error = serviceStatus.error;
                return shipping;
            }

            paymentService.DoPayment(PaymentModel.PaymentType.CASH, (int)shipping.payment.Cost.TotalCost);


            while (!requestDone)
            {
                var status = paymentService.AllServicesHaveBeenFinishedOrTimeout();


                if (status.error.HasError == true)
                {
                    requestDone = true;
                    shipping.error = status.error;

                }

                if (status.IsDone && !status.error.HasError)
                {
                    requestDone = true;
                    //shipping.error = status.error;
                }
                Thread.Sleep(1000);
            }

            


            return shipping;
        }
    }
}