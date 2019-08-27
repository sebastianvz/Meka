using Kiosko.Events;
using Kiosko.Models;
using KioskoAdmin.Models;
using Newtonsoft.Json;
using Prism.Events;
using SocketLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using static Kiosko.Models.Common;
using static Kiosko.Models.InventarioCash;

namespace Kiosko.Services
{
    public class PaymentService
    {

        bool TransactionFinished;
        EventAggregator evnt = new EventAggregator();
       
        int BufferPaymentReceived;
        int PaymentExpected;
        Helpers.Timeout timeout;
        System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();
        int interval2 = 0;
       public PaymentService()
        {
            try
            {
                timer1.Interval = 10000;
                timer1.Tick += Timer1_Tick;

            }
            catch(Exception e)
            {
                
            }

           
            
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            var serviceStatus = GetServiceStatus(ComClass.function.operation_status);

            interval2 += 1;

            if (serviceStatus.IsDone == true)
            {
                TransactionFinished = true;

            }
            
        }

        public List<Efectivo> GetInventario()
        {
            
                ComClass clase = new ComClass();
                clase.funciones = ComClass.function.get_money;

                string Test = JsonConvert.SerializeObject(clase);
                using (var socket = new ConnectedSocket("127.0.0.1", 1337)) // Connects to 127.0.0.1 on port 1337
                {
                    socket.Send(Test); // Sends some data
                    var data = socket.Receive(); // Receives some data back (blocks execution)
                    ComClass deserializedProduct = JsonConvert.DeserializeObject<ComClass>(data);
                    return deserializedProduct.Inventario;
                }


               

          

        }
        public List<Common.ServiceStatus> GetDevicesStatus()
        {

            try
            {
                ComClass clase = new ComClass();
                clase.funciones = ComClass.function.get_devices_status;

                string Test = JsonConvert.SerializeObject(clase);
                using (var socket = new ConnectedSocket("127.0.0.1", 1337)) // Connects to 127.0.0.1 on port 1337
                {
                    socket.Send(Test); // Sends some data
                    var data = socket.Receive(); // Receives some data back (blocks execution)
                    ComClass deserializedProduct = JsonConvert.DeserializeObject<ComClass>(data);
                    Helpers.Utilities.WriteLocalLog("[GetDevicesStatus] Devices loaded ok");
                    return deserializedProduct.DeviceStatus;
                }
            }catch(Exception e)
            {
                Helpers.Utilities.WriteLocalLog("[GetDevicesStatus] " + e.Message);
            }
            return null;   
        }
        public Common.ServiceStatus RemoveMoney(List<Efectivo> val, User user)
        {
            Common.ServiceStatus response = new Common.ServiceStatus();
            response.error.HasError = true;
            response.error.Message = "Error recibido del PaymentDevice";
            ComClass clase = new ComClass();
            clase.funciones = ComClass.function.remove_money;
            clase.Inventario = val;
            clase.user = user;
            string Test = JsonConvert.SerializeObject(clase);
            using (var socket = new ConnectedSocket("127.0.0.1", 1337)) // Connects to 127.0.0.1 on port 1337
            {
                socket.Send(Test); // Sends some data
                var data = socket.Receive(); // Receives some data back (blocks execution)
                ComClass deserializedProduct = JsonConvert.DeserializeObject<ComClass>(data);
                if (deserializedProduct.result == ComClass.status_cash.ok)
                {
                    response.IsDone = true;
                    response.error.HasError = false;
                    return response;
                }
            }


            return response;


        }

        /// <summary>
        ///cuando se agrega dinero al Sistema de pagos, hay que enviar el usuario que esta haciendo la operacion.
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>

        public Common.ServiceStatus AddMoney(List<Efectivo> val, User user)
        {



            Common.ServiceStatus response = new Common.ServiceStatus();
            response.error.HasError = true;
            response.error.Message = "Error recibido del PaymentDevice";
            ComClass clase = new ComClass();
            clase.funciones = ComClass.function.add_money;
            clase.Inventario = val;
            clase.user = user;
            string Test = JsonConvert.SerializeObject(clase);
            using (var socket = new ConnectedSocket("127.0.0.1", 1337)) // Connects to 127.0.0.1 on port 1337
            {
                socket.Send(Test); // Sends some data
                var data = socket.Receive(); // Receives some data back (blocks execution)
                ComClass deserializedProduct = JsonConvert.DeserializeObject<ComClass>(data);
                if (deserializedProduct.result == ComClass.status_cash.ok)
                {
                    response.IsDone = true;
                    response.error.HasError = false;
                    return response;
                }
            }


            return response;




        }


