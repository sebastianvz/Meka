using ID003ProtocolManager;
using System;
using System.IO.Ports;
using System.Threading;
using Prism.Events;
using Kiosko.Events;
using Kiosko.Models;

namespace Kiosko.Library.CashPayment.JCM
{


    public class JCMIvizion
    {
        Common.ServiceStatus ServiceStatus = new Common.ServiceStatus();
        private static Mutex mut = new Mutex();
        IEventAggregator _eventAggregator;
        Thread comThread; //Thread to poll bill acceptor
        SerialPort Port;  //Serial port for ID003 Communication  
        JCMModel _jcm_configuration;
        byte command;
        bool Active; //value that indicates if serial port is active or if it has been closed.
        int length; //length of status received or command sent
        byte[] buffer = new byte[255]; //Buffer to use the Dll commands
        byte[] status = new byte[255]; //Buffer to read data from the serial port
        ID003CommandCreater ComDll = new ID003CommandCreater(); //Declaring an instance of ID003CommandCreater which is a class in ID003ProtocolManager
        InventarioEfectivo inventory;
        Helpers.Timeout timeOut;

        public JCMIvizion(IEventAggregator ea, InventarioEfectivo Inv)
        {
            timeOut = new Helpers.Timeout(20000);
            inventory = Inv;
            _eventAggregator = ea;
            ReadConfiguration();
            command = 0x00;
            ea.GetEvent<Enable_Cash>().Subscribe(EnableCash, ThreadOption.BackgroundThread);
            ea.GetEvent<Disable_Cash>().Subscribe(DisableCash, ThreadOption.BackgroundThread);
            ServiceStatus.DeviceName = "JCM";
        }


        public void close()
        {
            Active = false;
            Port.Close();
            Port = null;


        }

        public Common.ServiceStatus getServiceStatus()
        {
            return ServiceStatus;
        }

        private void EnableCash(bool value)
        {
            
            byte inhibit = 0x00;
            SendInhibit(inhibit);
        }


        private void DisableCash(bool value)
        {
            byte inhibit = 0x01;
            SendInhibit(inhibit);


        }


        private void ReadConfiguration()
        {
            var file = PaymentServiceKiosk.Settings.Default.JCM_CONFIGURATION_PATH;

            _jcm_configuration = Helpers.Utilities.ReadFile<JCMModel>(file);

        }


        public void StarCom()
        {
            try

            {
                //Clicking Start button will get communication going
                Port = new SerialPort(_jcm_configuration.Port, 9600, Parity.Even, 8, StopBits.One);
                Active = true; //Polling is activated
                Port.Open(); //Opening com port for communication
                if (comThread != null)
                {
                    if (comThread.IsAlive || comThread.IsBackground || comThread.IsThreadPoolThread)
                    {
                        comThread.Abort();
                    }
                }

                comThread = new Thread(Poll); //starting new thread that calls Poll() method

                comThread.Start(); //Starting thread used to poll
                ServiceStatus.IsDone = true;
                ServiceStatus.error.HasError = false;

            }
            catch (Exception e)
            {
                ServiceStatus.IsDone = false;

                ServiceStatus.error.HasError = true;
                ServiceStatus.error.Message = e.Message;

            }

            

        }
        private void SendInhibit(byte cmd)
        {
            try
            {
                mut.WaitOne();
                ComDll.SetInhibit(buffer, cmd);
                length = (int)buffer[1];
                Port.Write(buffer, 0, length);
                System.Threading.Thread.Sleep(100);

                ServiceStatus.IsDone = true;//Wait for 100ms.
                mut.ReleaseMutex();
            }
            catch (Exception e)
            {

                ServiceStatus.IsDone = false;
                ServiceStatus.error.HasError = true;
                ServiceStatus.error.Message = e.Message;
            }



        }

