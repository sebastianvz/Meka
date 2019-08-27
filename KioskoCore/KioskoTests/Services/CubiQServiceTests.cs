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
    public class CubiQServiceTests
    {
        CubiQService CubiQService = new CubiQService("localhost:82/CubiqEmb_php");
        [TestMethod()]
        public void GetMeasureTest()
        {
            var measures = CubiQService.GetMeasure();

        }
    }
}