using Kiosko.Models;
using KioskoAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kiosko.Services
{
    public class CubiQManagerService
    {
        public CubiQManagerModel.CubiQManagerResponse PostShippingData(ShippingModel _shipping)
        {
            var shipping = MapShippingData(_shipping);
            string resource = CubiQManagerModel.KioskoResource.SAVEKIOSKOSHIPPING;
            var response = Helpers.Utilities.doRequest<CubiQManagerModel.CubiQManagerResponse>(CubiQManagerModel.Resource.URL, resource , shipping, "POST");
            return response;     
        }

        public static void PostKioskoCashInventory(object Inventory)
        {
            string kiosko_pid = Helpers.Utilities.GetMachinePid();
            var paramameters = new
            {
                pid = kiosko_pid,
                cash_inventory = Inventory
            };

            string api = CubiQManagerModel.Resource.URL;
            string resource = CubiQManagerModel.Resource.URL + "/" + CubiQManagerModel.KioskoResource.POSTCASHINVENTORY;          
            Helpers.Utilities.doRequest<KioskoModel>(CubiQManagerModel.Resource.URL, resource, paramameters, "post");
        }
        


        public void PostLogService(string service, bool IsActive, string message)
        {
            string kiosko_pid = Helpers.Utilities.GetMachinePid();
            string date = Helpers.Utilities.GetDate();

            var paramameters = new
            {
                machine_pid = kiosko_pid,
                service = service,
                status = IsActive.ToString(),
                message = message
            };

            string api = CubiQManagerModel.Resource.URL;
            string resource = CubiQManagerModel.KioskoResource.POSTLOGSERVICE;

           
            Helpers.Utilities.WriteLocalLog("[ " + service + "] ->" + message);

            Helpers.Utilities.doRequest<KioskoModel>(api, resource, paramameters, "post");
        }


        public static bool RequestKioskoGeneralInformation()
        {
            string api = CubiQManagerModel.Resource.URL;
            string resource = CubiQManagerModel.KioskoResource.GETCONFIGURATION;

            var paramameters = new
            {
                machine_pid = Helpers.Utilities.GetMachinePid()
            };

            var response = Helpers.Utilities.doRequest<KioskoModel>(api, resource, paramameters, "post");

            //Save the configuration locally
            if (response != null)
            {
                Helpers.Utilities.WriteJson(Properties.Settings.Default.CONFIGURATION_PATH, response);
                return true;
            }

            Helpers.Utilities.WriteLocalLog("Cannot found machine with PID : [" + Helpers.Utilities.GetMachinePid() + " ] on CubiQ Manager");
            return false;
        }


        public List<CubiQManagerShippingDataModel> RemoveBoxes(User user)
        {
            List<ShippingModel> lista = new List<ShippingModel>();
            string api = CubiQManagerModel.Resource.URL;
            string resource = CubiQManagerModel.KioskoResource.REMOVEBOXES;
            var paramameters = new
            {
                pid = Helpers.Utilities.GetMachinePid(),
                user= user
            };

            var response = Helpers.Utilities.doRequest<List<CubiQManagerShippingDataModel>>(api, resource, paramameters, "post");

            return response;


        }

        public static void RequestKioskoPaymentConfiguration()
        {
            string api = CubiQManagerModel.Resource.URL;
            string resource = CubiQManagerModel.KioskoResource.GETPAYMENTCONFIGURATION;
            var paramameters = new
            {
                machine_pid = Helpers.Utilities.GetMachinePid()
            };

            var response = Helpers.Utilities.doRequest<List<KioskoPaymentConfiguration>>(api, resource, paramameters, "post");

            if (response == null)
                return;

            foreach (var conf in response)
            {
                RequestSinglePaymentConfiguration(conf);
            }
        }


        public User GetUser(User user)
        {
            user.UID = Helpers.Utilities.GetMachinePid();
            string resource = CubiQManagerModel.KioskoResource.LOGINKIOSKOOPUSER;
            var userResponse = Helpers.Utilities.doRequest<User>(CubiQManagerModel.Resource.URL, resource, user, "post", 10000);
            return userResponse;
        }

    

     


        private static CubiQManagerShippingModel MapShippingData(ShippingModel _shipping)
        {
            CubiQManagerShippingModel mapped = new CubiQManagerShippingModel();

            mapped.shipping = new CubiQManagerShippingDataModel()
            {
                code = _shipping.guide.Code,
                declared_value = _shipping.content.Value,
                shipping_type = _shipping.shippingType.KeyName,
                content = _shipping.content.Description,
                origin_name = _shipping.origin.Name,
                origin_id = _shipping.origin.Identification,
                origin_phone = _shipping.origin.Phone,
                origin_email = _shipping.origin.Email,
                origin_address = _shipping.origin.Location.Address,
                origin_city_name = _shipping.origin.Location.City,
                origin_department_name = _shipping.origin.Location.Department, 
                receiver_id = _shipping.receiver.Identification,
                receiver_name = _shipping.receiver.Name,
                receiver_address = _shipping.receiver.Location.Address,
                receiver_city_name = _shipping.receiver.Location.City,
                receiver_department_name = _shipping.receiver.Location.Department,
                receiver_phone = _shipping.receiver.Phone,
                initial_date = _shipping.tracking.InitialDate,
                final_date = Helpers.Utilities.GetDate(),
                authorization_transaction_code = _shipping.payment.AuthorizationTransactionCode,
                payment_method = _shipping.payment.PaymentMethod,
                total_payment = _shipping.payment.Cost.TotalCost,
                vat_number = _shipping.payment.Invoice,
                kiosko_pid = Helpers.Utilities.GetMachinePid()
                
            };

            mapped.measures = new List<CubiQManagerShippingMeasuresModel>();

            foreach (var dimension in _shipping.content.Measures)
            {
                mapped.measures.Add(
                    new CubiQManagerShippingMeasuresModel()
                    {
                        height = dimension.Height,
                        length = dimension.Length,
                        weight = dimension.Weight,
                        width = dimension.Width,
                        image64 = dimension.ImageBase64
                    }
                );
            }

            return mapped;

        }


        /**
         * Request the configuration of a single payment configuration type
         * */

        private static void RequestSinglePaymentConfiguration(KioskoPaymentConfiguration configuration)
        {

            string api = CubiQManagerModel.Resource.URL;
            string resource = CubiQManagerModel.KioskoResource.GETSINGLEPAYMENTCONFIGURATION;
            var dataConfig = new { };
            var paramameters = new
            {
                machine_pid = Helpers.Utilities.GetMachinePid(),
                kiosko_payment_type_keyname = configuration.kiosko_payment_type_keyname
            };

            var response = Helpers.Utilities.doRequest<List<object>>(api, resource, paramameters, "post");


            if(response == null)
            {
                return;
            }

            if(configuration.kiosko_payment_type_keyname.Equals("cash"))
            { 
                var cof = new
                {
                    port = "COM3",
                    bills = response
                };

                //Save the configuration locally
                Helpers.Utilities.WriteJson(Properties.Settings.Default.PAYMENT_CONFIGURATION_PATH + configuration.kiosko_payment_type_keyname + ".json", cof);

            }
            else
            {
                //Save the configuration locally
                Helpers.Utilities.WriteJson(Properties.Settings.Default.PAYMENT_CONFIGURATION_PATH + configuration.kiosko_payment_type_keyname + ".json", response);

            }





        }

    }
}