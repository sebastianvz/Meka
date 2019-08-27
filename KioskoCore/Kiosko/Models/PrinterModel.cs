using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kiosko.Models
{
    public class PrintModel
    {
    }

    public class PrintModelResponse
    {
        public string GuideCode { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }

    }




    public class GuiaOutModel
    {
        public bool Success { get; set; }
        public int RemisionId { get; set; }
        public string RemisionCode { get; set; }
        public string PdfGuide { get; set; }
        public string Url { get; set; }
        public string Message { get; set; }
    }
}