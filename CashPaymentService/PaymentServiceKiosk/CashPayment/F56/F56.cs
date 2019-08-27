using Kiosko.Events;
using Kiosko.Models;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using static Kiosko.Models.InventarioCash;

namespace Kiosko.Library.CashPayment.F56
{
    public class F56
    {
        Serial _serial;
        F56Model _f56_configuration;
        InventarioEfectivo Inventario;
        Common.ServiceStatus ServiceStatus = new Common.ServiceStatus();
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static F56 f56 = null;
        IEventAggregator _envt;
        public F56(IEventAggregator ea, InventarioEfectivo inv)
        {
            Inventario = inv;
            ServiceStatus.DeviceName = "F56";
            _envt = ea;
            ReadConfiguration();
            _serial = new Serial(_f56_configuration.port, ea, log);
            ea.GetEvent<F56PortResponse>().Subscribe(GetPortResponse, ThreadOption.BackgroundThread);
            ea.GetEvent<Devol_Cash>().Subscribe(BillCount, ThreadOption.BackgroundThread);
            Start();
        }

        public void stop()
        {

            _serial.Close();


        }

        public static F56 GetInstace(IEventAggregator ea, InventarioEfectivo inv)
        {
            if (f56 == null)
            {
                f56 = new F56(ea, inv);
            }

            return f56;
        }

        private void GetPortResponse(byte[] resp)
        {

        }

        public Common.ServiceStatus getServiceStatus()
        {
            return ServiceStatus;
        }

        private void ReadConfiguration()
        {
          
            var file = PaymentServiceKiosk.Settings.Default.F56_CONFIGURATION_PATH;
            var response = Helpers.Utilities.ReadFile<F56Model>(file);
            _f56_configuration = response;
        }





        /**
         * Device initialization and set _f56_configuration
         * */
        public void Start()
        {
            try
            {
              

                    // Bill configuration
                    byte[] l1 = { (byte)Decimal.Parse(_f56_configuration.bills.ElementAt(0).max_length), (byte)Decimal.Parse(_f56_configuration.bills.ElementAt(0).min_length) };
                    byte[] l2 = { (byte)Decimal.Parse(_f56_configuration.bills.ElementAt(1).max_length), (byte)Decimal.Parse(_f56_configuration.bills.ElementAt(1).min_length) };
                    byte[] l3 = new byte[] { 0x00, 0x00 };
                    byte[] l4 = new byte[] { 0x00, 0x00 };
                    byte[] t1 = { (byte)Decimal.Parse(_f56_configuration.bills.ElementAt(0).tickness) };
                    byte[] t2 = { (byte)Decimal.Parse(_f56_configuration.bills.ElementAt(1).tickness) };
                    byte[] t3 = new byte[] { 0x00 };
                    byte[] t4 = new byte[] { 0x00 };
                    byte[] fs = { 0x1c };

                    List<byte[]> byteList = new List<byte[]>()
                    {
                        Commands.F56_CMD_INIT_DEVICE , l1, l2 , l3 , l4 , t1,t2,t3,t4,fs
                    };

                    // COMMAND : F56_CMD_INIT_DEVICE + L1 + L2 + T1 + T2 + FS(0X1C)
                    byte[] command = new byte[] { };

                    byteList.ForEach(delegate (byte[] el)
                    {
                        command = Utilities.ConcatByteArray(command, el);
                    });

                    // Compute length of (COMMAND)
                    byte[] command_length = { 0x00, (byte)command.Length };

                    //Compute CRC [ LENGTH + DATA + ETX ]
                    //https://www.lammertbies.nl/comm/info/crc-calculation.html 

                   // command = Utilities.ConcatByteArray(command_length, command, Commands.F56_CMD_ETX);
                   // byte[] crc = Utilities.CalculateCRC(command);

                    // DATA_SEND = STX + DATA_LENGTH + COMMAND + ETX + CRC

                    //byte[] dataSend = Utilities.ConcatByteArray(Commands.F56_CMD_STX, command, crc);


                    HandleCommand(command);

                   
                    //   BillCount(PayoutAmount);

                

            }
            catch (Exception e)
            {
                log.Error(e.Message);
            }

        }


