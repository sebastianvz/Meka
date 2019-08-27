using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kiosko.Models
{
    public class Common
    {
        public class Response
        {
            public Response()
            {
                Success = true;
                Message = "";
            }
            public bool Success { get; set; }
            public string Message { get; set; }
        }
        public class Error
        {
            public Error()
            {
                this.HasError = false;
            }
            public bool HasError { get; set; }
            public string Message { get; set; }

        }
        public class ServiceStatus
        {
            public ServiceStatus()
            {
                IsDone = false;
            }
            public string DeviceName { get; set; }

            public Error error { get; set; } = new Error();
            public bool IsDone { get; set; }

        }

    }
}