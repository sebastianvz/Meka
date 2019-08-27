using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kiosko.Models
{
    public class CubiQModel:Common
    {
        
        public class Resource
        {
            public static string MEASUREMENTS = "getMeasurements";
            public static string GENERALINFORMATION = "getGeneralInformation";
            public static string IMAGE = "getImage/color";
        }

        public class Measure
        {
            public double Width { get; set; }
            public double Length { get; set; }
            public double Height { get; set; }
            public double Weight { get; set; }
            public string Status { get; set; }
            public double VolumetricWeight { get; set; }
            public string ImageBase64 { get; set; }
            public bool Success { get; set; }
            public Error Error = new Error();
        }

        public class Image
        {
            public string Success { get; set; }
            public string Image64 { get; set; }
        }

        public class GeneralInformation
        {
            public bool IsCubiQRunning { get; set; }
            public bool IsCameraConnected { get; set; }
            public bool IsCameraCalibrated { get; set; }
            public bool IsWeightReading { get; set; }
            public bool IsBarcodeHandler { get; set; }
            public bool ActiveLicense { get; set; }
            public string CameraSerial { get; set; }
            public string LicenseExpirationDate { get; set; }
            public string MachinePID { get; set; }
            public string StatusMessage { get; set; }
            public bool Success { get; set; }
            public Error Error = new Error();
        }
    }
}