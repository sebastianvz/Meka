using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketTest
{
    public  class ComClass
    {

        public function funciones { get; set; }
        public int Value { get; set; }
        public status_cash result { get; set; }


        public enum function
        {
            cash_handling,
            operation_status

        }

        public enum status_cash
        {
            ok,
            time_out,
            operation_error,
            fail

        }
    }
}
