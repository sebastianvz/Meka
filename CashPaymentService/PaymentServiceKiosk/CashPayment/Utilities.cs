using Kiosko.Events;
using Kiosko.Library.CashPayment.F56;
using log4net;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Web;

namespace Kiosko.Library.CashPayment
{
    public class Utilities
    {
        public static byte[] ConcatByteArray(byte[] a, byte[] b, byte[] c = null)
        {
            var r = a.Concat(b);
            if (c != null)
                r = r.Concat(c);

            return r.ToArray();

        }

        public static byte[] CalculateCRC(byte[] input)
        {
            var crc16Kermit = new Crc16(Crc16Mode.CcittKermit);
            return crc16Kermit.ComputeChecksumBytes(input);
        }

        [DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern int memcmp(byte[] b1, byte[] b2, long count);

        public static bool ByteArrayCompare(byte[] b1, byte[] b2)
        {
            // Validate buffers are the same length.
            // This also ensures that the count does not exceed the length of either buffer.  
            return b1.Length == b2.Length && memcmp(b1, b2, b1.Length) == 0;
        }


        public static int[] SpllitDigits(int num, bool reverse = true)
        {
            List<int> listOfInts = new List<int>();
            while (num > 0)
            {
                listOfInts.Add(num % 10);
                num = num / 10;
            }
            if (reverse)
                listOfInts.Reverse();
            return listOfInts.ToArray();
        }

        public static void UpdateRemoteMoneyQuantity()
        {

        }


    }

    public enum Crc16Mode : ushort { Standard = 0xA001, CcittKermit = 0x8408 }


    public class Crc16
    {
        static ushort[] table = new ushort[256];

        public ushort ComputeChecksum(params byte[] bytes)
        {
            ushort crc = 0;
            for (int i = 0; i < bytes.Length; ++i)
            {
                byte index = (byte)(crc ^ bytes[i]);
                crc = (ushort)((crc >> 8) ^ table[index]);
            }
            return crc;
        }

        public byte[] ComputeChecksumBytes(params byte[] bytes)
        {
            ushort crc = ComputeChecksum(bytes);
            return BitConverter.GetBytes(crc);
        }

        public Crc16(Crc16Mode mode)
        {
            ushort polynomial = (ushort)mode;
            ushort value;
            ushort temp;
            for (ushort i = 0; i < table.Length; ++i)
            {
                value = 0;
                temp = i;
                for (byte j = 0; j < 8; ++j)
                {
                    if (((value ^ temp) & 0x0001) != 0)
                    {
                        value = (ushort)((value >> 1) ^ polynomial);
                    }
                    else
                    {
                        value >>= 1;
                    }
                    temp >>= 1;
                }
                table[i] = value;
            }
        }
    }

    public class Serial
    {
        IEventAggregator _eventAggregator;
        SerialPort port;
        bool dataProcessed = false;
        byte[] dataResponse;
        byte[] buffer;
        Mutex mut = new Mutex();
        ILog logger;
        public Serial(String com, IEventAggregator ea, ILog lgger)
        {
            logger = lgger;
            _eventAggregator = ea;
            port = new SerialPort
            {
                PortName = com,
                Parity = Parity.Even,
                BaudRate = 9600,
                DataBits = 8,
                StopBits = StopBits.One,
                WriteTimeout=200,
                ReadTimeout= 200
            };

            try
            {
                port.Open();
            }
            catch (Exception e)
            {

            }

            port.RtsEnable = true;
            port.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

        }
        public void Close()
        {
            port.Close();
            port.Dispose();
            port = null;

        }

        public byte[] GetResponse()
        {
            //buffer = ;
            int counter;
            while (dataResponse==null)
            {
                buffer = dataResponse;
                Thread.Sleep(10);
            }
            buffer = dataResponse;
            mut.WaitOne();
            dataResponse = null;
            mut.ReleaseMutex();
             return buffer;
            
        }

        public void SendWithExpected(byte[] dataInput, int DelayMs)
        {
            int count = 0;
            dataProcessed = false;
            
            port.Write(dataInput, 0, dataInput.Length);



            int bytes = port.BytesToRead;
            byte[] buffer = new byte[bytes];

            Thread.Sleep(DelayMs);

            //port.Read(buffer, 0, bytes);

            //dataResponse = buffer;





            while (!dataProcessed)
            {
                Thread.Sleep(10);
                count++;
                if (count > 250)
                    dataProcessed = true;
            }

            dataProcessed = false;

        }

        public void Send(byte[] dataInput)
        {
            int count = 0;
            dataProcessed = false;
           
            port.Write(dataInput, 0, dataInput.Length);



            int bytes = port.BytesToRead;
            byte[] buffer = new byte[bytes];



            logger.Info("Sent: " +ByteArrayToString(dataInput));



            while (!dataProcessed)
            {
                Thread.Sleep(10);
                count++;
                if (count > 100)
                    dataProcessed = true;
            }

            dataProcessed = false;

        }
        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }
        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {

            SerialPort port = (SerialPort)sender;
          

            int bytes = port.BytesToRead;
            byte[] buffer = new byte[bytes];

            if (bytes > 1)
            {

                port.Read(buffer, 0, bytes);

                if (buffer.Length > 0)
                {
                    mut.WaitOne();

                    dataResponse = buffer;
                    mut.ReleaseMutex();
                    Console.WriteLine("Received: " +ByteArrayToString(dataResponse) + "\n");
                    logger.Info("Received: " +ByteArrayToString(dataResponse));
                    _eventAggregator.GetEvent<F56PortResponse>().Publish(dataResponse);
                    dataProcessed = true;

                }

            }

         
          //  port.Close();


        }
    }
}

