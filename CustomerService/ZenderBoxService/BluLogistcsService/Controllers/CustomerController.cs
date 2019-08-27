using ZenderBoxServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;


namespace ZenderBoxServiceControllers
{
    public class CustomerController : ApiController
    {
        Services.BluLogistcsService _service;
        Services.DaneService _daneService;

        public CustomerController()
        {
            _service = new Services.BluLogistcsService();
            _daneService = new Services.DaneService();
        }

        [HttpPost]
        [Route("api/customer/getCost/")]
        public IHttpActionResult GetCost([FromBody] ShippingModel shipping)
        {           
            try
            {
                return Ok(_service.GetCost(shipping));

            }
            catch(Exception e)
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
    }
}