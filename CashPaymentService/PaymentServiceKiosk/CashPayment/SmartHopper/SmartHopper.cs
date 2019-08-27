using Kiosko.Events;
using Kiosko.Library.CashPayment.SmartHopper.SSP;
using Kiosko.Models;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Windows.Forms;
using static Kiosko.Models.InventarioCash;

namespace Kiosko.Library.CashPayment.SmartHopper
{


    public class SmartHopper
    {

        Mutex mut = new Mutex();
        IEventAggregator _eventAggregator;
        bool Running = false;
        int pollTimer = 250; // timer in ms
        CHopper Hopper;
        bool bFormSetup = false;
        private System.Windows.Forms.Timer timer1;
        System.Windows.Forms.Timer reconnectionTimer = new System.Windows.Forms.Timer();
        TextBox textBox1;
        bool CanSendCommmand = false;
        InventarioEfectivo inventory;
        Thread thr;
        Common.ServiceStatus ServiceStatus = new Common.ServiceStatus();
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public SmartHopper(IEventAggregator ea, InventarioEfectivo Inve)
        {


            inventory = Inve;

            _eventAggregator = ea;
            timer1 = new System.Windows.Forms.Timer();
            timer1.Interval = pollTimer;
            timer1.Tick += Timer1_Tick;
            reconnectionTimer.Tick += new EventHandler(reconnectionTimer_Tick);
            reconnectionTimer.Start();
            textBox1 = new TextBox();
            Hopper = new CHopper(_eventAggregator, inventory);
            ea.GetEvent<Enable_Cash>().Subscribe(EnableCash, ThreadOption.BackgroundThread);
            ea.GetEvent<Disable_Cash>().Subscribe(DisableCoinMech, ThreadOption.BackgroundThread);
            ea.GetEvent<Devol_Cash>().Subscribe(DoPayment, ThreadOption.BackgroundThread);
            ServiceStatus.DeviceName = "SMARTHOPPER";
            ServiceStatus.error.HasError = true;
            ServiceStatus.error.Message = "no ha inicializado";
            thr = new Thread(MainLoop);

        }

        public Common.ServiceStatus getServiceStatus()
        {
            return ServiceStatus;
        }


        public bool SetCoinLevel(int Coin, int amount)
        {

            try
            {

                mut.WaitOne();
                string Currency = "COP";

                log.Info("Set Coin Level " + Coin);
                //Hopper.SetCoinLevelsByCoin(Coin, Currency.ToCharArray(), 0, log);

                //Thread.Sleep(500);
               

                Hopper.SetCoinLevelsByCoin(Coin, Currency.ToCharArray(), Convert.ToInt16(amount), log);

                mut.ReleaseMutex();

                return true;
            }catch(Exception Ex
            )
            {
                log.Error(Ex.ToString(), Ex);
                return false;

            }
        }

        private void EnableCash(bool value)
        {

           

            mut.WaitOne();


          
            var val = Hopper.EnableCoinMech(log);

            mut.ReleaseMutex();

        }

        public void StartThread()
        {

            thr.Start();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }



        private void reconnectionTimer_Tick(object sender, EventArgs e)
        {
            reconnectionTimer.Enabled = false;
        }

        public void DisableCoinMech(bool val)
        {
            mut.WaitOne();
            Hopper.DisableCoinMech();
            mut.ReleaseMutex();
        }

      
        public void DoPayment(List<Efectivo> PayoutAmount)
        {
            string Currency = "COP";

            mut.WaitOne();
            Hopper.DisableCoinMech();
            

            Efectivo payment = PayoutAmount.Where(c => c.Location == InventarioCash.Location.SMARTHOPPER).FirstOrDefault();

            Hopper.PayoutAmount(payment.Value, Currency.ToCharArray());

            //Hopper.EnableHopper();
            mut.ReleaseMutex();
        }


    


