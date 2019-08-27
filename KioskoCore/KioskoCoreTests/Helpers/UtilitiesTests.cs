using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kiosko.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiosko.Helpers.Tests
{
    [TestClass()]
    public class UtilitiesTests
    {
        
        [TestMethod()]
        public void UrlPdfToBase64Test()
        {
            string url = "https://testsomos.tcc.com.co:8484/Informesdsp.aspx?opc=1&tipo=REMESAS&modelos=1,1,210-&naturaleza=J&tipoIdentificacion=NIT&ids=15248577-TEMPORAL";
            var urlToBase64 = Helpers.Utilities.UrlPdfToBase64("");
            Assert.AreEqual(urlToBase64 , "");
        }
    }
}