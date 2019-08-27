﻿using Kiosko.Library.CashPayment.SmartHopper.SSP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kiosko.Library.CashPayment.SmartHopper
{

    public class PollResponse
    {

        public Commandos Comand;
        public bool Success;


        public enum Commandos
        {
            SSP_POLL_SLAVE_RESET,
            SSP_POLL_DISABLED,
            SSP_POLL_DISPENSING,
            SSP_POLL_DISPENSED,
            SSP_POLL_JAMMED,
            SSP_POLL_HALTED,
            SSP_POLL_FLOATING,
            SSP_POLL_FLOATED,
            SSP_POLL_TIME_OUT,
            SSP_POLL_INCOMPLETE_PAYOUT,
            SSP_POLL_INCOMPLETE_FLOAT,
            SSP_POLL_CASHBOX_PAID,
            SSP_POLL_COIN_CREDIT,
            SSP_POLL_COIN_MECH_JAMMED,
            SSP_POLL_COIN_MECH_RETURN_PRESSED,
            SSP_POLL_EMPTYING,
            SSP_POLL_EMPTIED,
            SSP_POLL_FRAUD_ATTEMPT,
            SSP_POLL_SMART_EMPTYING,
            SSP_POLL_SMART_EMPTIED,
            UNSUPPORTED_POLL
        }


    }



    public class ChannelData
    {
        public int Value;
        public byte Channel;
        public char[] Currency;
        public int Level;
        public bool Recycling;
        public ChannelData()
        {
            Value = 0;
            Channel = 0;
            Currency = new char[3];
            Level = 0;
            Recycling = false;
        }
    };

    public class CHelpers
    {
        // Helper function to convert 4 bytes to an int from a specified array and index.
        static public int ConvertBytesToInt32(byte[] b, int index)
        {
            return BitConverter.ToInt32(b, index);
        }

        // Same for 2 bytes to int
        static public short ConvertBytesToInt16(byte[] b, int index)
        {
            return BitConverter.ToInt16(b, index);
        }

        // Convert bytes to int
        static public byte[] ConvertInt8ToBytes(char i)
        {
            return BitConverter.GetBytes(i);
        }
        static public byte[] ConvertInt16ToBytes(short i)
        {
            return BitConverter.GetBytes(i);
        }

        static public byte[] ConvertInt32ToBytes(int i)
        {
            return BitConverter.GetBytes(i);
        }

        // Uses value of channel and g

        static public string FormatToCurrency(int unformattedNumber)
        {
            float f = unformattedNumber * 0.01f;
            return f.ToString("0.00");
        }






        // This helper takes a byte and returns the command/response name as a string.
        static public string ConvertByteToName(byte b)
        {
            switch (b)
            {
                case 0x01:
                    return "RESET COMMAND";
                case 0x11:
                    return "SYNC COMMAND";
                case 0x4A:
                    return "SET GENERATOR COMMAND";
                case 0x4B:
                    return "SET MODULUS COMMAND";
                case 0x4C:
                    return "KEY EXCHANGE COMMAND";
                case 0x2:
                    return "SET INHIBITS COMMAND";
                case 0xA:
                    return "ENABLE COMMAND";
                case 0x09:
                    return "DISABLE COMMAND";
                case 0x7:
                    return "POLL COMMAND";
                case 0x05:
                    return "SETUP REQUEST COMMAND";
                case 0x03:
                    return "DISPLAY ON COMMAND";
                case 0x04:
                    return "DISPLAY OFF COMMAND";
                case 0x5C:
                    return "ENABLE PAYOUT COMMAND";
                case 0x5B:
                    return "DISABLE PAYOUT COMMAND";
                case 0x3B:
                    return "SET ROUTING COMMAND";
                case 0x45:
                    return "SET VALUE REPORTING TYPE COMMAND";
                case 0X42:
                    return "PAYOUT LAST NOTE COMMAND";
                case 0x3F:
                    return "EMPTY COMMAND";
                case 0x41:
                    return "GET NOTE POSITIONS COMMAND";
                case 0x43:
                    return "STACK LAST NOTE COMMAND";
                case 0xF1:
                    return "RESET RESPONSE";
                case 0xEF:
                    return "NOTE READ RESPONSE";
                case 0xEE:
                    return "CREDIT RESPONSE";
                case 0xED:
                    return "REJECTING RESPONSE";
                case 0xEC:
                    return "REJECTED RESPONSE";
                case 0xCC:
                    return "STACKING RESPONSE";
                case 0xEB:
                    return "STACKED RESPONSE";
                case 0xEA:
                    return "SAFE JAM RESPONSE";
                case 0xE9:
                    return "UNSAFE JAM RESPONSE";
                case 0xE8:
                    return "DISABLED RESPONSE";
                case 0xE6:
                    return "FRAUD ATTEMPT RESPONSE";
                case 0xE7:
                    return "STACKER FULL RESPONSE";
                case 0xE1:
                    return "NOTE CLEARED FROM FRONT RESPONSE";
                case 0xE2:
                    return "NOTE CLEARED TO CASHBOX RESPONSE";
                case 0xE3:
                    return "CASHBOX REMOVED RESPONSE";
                case 0xE4:
                    return "CASHBOX REPLACED RESPONSE";
                case 0xDB:
                    return "NOTE STORED RESPONSE";
                case 0xDA:
                    return "NOTE DISPENSING RESPONSE";
                case 0xD2:
                    return "NOTE DISPENSED RESPONSE";
                case 0xC9:
                    return "NOTE TRANSFERRED TO STACKER RESPONSE";
                case 0xF0:
                    return "OK RESPONSE";
                case 0xF2:
                    return "UNKNOWN RESPONSE";
                case 0xF3:
                    return "WRONG PARAMS RESPONSE";
                case 0xF4:
                    return "PARAM OUT OF RANGE RESPONSE";
                case 0xF5:
                    return "CANNOT PROCESS RESPONSE";
                case 0xF6:
                    return "SOFTWARE ERROR RESPONSE";
                case 0xF8:
                    return "FAIL RESPONSE";
                case 0xFA:
                    return "KEY NOT SET RESPONSE";
                default:
                    return "Byte command name unsupported";
            }
        }
    }
}