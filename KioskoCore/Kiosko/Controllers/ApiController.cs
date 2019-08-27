using Kiosko.Models;
using Kiosko.Services;
using KioskoAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using static Kiosko.Models.InventarioCash;
using static Kiosko.Models.KioskoHardware;

namespace Kiosko.Controllers
{
    /**
     * This class handle all POST and GET communication from CustomerView
     * 
     * 
     */
    public class ApiController : System.Web.Http.ApiController
    {
        CubiQController _cubiqService = new CubiQController();
        CustomerController _customerService = new CustomerController();
        PaymentController _paymentService = new PaymentController();
        PrinterController _printerService = new PrinterController();
        CubiQManagerService _cubiqManagerService = new CubiQManagerService();
        KioskoCoreService _kioskoCoreService = new KioskoCoreService();
        Libraries.DoorLocker.DoorLocker _doorLocker;
        public ApiController()
        {

        }

        [HttpGet]
        [Route("api/GetKioskoInfo")]
        public IHttpActionResult GetKioskoInfo()
        {
            try
            {
                var pid = Helpers.Utilities.GetMachinePid();
                var response = new
                {
                    pid = pid
                };      
                return Ok(response);
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("api/getMeasures/{mode}")]
        public IHttpActionResult GetMeasures(string mode)
        {
            try
            {
                var data = _cubiqService.GetMeasures(mode);

                if(data.Error.HasError)
                {
                    _cubiqManagerService.PostLogService(CubiQManagerModel.KioskoService.CUBIQSERVICE, false, data.Error.Message);
                }
                var response = new
                {
                    data
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }
        
        [HttpGet]
        [Route("api/isMaxDeclaredValue/{value}")]
        public IHttpActionResult IsMaxDeclaredValue(double value)
        {
            try
            {
                var isMaxDeclaredValue = Helpers.KioskoRestrictions.IsMaxDeclaredValue(value);

                var response = new
                {
                    isMaxDeclaredValue  = isMaxDeclaredValue
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }



        [HttpGet]
        [Route("api/getDepartmentList/")]
        public IHttpActionResult GetDepartmentList()
        {
            try
            {
                var data = _customerService.GetDepartmentList();
                
                var response = new
                {
                    data
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }
        

        [HttpGet]
        [Route("api/getCitiesList/{departmentCode}")]
        public IHttpActionResult GetCitiesList(string departmentCode)
        {
            try
            {
                var data = _customerService.GetCitiesList(departmentCode);
                var response = new
                {
                    data
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        [Route("api/getCost/")]
        [HttpPost]
        public IHttpActionResult GetCost([FromBody] ShippingModel shipping)
        {
            ShippingModel _shipping = new ShippingModel();
            _shipping = shipping;

            try
            {
                var data = _customerService.GetCost(shipping);
                if (data.error.HasError)
                {
                    _cubiqManagerService.PostLogService(CubiQManagerModel.KioskoService.CUSTOMERSERVICE, false, data.error.Message);
                }
                var response = new
                {
                    data
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }


        


        [Route("api/PaymentTest/{value}")]
        [HttpGet]
        public IHttpActionResult PaymentTest(string value)
        {
            try
            {
                var shipping = new ShippingModel();
                shipping.payment = new ShippingModel.Payment()
                {

                    PaymentMethod = "cash"

                };
                shipping.payment.Cost = new ShippingModel.Cost()
                {
                    TotalCost = Int32.Parse(value)
                };
                var data = _paymentService.RequestPayment(shipping);
                if (data.error.HasError)
                {
                    string service = shipping.payment.PaymentMethod == "pos" ? CubiQManagerModel.KioskoService.POSTERMINALSERVICE : CubiQManagerModel.KioskoService.CASHSERVICE;
                    _cubiqManagerService.PostLogService(service, false, data.error.Message);
                    data.error.Message = "Hubo un error al realizar el pago. Por favor intente nuevamente.";

                }

                var response = new
                {
                    data
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }


        [Route("api/requestPayment/")]
        [HttpPost]
        public IHttpActionResult RequestPayment([FromBody] ShippingModel shipping)
        {          
            try
            {
                var data = _paymentService.RequestPayment(shipping);
                if (data.error.HasError)
                {
                    string service = shipping.payment.PaymentMethod == "pos" ? CubiQManagerModel.KioskoService.POSTERMINALSERVICE : CubiQManagerModel.KioskoService.CASHSERVICE;
                    _cubiqManagerService.PostLogService(service, false, data.error.Message);
                    data.error.Message = "Hubo un error al realizar el pago. Por favor intente nuevamente.";

                }

                var response = new
                {
                    data
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }




        [HttpPost]
        [Route("api/generateGuide/")]
        public IHttpActionResult GenerateGuide([FromBody] ShippingModel shipping)
        {
            try
            {
                var data = _customerService.GenerateGuide(shipping);

                if (data.error.HasError)
                {
                    _cubiqManagerService.PostLogService(CubiQManagerModel.KioskoService.CUSTOMERSERVICE, false, data.error.Message);
                }
                var response = new
                {
                    data
                };

                _cubiqManagerService.PostShippingData(data);

                return Ok(response);

            }
            catch (Exception e)
            {
                return NotFound();
            }
        }



        [HttpPost]
        [Route("api/printGuide/")]
        public IHttpActionResult PrintGuide([FromBody] ShippingModel shipping)
        {
            try
            {
                var data = _printerService.PrintGuide(shipping.guide);
                if (data.HasError)
                {
                    _cubiqManagerService.PostLogService(CubiQManagerModel.KioskoService.CUSTOMERSERVICE, false, data.Message);
                }

                var response = new
                {
                    data
                };
                return Ok(response);

            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("api/generateInvoice")]
        public IHttpActionResult GenerateInvoice([FromBody] ShippingModel shipping)
        {
            try
            {
                var data = _customerService.GenerateInvoice(shipping);
               
                if (data.error.HasError)
                {
                    _cubiqManagerService.PostLogService(CubiQManagerModel.KioskoService.CUSTOMERSERVICE, false, data.error.Message);
                }
                var response = new
                {
                    data
                };
                return Ok(response);

            }
            catch (Exception e)
            {
                return NotFound();
            }
        }


        [HttpPost]
        [Route("api/printBill/")]
        public IHttpActionResult PrintBill([FromBody]ShippingModel shipping)
        {
            try
            {

                var data = _customerService.PrintInvoice(shipping);//  _printerService.PrintBill(invoice);
                if (data.error.HasError)
                {
                    _cubiqManagerService.PostLogService(CubiQManagerModel.KioskoService.CUSTOMERSERVICE, false, data.error.Message);
                }
                
                var response = new
                {
                    data
                };
                return Ok(response);

            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("api/CheckServices/")]
        public IHttpActionResult CheckServices()
        {
            try
            {
                var servicesStatus = _kioskoCoreService.CheckServices();
                var response = new
                {
                    servicesStatus
                };
                return Ok(response);

            }
            catch (Exception e)
            {
                return NotFound();
            }
        }


        [Route("api/getDevicesStatus/")]
        [HttpGet]
        public IHttpActionResult getDevicesStatus()
        {
            try
            {
                Common.Error response = new Common.Error()
                {
                    Message = "Hubo un error al abrir la compuerta",
                    HasError = true
                };



                PaymentService pyment = new PaymentService();
                List<Common.ServiceStatus> efect = pyment.GetDevicesStatus();
                return Ok(efect);



            }
            catch (Exception e)
            {
                return StatusCode(System.Net.HttpStatusCode.InternalServerError);

            }



        }



        [Route("api/getInventory/")]
        [HttpGet]
        public IHttpActionResult getInventory()
        {
            try
            {
                Common.Error response = new Common.Error()
                {
                    Message = "Hubo un error al abrir la compuerta",
                    HasError = true
                };



                PaymentService pyment = new PaymentService();
                List<Efectivo> efect = pyment.GetInventario();
                return Ok(efect);



            }
            catch (Exception e)
            {
                return StatusCode(System.Net.HttpStatusCode.InternalServerError);

            }
           


        }

        [Route("api/RemoveBoxes")]
        [HttpPost]

        public IHttpActionResult RemoveBoxes(User vm)
        {
            try
            {
                Common.Error response = new Common.Error()
                {
                    Message = "Hubo un error al comunicarse con el servicio",
                    HasError = true
                };

              var list=  _cubiqManagerService.RemoveBoxes(vm);

                return Ok(list);

            }catch(Exception E)
            {

                return NotFound();

            }
            
            
           }


        [Route("api/SetMoneyOperation/")]
        [HttpPost]
        public IHttpActionResult setMoneyOperation(EfectivoViewModel vm)
        {
            try
            {
                Common.Error response = new Common.Error()
                {
                    Message = "Hubo un error al abrir la compuerta1",
                    HasError = true
                };
                PaymentService pyment = new PaymentService();
                if (vm.operacion == "sumar")
                    response = pyment.AddMoney(vm.efectivo, vm.user).error;
                else if (vm.operacion== "restar")
                    response = pyment.RemoveMoney(vm.efectivo, vm.user).error;
                else
                    response = pyment.SetMoney(vm.efectivo).error;
                return Ok(response);



            }
            catch (SystemException E)
            {

                return NotFound();

            }



        }


        [Route("api/openBilleteros/")]
        [HttpGet]
        public IHttpActionResult OpenDoor()
        {

            try
            {
                Common.Error response = new Common.Error()
                {
                    Message = "Hubo un error al abrir la compuerta",
                    HasError = true
                };




                var doorLocker = Helpers.Utilities.GetDoorLockerByName(DoorLockerList.Billetero1);
                var doorLocker2 = Helpers.Utilities.GetDoorLockerByName(DoorLockerList.Billetero2);


                if (doorLocker != null && doorLocker2!=null)
                {
                    _doorLocker = new Libraries.DoorLocker.DoorLocker(doorLocker);
                    _doorLocker.OpenBill(Convert.ToByte(Convert.ToInt32(doorLocker.Id)), Convert.ToByte(Convert.ToInt32(doorLocker2.Id)));

                    var service = _doorLocker.getServiceStatus();
                    response = service.error;
                    if (service.error.HasError)
                    {
                        _cubiqManagerService.PostLogService(CubiQManagerModel.KioskoService.LOCKERSERVICE, false, service.error.Message);
                        response.Message = "Hubo un error al cominicarse con la puerta";
                    }

                }
                else
                {
                    _cubiqManagerService.PostLogService(CubiQManagerModel.KioskoService.LOCKERSERVICE, false, "[Billeteros] is null ->Try to open door");
                    response.HasError = true;
                    response.Message = "Hubo un error al cominicarse con la puerta";
                }


                return Ok(response);

            }
            catch (Exception e)
            {
                return NotFound();
            }


        }


        [Route("api/setMoney/{value}/{inventory}/{device}")]
        [HttpGet]
        public IHttpActionResult SetMoney(int value, int inventory, string device)
        {
            try
            {
                Common.Error response = new Common.Error()
                {
                    Message = "Hubo un error al abrir la compuerta1",
                    HasError = true
                };


                Efectivo efect = new Efectivo();
                efect.Location = device;
                efect.Inventory = inventory;
                efect.Value = value;
                PaymentService pyment = new PaymentService();
                List<Efectivo> list = new List<Efectivo>();
                list.Add(efect);
                response=  pyment.SetMoney(list).error;
                return Ok(response);



            }catch(SystemException E)
            {

                return NotFound();

            }


        }

        [Route("api/openDoor/{door}")]
        [HttpGet]
        public IHttpActionResult OpenDoor(string door)
        {
            try
            {
                Common.Error response = new Common.Error()
                {
                    Message = "Hubo un error al abrir la compuerta1",
                    HasError = true
                };

                var doorLocker = Helpers.Utilities.GetDoorLockerByName(door);
                if (doorLocker != null)
                {
                    _doorLocker = new Libraries.DoorLocker.DoorLocker(doorLocker);
                    _doorLocker.Open(Convert.ToByte(Convert.ToInt32(doorLocker.Id)));

                    var service = _doorLocker.getServiceStatus();
                    response = service.error;
                    if (service.error.HasError)
                    {
                        _cubiqManagerService.PostLogService(CubiQManagerModel.KioskoService.LOCKERSERVICE, false, service.error.Message);
                        response.Message = "Hubo un error al cominicarse con la puerta";
                    }

                }
                else
                {
                    _cubiqManagerService.PostLogService(CubiQManagerModel.KioskoService.LOCKERSERVICE, false, "[" + door + "] is null ->Try to open door");
                    response.HasError = true;
                    response.Message = "Hubo un error al cominicarse con la puerta";
                }


                return Ok(response);

            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        [Route("api/getDoorStatus/{door}")]
        [HttpGet]
        public IHttpActionResult GetDoorStatus(string door)
        {
            try
            {
                bool IsOpen = false;
                Common.Error error = new Common.Error();
                var doorLocker = Helpers.Utilities.GetDoorLockerByName(door);
                if (doorLocker != null)
                {
                    _doorLocker = new Libraries.DoorLocker.DoorLocker(doorLocker);
                    IsOpen = _doorLocker.IsOpen(Convert.ToByte(Convert.ToInt32(doorLocker.Id)));
                    var service = _doorLocker.getServiceStatus();
                    if (service.error.HasError)
                    {
                        _cubiqManagerService.PostLogService(CubiQManagerModel.KioskoService.LOCKERSERVICE, false, service.error.Message);
                        error.HasError = true;
                        error.Message = "Hubo un error al cominicarse con la puerta";
                    }
                }
                else
                {
                    _cubiqManagerService.PostLogService(CubiQManagerModel.KioskoService.LOCKERSERVICE, false, "[" + door + "] is null -> Try to get door status");
                    error.HasError = true;
                    error.Message = "Hubo un error al cominicarse con la puerta";
                }

                var response = new
                {
                    error,
                    IsOpen
                };

                return Ok(response);
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }
        
        [HttpPost]
        [Route("api/postShipping")]
        public IHttpActionResult PostShipping([FromBody] ShippingModel shipping)
        {
            try
            {
                var data = _cubiqManagerService.PostShippingData(shipping);
                return Ok(data);
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }


        [HttpPost]
        [Route("api/kioskoadmin/getUser")]
        public IHttpActionResult GetUser([FromBody] User user)
        {
            try
            {
                var data = _cubiqManagerService.GetUser(user);                
                return Ok(data);
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }


        [HttpPost]
        [Route("api/initializeShipping/")]
        public IHttpActionResult InitializeShipping([FromBody] ShippingModel shipping)
        {
            try
            {

                shipping.error = new Common.Error();
                shipping.tracking = new ShippingModel.Tracking()
                {
                    InitialDate = Helpers.Utilities.GetDate(),
                    FinalDate = ""
                };


                return Ok(shipping);
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }
    }
}