        public Common.ServiceStatus SetMoney(List<Efectivo> val)
        {
            Common.ServiceStatus response = new Common.ServiceStatus();
            response.error.HasError = true;
            response.error.Message = "Error recibido del PaymentDevice";
            ComClass clase = new ComClass();
            clase.funciones = ComClass.function.set_money;
            clase.Inventario = val;
            string Test = JsonConvert.SerializeObject(clase);
            using (var socket = new ConnectedSocket("127.0.0.1", 1337)) // Connects to 127.0.0.1 on port 1337
            {
                socket.Send(Test); // Sends some data
                var data = socket.Receive(); // Receives some data back (blocks execution)
                ComClass deserializedProduct = JsonConvert.DeserializeObject<ComClass>(data);
                if(deserializedProduct.result== ComClass.status_cash.ok)
                {
                    response.IsDone = true;
                    response.error.HasError = false;
                    return response;
                }
            }


            return response;




        }


        public Common.ServiceStatus GetServiceStatus(ComClass.function function)
        {

            Common.ServiceStatus response = new Common.ServiceStatus();

            try
            {
                ComClass clase = new ComClass();
                clase.funciones = function;
                string Test = JsonConvert.SerializeObject(clase);
                using (var socket = new ConnectedSocket("127.0.0.1", 1337)) // Connects to 127.0.0.1 on port 1337
                {
                    socket.Send(Test); // Sends some data
                    var data = socket.Receive(); // Receives some data back (blocks execution)
                    ComClass deserializedProduct = JsonConvert.DeserializeObject<ComClass>(data);
                    if (function == ComClass.function.system_status)
                    {
                        if (deserializedProduct.status == ComClass.machine_status.idle)
                        {
                            response.IsDone = true;
                            response.error.HasError = false;
                            return response;
                        }
                        else
                        {
                            response.IsDone = false;
                            response.error.HasError = true;
                            response.error.Message = deserializedProduct.status.ToString();
                        }
                    }
                    else if (function == ComClass.function.operation_status)
                    {

                        if (deserializedProduct.result == ComClass.status_cash.ok)
                        {
                            response.IsDone = true;
                            response.error.HasError = false;

                            return response;
                        }
                        else if (deserializedProduct.result == ComClass.status_cash.time_out)
                        {
                            response.IsDone = false;
                            response.error.HasError = true;
                            response.error.Message = ComClass.status_cash.time_out.ToString();
                            return response;

                        }
                        else if (deserializedProduct.result == ComClass.status_cash.operation_error)
                        {
                            response.IsDone = false;
                            response.error.HasError = true;
                            response.error.Message = ComClass.status_cash.time_out.ToString();
                            return response;

                        }
                        else if (deserializedProduct.result == ComClass.status_cash.waiting)
                        {
                            response.IsDone = false;
                            response.error.HasError = false;
                            response.error.Message = "";
                            return response;

                        }
                    }

                }
            }catch (Exception e)
            {
                response.error.HasError = true;
                response.error.Message = e.Message;
                response.IsDone = true;
            }
                           
             return response;     

        }



        public Common.ServiceStatus AllServicesHaveBeenFinishedOrTimeout(int _timeout = 100000)
        {
            Common.ServiceStatus response = new Common.ServiceStatus();
            try
            {


                
                var serviceStatus = GetServiceStatus( ComClass.function.operation_status);




                return serviceStatus;
        



               
                    
             
                response.IsDone = false;
            }catch(Exception e)
            {
                response.error.HasError = true;
                response.error.Message = e.Message;
                response.IsDone = true;
            }

            return response;
           
        }



       
        public EventAggregator GetEvents()
        {
            return evnt;

        }

       


   

        public void DoPayment(PaymentModel.PaymentType paymentType , int quantity)
        {

            switch(paymentType)
            {
                case PaymentModel.PaymentType.CASH:
                    this.HandlePaymentCash(quantity);
                    break;

                case PaymentModel.PaymentType.POS:
                    break;
            }

        }

        public void HandlePaymentCash(int quantity)
        {
            ComClass clase = new ComClass();
            clase.funciones = ComClass.function.cash_handling;
            clase.Value = quantity;
            string Test = JsonConvert.SerializeObject(clase);
            using (var socket = new ConnectedSocket("127.0.0.1", 1337)) // Connects to 127.0.0.1 on port 1337
            {
                socket.Send(Test); // Sends some data
                var data = socket.Receive(); // Receives some data back (blocks execution)
            }

        }

       
    }
}