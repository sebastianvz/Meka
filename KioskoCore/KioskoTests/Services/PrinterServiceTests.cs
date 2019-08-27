using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kiosko.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiosko.Services.Tests
{
    [TestClass()]
    public class PrinterServiceTests
    {
        [TestMethod()]
        public void PrinterExistsTest()
        {
            PrinterService serv = new PrinterService();

            serv.SetFileName("C:\\Users\\MEKADEV03\\Downloads\\IMPRESORAS\\IMPRESORAS\\GUIA-TCC.pdf");
            serv.SetPrinterName("Zebra KR203");
            serv.Print(150);
           //serv.Test();



        }
    }
}