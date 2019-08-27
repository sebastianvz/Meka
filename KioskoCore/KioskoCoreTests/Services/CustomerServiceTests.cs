using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kiosko.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kiosko.Models;

namespace Kiosko.Services.Tests
{
    [TestClass()]
    public class CustomerServiceTests
    {
        CustomerService _service = new CustomerService("http://localhost:58948/api/customer/");

        [TestMethod()]
        public void GetDepartmentListTest()
        {
            var response = _service.GetDepartmentList();
        }

        [TestMethod()]
        public void GenerateGuideTest()
        {
            ShippingModel shipping = GenerateShipping();
            var response = _service.GenerateGuide(shipping);
        }

        [TestMethod()]
        public void GetCostTest()
        {
            ShippingModel shipping = GenerateShipping();
            var response = _service.GetCost(shipping);
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
    }
}