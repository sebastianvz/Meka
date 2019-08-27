using Microsoft.VisualStudio.TestTools.UnitTesting;
using TCCService.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCCService.Models;

namespace TCCService.Controllers.Tests
{
    [TestClass()]
    public class CustomerControllerTests
    {
        CustomerController _service = new CustomerController();
       
        [TestMethod()]
        public void GetCostTest()
        {
            ShippingModel shipping = GenerateShipping();

            var costResponse = _service.GetCost(shipping);

        }

        [TestMethod()]
        public void GenerateGuideTest()
        {
            ShippingModel shipping = GenerateShipping();

            var response = _service.GenerateGuide(shipping);

        }



        public ShippingModel GenerateShipping()
        {
            ShippingModel shipping = new ShippingModel
            {
                code = "12345",
                content = new ShippingModel.Content()
                {
                    Description = "Bolso",
                    Quantity = 1,
                    Value = 12000,
                    Measures = new List<ShippingModel.Measure>()
                {
                    new ShippingModel.Measure()
                    {
                        Code = "123",
                        Height = 10,
                        Length = 10,
                        VolumetricWeight = 10,
                        Weight = 10,
                        Width = 10,
                        Units = 1,
                        Id = "123"
                    }
                }

                },
                origin = new ShippingModel.Origin()
                {
                    Email = "test@test.com",
                    Identification = "1234",
                    IdentificationType = "NIT",
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
                    IdentificationType = "CC",
                    Identification = "1234",
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
                shippingType = new ShippingModel.ShippingType()
                {
                    KeyName = "package",
                    Name = "Paquete"
                },

                error = new ShippingModel.Error()
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
    }
}