using Kiosko.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kiosko.Services
{
    public class CustomerService
    {
        private string CustomerServiceUrl;
        public CustomerService(string url)
        {
            CustomerServiceUrl = url;
        }

        public CustomerServiceModel.Cost GetCost(ShippingModel shipping)
        {
            CustomerServiceModel.Cost cost = new CustomerServiceModel.Cost();
           
            try
            {
                var costRequest = Helpers.Utilities.doRequest<CustomerServiceModel.Cost>(CustomerServiceUrl, CustomerServiceModel.Resource.GETCOST, shipping, "POST" , 50000);

                if(costRequest == null)
                {
                    cost.error.HasError = true;
                    cost.error.Message = "Hubo un error al consumir el servicio de costo";
                    return cost;
                }

                cost = costRequest;
            } catch(Exception e)
            {
                cost.error.HasError = true;
                cost.error.Message = e.Message;
            }

            return cost;
        }

        public ShippingModel GenerateInvoice(ShippingModel shipping)
        {
            try
            {
                var invoiceRequest = Helpers.Utilities.doRequest<ShippingModel>(CustomerServiceUrl, CustomerServiceModel.Resource.GENERATEINVOICE, shipping, "post", 50000);

                if (invoiceRequest == null)
                {
                    shipping.error.HasError = true;
                    shipping.error.Message = "Hubo un error al consumir el servicio de Factura :" + invoiceRequest.error.Message;
                    return shipping;
                }

                shipping = invoiceRequest;
            }
            catch (Exception e)
            {
                shipping.error.HasError = true;
                shipping.error.Message = "Hubo un error al consumir el servicio de FACTURA: " + e.Message;
            }

            return shipping;
        }



        public ShippingModel PrintInvoice(ShippingModel shipping)
        {
            try
            {
                var invoiceRequest = Helpers.Utilities.doRequest<ShippingModel>(CustomerServiceUrl, CustomerServiceModel.Resource.PRINTINVOICE, shipping, "post", 50000);

                if (invoiceRequest == null)
                {
                    shipping.error.HasError = true;
                    shipping.error.Message = "Hubo un error al consumir el servicio de Factura :" + invoiceRequest.error.Message;
                    return shipping;
                }

                shipping = invoiceRequest;
            }
            catch (Exception e)
            {
                shipping.error.HasError = true;
                shipping.error.Message = "Hubo un error al consumir el servicio de FACTURA: " + e.Message;
            }

            return shipping;
        }



        public object GetDepartmentList()
        {
            object departmentList = null;
            try
            {
               var departmentListRequest = Helpers.Utilities.doRequest<object>(CustomerServiceUrl, CustomerServiceModel.Resource.GETDEPARTMENTLIST,null,"get",20000);

                if(departmentListRequest != null)
                {
                    departmentList = departmentListRequest;
                }
            } catch(Exception e)
            {
               
            }

            return departmentList;
        }

        public object GetCitiesList(string departmentCode)
        {
            object citiesList = null;
            try
            {
                var citiestListRequest = Helpers.Utilities.doRequest<object>(CustomerServiceUrl, CustomerServiceModel.Resource.GETCITIESLIST + "/" + departmentCode, null, "get", 20000);

                if (citiestListRequest != null)
                {
                    citiesList = citiestListRequest;
                }
            }
            catch (Exception e)
            {

            }

            return citiesList;
        }


        public ShippingModel GenerateGuide(ShippingModel shipping)
        {
            
            try
            {
                var guideRequest = Helpers.Utilities.doRequest<ShippingModel>(CustomerServiceUrl, CustomerServiceModel.Resource.GENERATEGUIDE, shipping, "post", 500000);

                if (guideRequest == null)
                {
                    shipping.error.HasError = true;
                    shipping.error.Message = "Hubo un error al consumir el servicio de Guias :" + guideRequest;
                    return shipping;
                }

                shipping = guideRequest;
            }
            catch (Exception e)
            {
                shipping.error.HasError = true;
                shipping.error.Message = "Hubo un error al consumir el servicio de GUIAS" + e.Message;
            }

            return shipping;
        }

        
    }
}