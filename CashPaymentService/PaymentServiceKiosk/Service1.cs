using Kiosko.Events;
using Kiosko.Library.CashPayment.F56;
using Kiosko.Library.CashPayment.JCM;
using Kiosko.Library.CashPayment.SmartHopper;
using Kiosko.Models;
using KioskoAdmin.Models;
using Newtonsoft.Json;
using PaymentServiceKiosk.CashPayment;
using Prism.Events;
using SocketLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Kiosko.Models.ComClass;
using static Kiosko.Models.InventarioCash;


namespace PaymentServiceKiosk
{
    public partial class Service1 : ServiceBase
    {

        EventAggregator evnt = new EventAggregator();
        SmartHopper smrt;
        JCMIvizion jcm;
        F56 f56;
        InventarioEfectivo inventory;
        int BufferPaymentReceived;
        public int PaymentExpected;
        Kiosko.Helpers.Timeout timeout;
        bool CompletedTransaction = false;
        status_cash current_status = status_cash.fail;
        machine_status mach_status =   machine_status.idle;
        System.Timers.Timer timer1;
        Thread trd;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Service1()
        {
            InitializeComponent();
            evnt.GetEvent<Cash_credited>().Subscribe(CashReceived, ThreadOption.BackgroundThread);
            timer1 = new System.Timers.Timer();
            timer1.Interval = 120000;
            timer1.Elapsed += Timer1_Elapsed;
            timer1.Enabled = false;
            timer1.Start();
            evnt.GetEvent<UpdateInventory>().Subscribe(UpdateInventory, ThreadOption.BackgroundThread);
            evnt.GetEvent<CoinDispensed>().Subscribe(CashDispensed, ThreadOption.BackgroundThread);

        }


        private void UpdateInventory(bool value)
        {
            inventory.SaveInventory();

            inventory.UploadInventory(ComClass.operation_types.purchase.ToString("g"), null);


        }

        private void CashDispensed(bool value)
        {
            try
            {
                inventory.UploadInventory(ComClass.operation_types.purchase.ToString("g"), null);
            }catch(SystemException E)
            {
                log.Error(E);

            }

        }
        private void Timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                evnt.GetEvent<Disable_Cash>().Publish(true);
                current_status = status_cash.time_out;

                mach_status = machine_status.idle;

                /////////INICIAR DEVOLUCION DE DINERO ADQUIRIDO;


                List<Efectivo> dev = CalcularDevuelta(BufferPaymentReceived);

                evnt.GetEvent<Devol_Cash>().Publish(dev);

