using KioskoAdmin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;

using System.Web.Script.Serialization;
using static Kiosko.Models.InventarioCash;

namespace Kiosko.Models
{
    public class InventarioCash
    {

        public enum Type
        {
            Coin,
            Bill

        };
        public static class Location
        {
            public const string F56 = "F56";
            public const string JCM = "JCM";
            public const string SMARTHOPPER = "SMARTHOPPER";

        }
        public enum TipoOperacion
        {
            sumar,
            restar,
            reemplazar
        }
        public class Efectivo
        {
            public int Value { get; set; }
            public int Inventory { get; set; }
            public Type Type { get; set; }
            public string Location { get; set; }

        }

        public class EfectivoViewModel
        {
            public User user { get; set; }
            public List<Efectivo> efectivo { get; set; }
            public string operacion { get; set; }
        }



    }
  
}