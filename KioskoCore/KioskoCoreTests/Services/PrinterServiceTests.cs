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
        PrinterService _printerService = new PrinterService();
        [TestMethod()]
        public void CheckPrinterPaperLevelTest()
        {
            //_printerService.CheckPrinterPaperLevel("KioskoPrinterBill");
        }
    }
}