using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kiosko.Library.CashPayment.JCM
{
    public class JCMModel
    {
        public string Port { get; set; }
    }

    public enum Status
    {
        PowerUP = 0x40,
        Idling = 0x11,
        Inhibit = 0x1A,
        BillinScrow = 0x13,
        Rejected = 0x17,
        StarckerFull = 0x43,
        StackerOpen = 0x44,
        JamInAcceptor = 0x45,
        JamInStacker = 0x46,
        Paused = 0x47,
        Cheated = 0x48,
        MajorFailure = 0x49,
        ComError = 0x4A

    }
}