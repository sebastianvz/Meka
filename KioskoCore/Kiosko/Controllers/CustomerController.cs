using Kiosko.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kiosko.Controllers
{
    public class CustomerController
    {
        private Services.CustomerService _customerService;
        public CustomerController()
        {            
            _customerService = new Services.CustomerService(KioskoController.GetCustomerService());

        }

        public object GetDepartmentList()
        {
            return _customerService.GetDepartmentList();
        }


        public ShippingModel PrintInvoice(ShippingModel shipping)
        {
            return _customerService.PrintInvoice(shipping);


        }
        public object GetCitiesList(string departmentCode)
        {
            return _customerService.GetCitiesList(departmentCode);
        }

        public CustomerServiceModel.Cost GetCost(ShippingModel shipping)
        {
            return _customerService.GetCost(shipping);
        }

        public ShippingModel GenerateGuide(ShippingModel shipping)
        {
            return _customerService.GenerateGuide(shipping);
        }

        public ShippingModel GenerateInvoice(ShippingModel shipping)
        {
            return _customerService.GenerateInvoice(shipping);
        }
    }
}