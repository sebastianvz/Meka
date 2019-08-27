using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TCCService.Models
{
    public class FacturaModel
    {

        public ShippingModel Model { get; set; }
        public string RazonSocial { get; set; }
        public string NIT { get; set; }
        public string Direccion { get; set; }

        public string FactNumber { get; set; }
        public ResolucionDian Res { get; set; }

        public string Fecha { get; set; }

        public string FormaDePago { get; set; }

        public string Remesa { get; set; }

        public int ValorFactura { get; set; }

        public int Weight { get; set; }

        public int VolWeight { get; set; }
    }

    public class ResolucionDian
    {
        public long Resolucion { get; set; }
        public DateTime Fecha { get; set; }
        public int InicioNumeracion { get; set; }
        public int FinNumeracion { get; set; }
    }

}