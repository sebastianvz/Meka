﻿using BluLogisticsService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TCCService.Models;

namespace TCCService.Controllers
{
    public class CustomerController : ApiController
    {
        BluService _service;
        BluDaneService _daneService;

        public CustomerController()
        {
            _service = new BluLogisticsService.Services.BluService();
            _daneService = new BluDaneService();
        }




        [HttpPost]
        [Route("api/customer/getCost/")]
        public IHttpActionResult GetCost([FromBody] ShippingModel shipping)
        {
            try
            {
                return Ok(_service.GetCost(shipping));

            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("api/customer/generateGuide/")]
        public IHttpActionResult GenerateGuide([FromBody] ShippingModel shipping)
        {
            try
            {
                return Ok(_service.GenerateGuide(shipping));

            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("api/customer/getCities/{departmentCode}")]
        public IHttpActionResult GetCities(int departmentCode)
        {
            try
            {
                var cityList = _daneService.GetCityList(departmentCode);
                var response = new
                {
                    cityList
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("api/customer/getDepartments/")]
        public IHttpActionResult GetDepartments()
        {
            try
            {
                var departmentList = _daneService.GetDepartmentList();
                var response = new
                {
                    departmentList
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("api/customer/generateInvoice")]
        public IHttpActionResult GenerateInvoice([FromBody] ShippingModel shipping)
        {
            try
            {
                var data =  _service.GenerateGuide(shipping);
                var response = new
                {
                    data
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("api/customer/printAllTest")]
        public IHttpActionResult PrintAllTest()
        {
            try
            {
                ShippingModel shipping = GenerateShipping();
                var data = _service.GenerateGuide(shipping);
                var response = new
                {
                    data
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                return NotFound();
            }
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
    }
}