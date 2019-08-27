using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Kiosko.Models.Common;

namespace KioskoTests.Libraries.CashPayment
{
    public class Common
    {
        public class ServiceStatus
        {
            public Error error = new Error();
            public bool IsDone { get; set; }

        }
    }
}