        public void MechaReset()
        {
            HandleCommand(Commands.F57_CMD_MECH_RESET);
        }


        public void BillCount(List<Efectivo> PayoutAmount)
        {
            //_serial.Send(Commands.F56_CMD_ENQ);

            //var response = _serial.GetResponse();

            byte[] billDevolution = ProccessBillDevolution(PayoutAmount);

            if (billDevolution == null)
            {
                return;
            }

            BillTransportCommand cmd = new BillTransportCommand()
            {
                DH0 = 0x60,
                DH1 = 0x09,
                DH2 = 0x15,
                ODR = 0XE4,
                N1 = new byte[] { billDevolution[0], billDevolution[1] },
                N2 = new byte[] { billDevolution[2], billDevolution[3] },
                N3 = new byte[] { billDevolution[4], billDevolution[5] },
                N4 = new byte[] { billDevolution[6], billDevolution[7] },
                R1 = new byte[] { 0x30, 0x33 },
                R2 = new byte[] { 0x30, 0x33 },
                R3 = new byte[] { 0x30, 0x33 },
                R4 = new byte[] { 0x30, 0x33 },
                P1 = 0x00,
                P2 = 0x00,
                P3 = 0x00,
                P4 = 0x00,
                FS = 0x1C
            };

            // Basic request frame => [ DH0 DH1 DH2 ODR N1 N2 N3 N4 R1 R2 R3 R4 P1 P2 P3 P4 FS ]
            byte[] command =
            {
                cmd.DH0 , cmd.DH1 , cmd.DH2 , cmd.ODR, cmd.N1[0] ,cmd.N1[1] , cmd.N2[0], cmd.N2[1] ,
                cmd.N3[0], cmd.N3[1], cmd.N4[0] , cmd.N4[1], cmd.R1[0] , cmd.R1[1] , cmd.R2[0] , cmd.R2[1],
                cmd.R3[0] , cmd.R3[1] , cmd.R4[0] , cmd.R4[1] , cmd.P1 , cmd.P2 , cmd.P3, cmd.P4, cmd.FS
            };

            HandleCommand(command);

            ServiceStatus.IsDone = true;
        }

        public void BillTransporToFront()
        {
            HandleCommand(Commands.F56_CMD_BILL_TRANSPORT);
        }

        /**
         *  Process the quantity for devolution in bills as a bit array 
         *  Ni = [] []
         *  [N1 , N2 , N3 , N4]
         *  
         *  Read "F65 Dlevel Spec RoHs E05.pdf" Pag. 26
         *  
         *  Number of bills to count
         *  var: byte[] billDevolutionTemplate
         *  number  [ 0 , 1 , 2 , 3 , 4 , 5 , 6 , 7 , 8 , 9 ]
         *  code hex[30, B1 , B2 , B3, B4, 35, 36 , B7 , B8 , 39]    
         * 
         * */
        public byte[] ProccessBillDevolution(List<Efectivo> PayoutAmount)
        {
            try
            {

                byte[] billDevolution = new byte[] { 0x30, 0x30, 0x30, 0x30, 0x030, 0x30, 0x30, 0x30 };
                int[] billDevoltionInt = new int[] { 0, 0, 0, 0 };
                byte[] billDevolutionTemplate = new byte[] { 0x30, 0xB1, 0xB2, 0x33, 0xB4, 0x35, 0x36, 0xB7, 0xB8, 0x39 };
                //double _amount = quantity;
                int i = _f56_configuration.bills.Count() - 1;

                List<Efectivo> devolution = PayoutAmount.Where(c => c.Location == InventarioCash.Location.F56).ToList();
                foreach (Efectivo efe in devolution)
                {
                    Inventario.UpdateInventory(InventarioCash.Location.F56, efe.Value, TipoOperacion.restar, efe.Inventory);

                }

                _envt.GetEvent<UpdateInventory>().Publish(true);
                billDevoltionInt[0] = devolution.Where(c => c.Value == 2000).FirstOrDefault().Inventory;
                billDevoltionInt[1] = devolution.Where(c => c.Value == 5000).FirstOrDefault().Inventory;


                for (int j = 0; j < billDevoltionInt.Length; j++)
                {
                    //Split billDevolutionInt intwo integers
                    // Ni [ a , b ]
                    int c = billDevoltionInt[j];
                    int a = c < 9 ? 0 : c / 10;
                    int b = c % 10;
                    billDevolution[j * 2] = billDevolutionTemplate[a];
                    billDevolution[j * 2 + 1] = billDevolutionTemplate[b];
                }
                return billDevolution;
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                ServiceStatus.IsDone = true;
                ServiceStatus.error.HasError = true;
                ServiceStatus.error.Message = " [F56] Bill Count " + e.Message;
            }

            return null;
        }