        private void Poll()
        {

            mut.WaitOne();

            Actions.init(buffer, length, ComDll, Port); //Function to Initialize
            mut.ReleaseMutex();
            while (Active)
            {

               
                ComDll.StatusRequest(buffer); //Here Status request command is used. The dll constructs the command into buffer
                length = (int)buffer[1]; //the length is position 1 of the array. The length is casted from byte to int.
                mut.WaitOne();
                Port.Write(buffer, 0, length); //the buffer it is now written to the com port. offset is 0 and the length is given by length.
                Port.Read(status, 0, 255); //we now read the response of the Bill Acceptor
                mut.ReleaseMutex();
                System.Threading.Thread.Sleep(100); //Wait for 100ms.

                switch (status[2])
                {
                    //here we check the status of the bill acceptor.
                    case (byte)Status.PowerUP:

                        _eventAggregator.GetEvent<JCMEvents>().Publish(Status.PowerUP);

                        break;
                    case (byte)Status.Idling:
                        _eventAggregator.GetEvent<JCMEvents>().Publish(Status.Idling);
                        break;
                    case (byte)Status.Inhibit:
                        _eventAggregator.GetEvent<JCMEvents>().Publish(Status.Inhibit);
                        break;
                    case (byte)Status.BillinScrow://if there is a bill in escrow do the following.
                        mut.WaitOne();
                        Actions.Accept(buffer, length, ComDll, Port);
                        mut.ReleaseMutex();

                        switch (status[3])
                        {
                            case 0x61:
                                _eventAggregator.GetEvent<Cash_credited>().Publish(1000);
                                inventory.UpdateInventory(InventarioCash.Location.JCM, 1000, InventarioCash.TipoOperacion.sumar, 1);

                                break;
                            case 0x62:
                                _eventAggregator.GetEvent<Cash_credited>().Publish(2000);
                                inventory.UpdateInventory(InventarioCash.Location.JCM, 2000, InventarioCash.TipoOperacion.sumar, 1);
                                break;
                            case 0x63:
                                _eventAggregator.GetEvent<Cash_credited>().Publish(5000);
                                inventory.UpdateInventory(InventarioCash.Location.JCM, 5000, InventarioCash.TipoOperacion.sumar, 1);
                                break;
                            case 0x64:
                                _eventAggregator.GetEvent<Cash_credited>().Publish(10000);
                                inventory.UpdateInventory(InventarioCash.Location.JCM, 10000, InventarioCash.TipoOperacion.sumar, 1);
                                break;
                            case 0x65:
                                inventory.UpdateInventory(InventarioCash.Location.JCM, 20000, InventarioCash.TipoOperacion.sumar, 1);
                                _eventAggregator.GetEvent<Cash_credited>().Publish(20000);
                                break;
                            case 0x66:
                                _eventAggregator.GetEvent<Cash_credited>().Publish(50000);
                                inventory.UpdateInventory(InventarioCash.Location.JCM, 50000, InventarioCash.TipoOperacion.sumar, 1);
                                break;
                            case 0x67:
                                inventory.UpdateInventory(InventarioCash.Location.JCM, 100000, InventarioCash.TipoOperacion.sumar, 1);
                                _eventAggregator.GetEvent<Cash_credited>().Publish(100000);
                                break;
                        }
                        break;
                    case (byte)Status.Rejected:
                        _eventAggregator.GetEvent<JCMEvents>().Publish(Status.Rejected);
                        break;
                    case (byte)Status.StarckerFull:
                        _eventAggregator.GetEvent<JCMEvents>().Publish(Status.StarckerFull);
                        break;
                    case (byte)Status.StackerOpen:
                        _eventAggregator.GetEvent<JCMEvents>().Publish(Status.StackerOpen);
                        break;
                    case (byte)Status.JamInAcceptor:
                        _eventAggregator.GetEvent<JCMEvents>().Publish(Status.JamInAcceptor);
                        break;
                    case (byte)Status.JamInStacker:
                        _eventAggregator.GetEvent<JCMEvents>().Publish(Status.JamInStacker);
                        break;
                    case (byte)Status.Paused:
                        _eventAggregator.GetEvent<JCMEvents>().Publish(Status.Paused);
                        break;
                    case (byte)Status.Cheated:
                        _eventAggregator.GetEvent<JCMEvents>().Publish(Status.Cheated);
                        break;
                    case (byte)Status.MajorFailure:
                        _eventAggregator.GetEvent<JCMEvents>().Publish(Status.MajorFailure);
                        break;
                    case (byte)Status.ComError:
                        _eventAggregator.GetEvent<JCMEvents>().Publish(Status.ComError);
                        break;
                        /*All the other status conditions will be handled here (jams, rejections, returned notes, etc).
                         * Other cases will also include error handling.
                         */

                }

                switch (command)
                {
                    case 0xA2:
                        byte[] stat = new byte[255];
                        Port.DiscardInBuffer();
                        ComDll.RecyclerCurrentCountRequest(buffer);
                        length = (int)buffer[1]; //the length is position 1 of the array. The length is casted from byte to int.
                        mut.WaitOne();
                        Port.Write(buffer, 0, length); //the buffer it is now written to the com port. offset is 0 and the length is given by length.
                        System.Threading.Thread.Sleep(50); //Wait for 100ms.
                        Port.Read(stat, 0, 255); //we now read the response of the Bill Acceptor
                        mut.ReleaseMutex();
                        System.Threading.Thread.Sleep(100); //Wait for 100ms.
                        int temp = (int)stat[7];
                        //this.Invoke((MethodInvoker)delegate
                        //{
                        //    rc1.Text = stat[5].ToString();
                        //    rc2.Text = stat[7].ToString();
                        //});
                        command = 0x00;
                        break;


                }


            }
        }

    }
}