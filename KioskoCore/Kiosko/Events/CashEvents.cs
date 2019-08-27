using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static Kiosko.Models.InventarioCash;

namespace Kiosko.Events
{
    public class F56PortResponse : PubSubEvent<byte[]>
    {
    }
    
    public class Enable_Cash: PubSubEvent<bool>
    {
    }

    public class Disable_Cash : PubSubEvent<bool>
    {
    }


    public class Cash_credited : PubSubEvent<int>
    {
    }

    public class Devol_Cash : PubSubEvent<List<Efectivo>>
    {
    }

   

    public class Cash_Alert: PubSubEvent<string> { }
}