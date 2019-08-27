using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kiosko.Library.CashPayment.F56
{
    public class Commands
    {
        #region Basic request frame
        public static readonly byte[] F56_CMD_ENQ = new byte[] { 0x10, 0x05 };
        public static readonly byte[] F56_CMD_ENQ_OK = new byte[] { 0x10, 0x06 };
        public static readonly byte[] F56_CMD_STX = new byte[] { 0x10, 0x02 };
        public static readonly byte[] F56_CMD_ETX = new byte[] { 0x10, 0x03 };
        public static readonly byte[] F56_CMD_INIT_DEVICE = new byte[] { 0x60, 0x02, 0x0D, 0x00 };
        #endregion

        #region Bill transport
        public static readonly byte[] F56_CMD_BILL_COUNT = new byte[] { 0x60, 0x03, 0x15, 0XE4 };

        public static readonly byte[] F56_CMD_BILL_COUNT_TRANSPORT = new byte[] { 0x60, 0x09, 0x15, 0XE4 };
        public static readonly byte[] F56_CMD_BILL_TRANSPORT = new byte[] { 0x60, 0x05, 0x01, 0x01, 0x1C };

        #endregion


        public static readonly byte[] F57_CMD_MECH_RESET = new byte[] { 0x60, 0x02, 0x0D, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x1C };

        public const byte SSP_CMD_RESET = 0x01;
    }
}