        private byte[] CalculateMessage(byte[] command)
        {

            byte[] command_length = { 0x00, (byte)command.Length };

            byte[] command2 = Utilities.ConcatByteArray(command_length, command, Commands.F56_CMD_ETX);
            byte[] crc = Utilities.CalculateCRC(command2);

            byte[] dataSend = Utilities.ConcatByteArray(Commands.F56_CMD_STX, command2, crc);
            return dataSend;

        }
        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }


        private void HandleCommand(byte[] command)
        {
            try
            {
                _serial.Send(Commands.F56_CMD_ENQ);
                
                var response = _serial.GetResponse();
               // Console.WriteLine(ByteArrayToString(response)+ "\n");
               
                if (response.SequenceEqual(Commands.F56_CMD_ENQ_OK))
                {
                    _serial.Send(CalculateMessage(command));
                    
                    response = _serial.GetResponse();
                   // Console.WriteLine(ByteArrayToString(response)+"\n");
                    if (response.SequenceEqual(Commands.F56_CMD_ENQ_OK))
                    {
                       // _serial.SendWithExpected(Commands.F56_CMD_ENQ_OK, Commands.F56_CMD_ENQ);
                        
                        response = _serial.GetResponse();
                
                      //  Console.WriteLine(ByteArrayToString(response));
                        if (response.SequenceEqual(Commands.F56_CMD_ENQ))
                        {
                            _serial.Send(Commands.F56_CMD_ENQ_OK);
                        
                            response = _serial.GetResponse();
                            // Console.WriteLine(ByteArrayToString(response));
                        }else
                        {
                            response = _serial.GetResponse();
                           // Console.WriteLine(ByteArrayToString(response));
                            if (response.SequenceEqual(Commands.F56_CMD_ENQ))
                            {
                                _serial.Send(Commands.F56_CMD_ENQ_OK);

                                response = _serial.GetResponse();
                                // Console.WriteLine(ByteArrayToString(response));
                            }

                        }
                    }
                }
                else
                {
                    _serial.Send(Commands.F56_CMD_ENQ);
                    response = _serial.GetResponse();
                    Console.WriteLine(Encoding.UTF8.GetString(response));
                    _serial.Send(CalculateMessage(command));
                    response = _serial.GetResponse();
                    Console.WriteLine(Encoding.UTF8.GetString(response));
                    if (response.SequenceEqual(Commands.F56_CMD_ENQ_OK))
                    {
                        _serial.Send(Commands.F56_CMD_ENQ_OK);
                        response = _serial.GetResponse();
                        Console.WriteLine(Encoding.UTF8.GetString(response));
                        if (response.SequenceEqual(Commands.F56_CMD_ENQ))
                        {
                            _serial.Send(Commands.F56_CMD_ENQ_OK);
                            response = _serial.GetResponse();
                            Console.WriteLine(Encoding.UTF8.GetString(response));
                        }
                    }

                }

            }
            catch (Exception e)
            {
                ServiceStatus.IsDone = true;
                ServiceStatus.error.HasError = true;
                ServiceStatus.error.Message = " [F56] " + e.Message;
            }

        }
    }
}