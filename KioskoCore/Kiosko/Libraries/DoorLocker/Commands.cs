using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kiosko.Libraries.DoorLocker
{
    public class Commands
    {

        public static readonly byte CMD_OPEN = 0x71;
        public static readonly byte CMD_GET_STATUS = 0x70;
        //public static readonly byte[] CMD_GET_STATUS = new byte[] { 0xF5, 0x00, 0x70, 0x00, 0x00, 0x5F, 0xC4 };
        //public static readonly byte[] CMD_OPEN = new byte[] { 0xF5, 0x00, 0x71, 0x00, 0x01, 0x5F, 0xC6, 0x00 };
        public static readonly byte CMD_IS_OPEN = 0x00;
        public static readonly byte CMD_IS_CLOSE = 0x01;



        //public static readonly byte[] CMD_IS_OPEN = new byte[] { 0xF5,0x0,0x70,0x10,0x1,0x5F,0xD5,0x0 };
        //public static readonly byte[] CMD_IS_CLOSE = new byte[] { 0xF5, 0x00, 0x70, 0x10, 0x01, 0x5F, 0xD6, 0x01 };
    }
}