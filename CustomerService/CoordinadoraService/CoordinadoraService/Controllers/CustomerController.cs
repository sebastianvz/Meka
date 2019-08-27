using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using CoordinadoraService.Models;
using Kiosko.Models;

namespace CoordinadoraService.Controllers
{
    public class CustomerController : ApiController
    {
        Services.CoordinadoraWebService _service;
     //   Services.DaneService _daneService;

        public CustomerController()
        {
            _service = new Services.CoordinadoraWebService();
            //_daneService = new Services.DaneService();
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

        [Route("api/customer/test/{a}")]
        public IHttpActionResult test(int a)
        {
            try
            {
                var response = new
                {
                    data = a
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }


        [HttpPost]
        [Route("api/customer/printInvoice/")]
        public IHttpActionResult printInvoice([FromBody] ShippingModel shipping)
        {
            try
            {
                var shipp = _service.PrintInvoice(shipping);
                //var response = new
                //{
                //    shipp
                //};
                return Ok(shipp);
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }


            

        [HttpGet]
        [Route("api/customer/getCities/{departmentCode}")]
        public IHttpActionResult GetCities(string departmentCode)
        {
            try
            {
                var cityList = _service.GetCityList(departmentCode);
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
                var departmentList = _service.GetDepartmentList();
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
    }
}