                BufferPaymentReceived = 0;
                timer1.Enabled = false;
            }catch(SystemException E)
            {
                log.Error(E.Message);

            }
        }

        protected override void OnStart(string[] args)
        {

            trd = new Thread(process);
            trd.Start();
            //this.process();
        }

     

        public void CashReceived(int value)
        {
            timer1.Stop();
            timer1.Start();
            BufferPaymentReceived += value;
            if (BufferPaymentReceived == PaymentExpected)
            {
                ////DEJAR DE RECIBIR DINERO Y PROCEDER A DEVOLVER SI ES EL CASO;
                evnt.GetEvent<Disable_Cash>().Publish(true);
                current_status = status_cash.ok;
                mach_status = machine_status.idle;
                BufferPaymentReceived = 0;
                timer1.Enabled = false;


                inventory.UploadInventory(ComClass.operation_types.purchase.ToString("g"), null);

                /////



            }
            else if (BufferPaymentReceived > PaymentExpected)
            {
                current_status = status_cash.ok;
                mach_status = machine_status.idle;
                evnt.GetEvent<Disable_Cash>().Publish(true);
                int Devuelta = BufferPaymentReceived - PaymentExpected;

                List<Efectivo> dev = CalcularDevuelta(Devuelta);
               // UpdateDevuelta(dev);
                evnt.GetEvent<Devol_Cash>().Publish(dev);

                CompletedTransaction = true;
                BufferPaymentReceived = 0;
                timer1.Enabled = false;
                inventory.UploadInventory(ComClass.operation_types.purchase.ToString("g"), null);


            }



        }

        public void UpdateDevuelta(List<Efectivo> efectivo)
        {
            foreach (Efectivo efect in efectivo)
            {
                if (efect.Location == "F56")
                {
                    inventory.GetInventario().Where(c => c.Location == efect.Location && c.Value == efect.Value).FirstOrDefault().Inventory -= efect.Inventory;
                }
            }
        }


        public void AddMoney(List<Efectivo> efectivo, User user)
        {

            foreach (Efectivo efect in efectivo)
            {
                if (efect.Location == "SMARTHOPPER")
                {
                    if (smrt.getServiceStatus().error.HasError != true)
                    {
                        inventory.GetInventario().Where(c => c.Location == efect.Location && c.Value == efect.Value).FirstOrDefault().Inventory += efect.Inventory;

                        smrt.SetCoinLevel(efect.Value, efect.Inventory);


                       
                    }
                }
                else
                {
                    inventory.GetInventario().Where(c => c.Location == efect.Location && c.Value == efect.Value).FirstOrDefault().Inventory += efect.Inventory;

                }

            }
            inventory.SaveInventory();
            inventory.UploadInventory(ComClass.operation_types.load.ToString("g"),user);


        }


        public void RemoveMoney(List<Efectivo> efectivo, User user)
        {

            foreach (Efectivo efect in efectivo)
            {
                if (efect.Location == "SMARTHOPPER")
                {
                    if (smrt.getServiceStatus().error.HasError != true)
                    {
                        inventory.GetInventario().Where(c => c.Location == efect.Location && c.Value == efect.Value).FirstOrDefault().Inventory -= efect.Inventory;

                        smrt.SetCoinLevel(efect.Value, inventory.GetInventario().Where(c => c.Location == efect.Location && c.Value == efect.Value).FirstOrDefault().Inventory);


                    }
                }
                else
                {
                    inventory.GetInventario().Where(c => c.Location == efect.Location && c.Value == efect.Value).FirstOrDefault().Inventory -= efect.Inventory;

                }

            }
            inventory.SaveInventory();
            inventory.UploadInventory(ComClass.operation_types.withdrawal.ToString("g"), user);


        }



        public void SetMoney(List<Efectivo> efectivo, User user)
        {
            foreach(Efectivo efect in efectivo)
            {
                if (efect.Location == "SMARTHOPPER")
                {
                    if (smrt.getServiceStatus().error.HasError != true)
                    {
                        smrt.SetCoinLevel(efect.Value, efect.Inventory);
                    
                    inventory.GetInventario().Where(c => c.Location == efect.Location && c.Value == efect.Value).FirstOrDefault().Inventory = efect.Inventory;
                    }
                }
                else
                {
                    inventory.GetInventario().Where(c => c.Location == efect.Location && c.Value == efect.Value).FirstOrDefault().Inventory = efect.Inventory;

                }

            }
            inventory.SaveInventory();
            inventory.UploadInventory(ComClass.operation_types.setmoney.ToString("g"), user);

        }

        public List<Efectivo> CalcularDevuelta(int Valor)
        {
            int buffervalor = 0;

            Valor = (int)Math.Ceiling(Valor / 50d) * 50;


            List<Efectivo> Vueltas = new List<Efectivo>();

            Efectivo val1 = inventory.GetInventario().OrderByDescending(c => c.Value).Where(c => c.Location == InventarioCash.Location.F56).FirstOrDefault();
            int res;
            int value = Math.DivRem(Valor, val1.Value, out res);
            if (val1.Inventory >= value)
            {
                Vueltas.Add(new Efectivo() { Inventory = value, Type = InventarioCash.Type.Bill, Location = InventarioCash.Location.F56, Value = val1.Value });
                buffervalor = res;
            }
            else
            {
                Vueltas.Add(new Efectivo() { Inventory = val1.Inventory, Type = InventarioCash.Type.Bill, Location = InventarioCash.Location.F56, Value = val1.Value });
                buffervalor = Valor - (val1.Inventory * val1.Value);

            }

            Efectivo val2 = inventory.GetInventario().OrderByDescending(c => c.Value).Where(c => c.Location == InventarioCash.Location.F56 && c.Value != val1.Value).FirstOrDefault();
            value = Math.DivRem(buffervalor, val2.Value, out res);
            if (val2.Inventory >= value)
            {
                Vueltas.Add(new Efectivo() { Inventory = value, Type = InventarioCash.Type.Bill, Location = InventarioCash.Location.F56, Value = val2.Value });
                buffervalor = res;
            }
            else
            {
                Vueltas.Add(new Efectivo() { Inventory = val2.Inventory, Type = InventarioCash.Type.Bill, Location = InventarioCash.Location.F56, Value = val2.Value });
                buffervalor = Valor - (val2.Inventory * val2.Value);

            }

            Vueltas.Add(new Efectivo() { Inventory = val2.Inventory, Type = InventarioCash.Type.Coin, Location = InventarioCash.Location.SMARTHOPPER, Value = buffervalor });


            return Vueltas;

        }
        private void HandlePaymentCash(int quantity)
        {
            
            current_status = status_cash.waiting;

            mach_status = machine_status.receiving;
            CompletedTransaction = false;
            BufferPaymentReceived = 0;
            PaymentExpected = quantity;
            evnt.GetEvent<Enable_Cash>().Publish(true);
            timer1.Enabled = true;
      

        }

        public void Test()
        {

          
            inventory = new InventarioEfectivo();
            f56 = new F56(evnt, inventory);
            Thread.Sleep(2000);
            //jcm = new JCMIvizion(evnt, inventory);
            //jcm.StarCom();

            //Thread.Sleep(2000);

            //smrt = new SmartHopper(evnt, inventory);
            //smrt.StartThread();

            Thread.Sleep(2000);





            var _jcm = jcm.getServiceStatus();
            var _f56 = f56.getServiceStatus();
            var _smart = smrt.getServiceStatus();


        }

        public void process()
        {


            //this.CalcularDevuelta(1670);

            inventory = new InventarioEfectivo();

            f56 = new F56(evnt, inventory);
            log.Debug("F56 Started");

            Thread.Sleep(1000);

            jcm = new JCMIvizion(evnt, inventory);
            jcm.StarCom();
            log.Debug("JCM Started");
            Thread.Sleep(1000);

            smrt = new SmartHopper(evnt, inventory);
            smrt.StartThread();
            log.Debug("smrt Started");
            Thread.Sleep(1000);




            //



            var _jcm = jcm.getServiceStatus();
            var _f56 = f56.getServiceStatus();
            var _smart= smrt.getServiceStatus();

            evnt.GetEvent<Disable_Cash>().Publish(true);


            if (_jcm.error.HasError || _f56.error.HasError || _smart.error.HasError)
            {
                mach_status = machine_status.error;
            }
            if (_jcm.IsDone && _f56.IsDone )
            {
                mach_status = machine_status.idle;
                
            }
            log.Debug("Service started Started");
            using (var listener = new SocketListener(1337)) // Start listening
            {
                log.Debug("Socket Started");

                for (;;)
                {
                    try
                    {
                        using (var remote = listener.Accept()) // Accepts a connection (blocks execution)
                        {


                            var data = remote.Receive(); // Receives data (blocks execution)
                            ComClass deserializedProduct = JsonConvert.DeserializeObject<ComClass>(data);


                            if (deserializedProduct.funciones == ComClass.function.cash_handling)
                            {


                                if (mach_status == machine_status.idle)
                                {
                                    HandlePaymentCash(deserializedProduct.Value);
                                    deserializedProduct.result = ComClass.status_cash.ok;
                                }
                                else
                                {
                                    deserializedProduct.result = ComClass.status_cash.fail;

                                }
                            } else if (deserializedProduct.funciones == function.set_money)
                            {

                                this.SetMoney(deserializedProduct.Inventario, deserializedProduct.user);
                                deserializedProduct.result = status_cash.ok;

                            } 
                            else if(deserializedProduct.funciones== function.add_money)
                            {

                                this.AddMoney(deserializedProduct.Inventario, deserializedProduct.user);
                                deserializedProduct.result = status_cash.ok;

                            }
                            else if (deserializedProduct.funciones == ComClass.function.system_status)
                            {

                                deserializedProduct.status = mach_status;

                            }
                            else if( deserializedProduct.funciones == function.get_devices_status)
                            {

                                deserializedProduct.DeviceStatus = new List<Common.ServiceStatus>();
                                deserializedProduct.DeviceStatus.Add(f56.getServiceStatus());
                                deserializedProduct.DeviceStatus.Add(smrt.getServiceStatus());
                                deserializedProduct.DeviceStatus.Add(jcm.getServiceStatus());

                            }
                            else if (deserializedProduct.funciones == ComClass.function.operation_status)
                            {
                                deserializedProduct.result = current_status;

                            }else if(deserializedProduct.funciones == ComClass.function.get_money)
                            {
                                deserializedProduct.Inventario = inventory.GetInventario();
                                
                            }
                            else if (deserializedProduct.funciones == ComClass.function.remove_money)
                            {
                                this.RemoveMoney(deserializedProduct.Inventario, deserializedProduct.user);
                                deserializedProduct.result = status_cash.ok;

                            }
                            remote.Send(JsonConvert.SerializeObject(deserializedProduct)); // Sends the received data back
                        }

                    }catch(SystemException E)
                    {

                        log.Error(E.Message);
                       

                    }
                    }
            }

        }

        protected override void OnStop()
        {

            trd.Suspend();
            trd = null;
            f56.stop();


            ///DISCONNECT DEVICES.
            ///





        }
    }
}
