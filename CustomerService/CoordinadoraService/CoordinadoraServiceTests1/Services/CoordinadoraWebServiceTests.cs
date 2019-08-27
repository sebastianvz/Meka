using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoordinadoraService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoordinadoraService.Services.Tests
{
    [TestClass()]
    public class CoordinadoraWebServiceTests
    {
        [TestMethod()]
        public void GetDepartmentListTest()
        {

            CoordinadoraWebService _service = new CoordinadoraWebService();

            _service.GetDepartmentList();
        }
    }
}