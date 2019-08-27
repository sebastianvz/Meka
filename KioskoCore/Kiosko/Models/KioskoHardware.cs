using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kiosko.Models
{
    public class KioskoHardware
    {
        public class DoorLocker
        {
            public string Name { get; set; }
            public string COMPort { get; set; }
            public string Id { get; set; }
        }

        public class DoorLockerList
        {
            public static string Screen = "Screen";
            public static string Billetero1 = "Billetero1";
            public static string Billetero2 = "Billetero2";
            public static string Bascula = "Bascula";
            public static string Container = "Container";
            public static string ContainerReceiver = "ContainerReceiver";
        }
    }
}