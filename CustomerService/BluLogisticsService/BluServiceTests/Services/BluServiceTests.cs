using Microsoft.VisualStudio.TestTools.UnitTesting;
using BluLogisticsService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCCService.Models;

namespace BluLogisticsService.Services.Tests
{
    [TestClass()]
    public class BluServiceTests
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
        public void BluServiceTest()
        {


        }

        [TestMethod()]
        public void GetCostTest()
        {
            BluService serv = new BluService();
            serv.GetCost(GenerateShipping());

        }

        [TestMethod()]
        public void GenerateGuideTest()
        {
            BluService serv = new BluService();
            serv.GenerateGuide(GenerateShipping());

        }

        [TestMethod()]
        public void GetCitiesTest()
        {
            BluService serv = new BluService();
            serv.GetCities();
        }

        [TestMethod()]
        public void GetCitiesTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetDepartmentListTest()
        {
            BluService serv = new BluService();
            List<object> departamentos = serv.GetDepartmentList();

        }

        [TestMethod()]
        public void GetCityListTest()
        {
            BluService serv = new BluService();
            List<object> ciudades = serv.GetCityList("05");
        }
    }
}