using Microsoft.VisualStudio.TestTools.UnitTesting;
using TCCService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCCService.Models;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;

namespace TCCService.Services.Tests
{
    [TestClass()]
    public class TCCServiceTests
    {
        TCCService _tccService = new TCCService();
        [TestMethod()]
        public void GetCostTest()
        {
            var shipping = GenerateShipping();
            var response = _tccService.GenerateGuide(shipping);
            //Assert.AreEqual(response.TotalCost, 2388);
        }

        public ShippingModel GenerateShipping()
        {
            ShippingModel shipping = new ShippingModel
            {
                code = "123456",
                content = new ShippingModel.Content()
                {
                    Description = "Bolso2",
                    Quantity = 1,
                    Value = 20000,
                    Measures = new List<ShippingModel.Measure>()
                    {
                        new ShippingModel.Measure()
                        {
                            Code = "123",
                            Height = 4,
                            Length = 4,
                            VolumetricWeight = 6,
                            Weight = 10,
                            Width = 4,
                            Units = 1,
                            Id = "123"
                        }
                    }

                },
                origin = new ShippingModel.Origin()
                {
                    Email = "test@test.com",
                    Identification = "1234",
                    IdentificationType = "CC",
                    Name = "Test",
                    Phone = "1234",
                    Location = new ShippingModel.Location()
                    {
                        Address = "calle 12",
                        City = "Medellin",
                        CityCode = "5001",
                        Country = "Colombia",
                        Department = "Antioquia"
                    }
                },
                receiver = new ShippingModel.Receiver()
                {
                    Email = "test@test.com",
                    Identification = "1234",
                    IdentificationType = "CC",
                    Name = "Test",
                    Phone = "1234",
                    Location = new ShippingModel.Location()
                    {
                        Address = "calle 12",
                        City = "Medellin",
                        CityCode = "11001",
                        Country = "Colombia",
                        Department = "Antioquia"
                    }
                },
                shippingType = new ShippingModel.ShippingType()
                {
                    KeyName = "package",
                    Name = "Paquete"
                },

                error = new Common.Error()
                {
                    HasError = false,
                    Message = ""
                },

                payment = new ShippingModel.Payment()
                {
                    Cost = new ShippingModel.Cost()
                }
            };

            return shipping;

        }

        [TestMethod()]
        public void GenerateGuideTest()
        {
            var shipping = GenerateShipping();
            var response = _tccService.GenerateGuide(shipping);

        }


        [TestMethod()]
        public void GenerateInvoice()
        {
            var shipping = GenerateShipping();

            FacturaModel fact = new FacturaModel();

            fact.NIT = "900.656.261.6";
            fact.RazonSocial = "TCC S.A.S";
            fact.Model = shipping;

            
            List<FacturaModel> ord = new List<FacturaModel>();

            ord.Add(fact);
            Factura xtrareport = new Factura();
            PdfExportOptions opts = new PdfExportOptions();

            try
            {
                xtrareport.DataSource = ord;
                string path = @"C:\Kiosko\temp\test.pdf";
                xtrareport.ExportToPdf(path);
                xtrareport.CreateDocument(false);

            }
            catch(Exception e)
            {

            }
          

         
        }
    }
}