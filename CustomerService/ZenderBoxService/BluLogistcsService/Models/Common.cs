using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZenderBoxServiceModels
{
    public class Common
    {
        public class Error
        {
            public bool HasError { get; set; }
            public string Message { get; set; }
        }
    }
}