        public void MainLoop()
        {


            textBox1.Clear();
            textBox1.Refresh();
            Hopper.CommandStructure.ComPort = "COM7";
            Hopper.CommandStructure.SSPAddress = 16;
            Hopper.CommandStructure.Timeout = 2000;
            Hopper.CommandStructure.RetryLevel = 3;

            // First connect to the hopper
            if (ConnectToHopper(10, 3))
            {

                Running = true;
                ServiceStatus.error.HasError = false;
                ServiceStatus.error.Message = "";
                ServiceStatus.IsDone = true;
                log.Info("\r\nPoll Loop\r\n"
                + "*********************************\r\n");

               
            }


            Hopper.DisableCoinMech();
            Hopper.Reset();
           // Hopper.EnableHopper();

            // This loop won't run until the hopper is connected
            while (Running)
            {
                mut.WaitOne();
                // poll the hopper
                if (!Hopper.DoPoll(log).Success)
                {
                    // If the poll fails, try to reconnect
                    Console.WriteLine("Attempting to reconnect");
                    if (!ConnectToHopper(10, 3))
                    {

                        ServiceStatus.error.HasError = true;
                        ServiceStatus.error.Message = " If it fails after 5 attempts, exit the loop";
                        ServiceStatus.IsDone = false;
                        // If it fails after 5 attempts, exit the loop
                        Running = false;
                    }
                }
                mut.ReleaseMutex();
                Thread.Sleep(250);


            }

            //close com port
            Hopper.SSPComms.CloseComPort();


        }

        private bool ConnectToHopper(int attempts, int interval)
        {
            // setup the timer
            reconnectionTimer.Interval = interval * 1000; // for ms

            // run for number of attempts specified
            for (int i = 0; i < attempts; i++)
            {
                // reset timer
                reconnectionTimer.Enabled = true;

                // close com port in case it was open
                Hopper.SSPComms.CloseComPort();

                // turn encryption off for first stage
                Hopper.CommandStructure.EncryptionStatus = false;

                // if the key negotiation is successful then set the rest up
                if (Hopper.OpenComPort(log) && Hopper.NegotiateKeys(log) == true)
                {
                    Hopper.CommandStructure.EncryptionStatus = true; // now encrypting
                    // find the max protocol version this hopper supports
                    byte maxPVersion = FindMaxProtocolVersion();
                    if (maxPVersion >= 6)
                    {
                        Hopper.SetProtocolVersion(maxPVersion, log);
                    }
                    else
                    {
                        log.Error("This program does not support hoppers under protocol 6!");
                        //MessageBox.Show("This program does not support hoppers under protocol 6!", "ERROR");
                        return false;
                    }
                    // get info from the hopper and store useful vars
                    Hopper.HopperSetupRequest(log);
                    // Get serial number.
                    Hopper.GetSerialNumber(log);
                    // check unit is valid type
                    if (!IsUnitValid(Hopper.UnitType))
                    {

                        log.Error("Unsupported type shown by SMART Hopper, this SDK supports the SMART Hopper only");
                        //MessageBox.Show();
                        Application.Exit();
                        return false;
                    }
                    // inhibits, this sets which channels can receive coins
                    Hopper.SetInhibits(log);
                    // enable, this allows the hopper to operate
                    Hopper.EnableHopper(log);

                    Hopper.SetHopperOptions(0x01, 0x01, 0x01, 0x00, log);

                    Hopper.GetHopperOptions(log);

                    return true;
                }
                while (reconnectionTimer.Enabled)
                {
                    Application.DoEvents();
                    Thread.Sleep(1); // Yield to free up CPU
                }
            }
            return false;
        }

        private bool IsUnitValid(char unitType)
        {
            if (unitType == (char)0x03) // 0x03 is Hopper, only Hopper supported here
                return true;
            return false;
        }

        // This function finds the maximum protocol version that a hopper supports. To do this
        // it attempts to set a protocol version starting at 6 in this case, and then increments the
        // version until error 0xF8 is returned from the hopper which indicates that it has failed
        // to set it. The function then returns the version number one less than the failed version.
        private byte FindMaxProtocolVersion()
        {
            // not dealing with protocol under level 6
            // attempt to set in hopper
            byte b = 0x06;
            while (true)
            {
                if (!Hopper.SetProtocolVersion(b) || b > 20)
                    return 0x06; // return default
                // If it fails then it can't be set so fall back to previous iteration and return it
                if (Hopper.CommandStructure.ResponseData[0] == CCommands.SSP_RESPONSE_FAIL)
                    return --b;
                b++;
            }
        }
    }
}