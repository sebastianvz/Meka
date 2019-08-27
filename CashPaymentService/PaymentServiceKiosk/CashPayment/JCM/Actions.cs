using ID003ProtocolManager;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Web;

namespace Kiosko.Library.CashPayment.JCM
{
    class Actions
    {

        public static void init(byte[] buffer, int length, ID003CommandCreater ComDll, SerialPort Port)
        {
            //This method does the BV setup in order to accept bills 
            byte enable1 = 0;
            byte enable2 = 0;



            //Sending the reset command
            ComDll.Reset(buffer);
            length = (int)buffer[1];
            Port.Write(buffer, 0, length);
            System.Threading.Thread.Sleep(100); //wait for 100ms (poll rate should be between 100ms and 200ms)

            //Enabling denominations ($1, $5, $10, $20, $50, $100)
            ComDll.SetEnableDeno(buffer, enable1, enable2);
            length = (int)buffer[1];
            Port.Write(buffer, 0, length);
            System.Threading.Thread.Sleep(100);

            //Setting standard security enable1= 0x00, enable2= 0x00
            ComDll.SetSecurity(buffer, enable1, enable2);
            length = (int)buffer[1];
            buffer[5] = 0x01;
            System.Threading.Thread.Sleep(10);
            Port.Write(buffer, 0, length);
            System.Threading.Thread.Sleep(100);


            //No optional functions enable1= 0x00, enable2= 0x00
            ComDll.SetOpFunction(buffer, enable1, enable2);
            length = (int)buffer[1];
            Port.Write(buffer, 0, length);
            System.Threading.Thread.Sleep(100);

            //Enabling bill acceptor enable=0x00 (0x01 to disable)
            ComDll.SetInhibit(buffer, enable1);
            length = (int)buffer[1];
            Port.Write(buffer, 0, length);
            System.Threading.Thread.Sleep(100);

        }

        public static void Accept(byte[] buffer, int length, ID003CommandCreater ComDll, SerialPort Port)
        {
            //This method will stack the note once the vend valid message is sent by the unit

            bool vend = false; //value to check if vend valid has been sent
            byte[] status = new byte[255];  //capturing the status message from the serial port

            ComDll.Stack1(buffer); //we have detected escrow and now sending the stack command.
            length = (int)buffer[1];
            Port.Write(buffer, 0, length); //writing buffer to com port
            System.Threading.Thread.Sleep(100);

            while (!vend) //if no vend valid, keep checking status
            {
                ComDll.StatusRequest(buffer); //checking status
                length = (int)buffer[1];
                Port.Write(buffer, 0, length);
                Port.Read(status, 0, 255); //capturing status from the com port
                System.Threading.Thread.Sleep(100);

                if (status[2] == 0x15) //we have received vend valid response
                {
                    ComDll.Ack(buffer); //Sending an ACK
                    length = (int)buffer[1];
                    Port.Write(buffer, 0, length);
                    vend = true;
                }
            }

        }

        public static uint Calc_CRC_main(uint crc, uint ch)
        {

            uint quo;

            quo = (crc ^ ch) & 15;
            crc = (crc >> 4) ^ (quo * 4225);
            quo = (uint)((crc ^ (ch >> 4)) & 15);
            crc = (crc >> 4) ^ (quo * 4225);
            return (crc);

        }

        public static uint Calc_CRC(byte[] msg, int len)
        {
            //char output[128] = {0};
            uint crc;
            uint ch;
            crc = 0;
            int i = 0;
            while (len-- > 0)
            {
                ch = (uint)msg[i];
                crc = Calc_CRC_main(crc, ch);
                i++;
            }

            return (crc);
        }


        public static void messageFinder(byte[] memory, ref int host, ref int slave)
        {
            int temp = 0;
            bool found = false;
            int origin;

            if (memory[temp] == 0x00)
                temp++;


            if ((memory[temp] == 0x11) && (memory[temp + 2] == 0x11))
            {
                host = temp;
                slave = temp + 2;
                found = true;
            }

            while (!found)
            {
                if ((memory[temp] >= 0x12) && (memory[temp] <= 0x1B))
                {
                    slave = temp;
                    found = true;
                }
                temp++;

            }




        }

        public static void Transfer(byte[] memory, byte[] log, ref int marker)
        {
            int temp;
            for (temp = 7; temp < 253; temp++)
            {
                log[marker] = memory[temp];
                marker++;
            }

        }




    }
}