using Kiosko.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;

namespace Kiosko.Libraries.DoorLocker
{
    public class DoorLocker
    {
        SerialPort port;
        Common.ServiceStatus ServiceStatus = new Common.ServiceStatus();
        bool dataProcessed = false;
        bool hasError = false;
        byte[] dataResponse;
        KioskoHardware.DoorLocker _doorLocker;
        public DoorLocker(KioskoHardware.DoorLocker doorLocker)
        {
            _doorLocker = doorLocker;
            port = new SerialPort
            {
                PortName = _doorLocker.COMPort,
                Parity = Parity.None,
                BaudRate = 19200,
                DataBits = 8,
                StopBits = StopBits.One
            };

            try
            {
                System.Threading.Thread.Sleep(100);
                //port.Open();

            }
            catch (Exception e)
            {
                ServiceStatus.error.HasError = true;
                ServiceStatus.error.Message = e.Message;
            }
        }


        public Common.ServiceStatus getServiceStatus()
        {
            if (ServiceStatus.error.HasError)
                ServiceStatus.error.Message = "[DoorLocker]["+_doorLocker.Name+ " - " +_doorLocker.COMPort+"]" + ServiceStatus.error.Message;

            return ServiceStatus;
        }

        private byte[] GerenarateCommand(byte Channel, byte cmd)
        {
            try
            {
                byte[] result = new byte[] { 0xF5, Channel, cmd, 0x00, 0x01, 0x5F };
                long longSum = result.Sum(x => (long)x);
                byte d = ((byte)longSum);
                var res = result.Concat(new byte[] { d, 0x00 }).ToArray();
                return res;
            }
            catch(Exception e)
            {
                ServiceStatus.error.HasError = true;
                ServiceStatus.error.Message = e.Message;
            }

            return new byte[] { };
        }
       

        public void OpenBill(byte Channel1, byte Channel2)
        {
            port.Open();
            HandleCommand(Commands.CMD_OPEN, Channel1);
            Thread.Sleep(500);

            HandleCommand(Commands.CMD_OPEN, Channel2);
            port.Close();


        }

        public void Open(byte Channel)
        {
            try
            {
                port.Open();
                HandleCommand(Commands.CMD_OPEN, Channel);
                Thread.Sleep(400);
                HandleCommand(Commands.CMD_OPEN, Channel);
                port.Close();
            }           

            catch(Exception e)
            {
                ServiceStatus.error.HasError = true;
                ServiceStatus.error.Message = e.Message;
            }
        }

        public void Close()
        {
            port.Close();
        }
        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }
        public bool IsOpen(byte channel)
        {
            port.Open();
            int count = 0;
            dataProcessed = false;
            dataResponse = new byte[] { };
            HandleCommand(Commands.CMD_GET_STATUS, channel);
            Thread.Sleep(100);
            int bytes = port.BytesToRead;
            byte[] buffer = new byte[bytes];
            while (port.BytesToRead < 5)
            {
               
            }
            port.Read(buffer, 0, bytes);
            dataResponse = buffer;
            dataProcessed = true;
            port.Close();
            byte[] cmd_response = dataResponse;           
            dataProcessed = false;
            try
            {
                bool isOpen = CheckDoorStatus(cmd_response, '0');
                if (!isOpen)
                {
                    if (CheckDoorStatus(cmd_response, '1'))
                    {
                        isOpen = false;
                    }
                    else
                    {
                        ServiceStatus.error.HasError = true;
                        ServiceStatus.error.Message = "Current Secuence: [" + ByteArrayToString(cmd_response) + "  " + Convert.ToString(cmd_response[7], 2).PadLeft(8, '0') + " ] is not equals to CMD_IS_CLOSE";
                    }
                }
                return isOpen;

            } catch (Exception E)
            {
                ServiceStatus.error.HasError = true;
                ServiceStatus.error.Message = E.Message;
                return false;
            }
        }

        public bool CheckDoorStatus(byte[] response, char status)
        {
            string yourByteString = Convert.ToString(response[7], 2).PadLeft(8, '0');
            char[] charity = yourByteString.ToCharArray();
            return (charity[7] == status);      
        }
        public void HandleCommand(byte Command, byte Channel)
        {

          byte[] commando = GerenarateCommand(Channel, Command);

            try
            {
                port.Write(commando, 0, commando.Length);
            }
            catch (Exception e)
            {
                ServiceStatus.error.HasError = true;
                ServiceStatus.error.Message = e.Message;
            }
        }

        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            
            SerialPort port = (SerialPort)sender;


            int bytes = port.BytesToRead;
            byte[] buffer = new byte[bytes];

            if (port.BytesToRead > 1)
            {
                port.Read(buffer, 0, bytes);
            }
            if (buffer.Length > 1)
            {
                dataResponse = buffer;
                dataProcessed = true;

            }
            port.Close();


        }
    }
}