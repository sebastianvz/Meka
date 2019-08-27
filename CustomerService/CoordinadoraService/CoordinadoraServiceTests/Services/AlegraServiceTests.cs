using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoordinadoraService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoordinadoraService.Models;
using Kiosko.Models;

namespace CoordinadoraService.Services.Tests
{
    [TestClass()]
    public class AlegraServiceTests
    {

        public ShippingModel GenerateShipping()
        {
            ShippingModel shipping = new ShippingModel
            {
                code = "123456",
                content = new ShippingModel.Content()
                {
                    Description = "Bolso2",
                    Quantity = 1,
                    Value = 12000,
                    Measures = new List<ShippingModel.Measure>()
                    {
                        new ShippingModel.Measure()
                        {
                            Code = "123",
                            Height = 4,
                            Length = 4,
                            VolumetricWeight = 4,
                            Weight = 4,
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
                        CityCode = "05001",
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
                        CityCode = "05001",
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
                    {
                        TotalCost = 22000
                    }
                }
            };

            return shipping;

        }

        [TestMethod()]
        public void CreateInvoiceTest()
        {
            AlegraService serv = new AlegraService();
            var shipping = GenerateShipping();
            AlegraResult test= serv.CreateInvoice(shipping);


            
        }
    }
}