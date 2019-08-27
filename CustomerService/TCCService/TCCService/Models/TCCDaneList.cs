using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TCCService.Models
{
    public class TCCDaneList
    {

        public string CODIGO_AS400 { get; set; }
        public string DANE { get; set; }
        public string NOMBRE_POBLACION { get; set; }
        public string DEPARTAMENTO { get; set; }

        public string REEXPEDIDA { get; set; }

        public string ESTADO { get; set; }
    }
}