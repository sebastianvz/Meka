using Kiosko.Models;
using KioskoAdmin.Models;
using KioskoAdmin.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;


namespace KioskoAdmin.Controllers
{
    public class ApiController : System.Web.Http.ApiController
    {
        KioskoCoreService _kioskoCoreService = new KioskoCoreService();
        public ApiController()
        {

        }

        [Route("api/removeboxes")]
        [HttpPost]
        public IHttpActionResult removeBoxes([FromBody]User user)
        {
          

            return Ok();
        }

        [Route("api/openDoor/{door}")]
        [HttpPost]
        public IHttpActionResult openDoor([FromBody]User user,[FromUri] string Door)
        {
            try
            {
                var response = new Common.Response();

                if (!Helpers.Utilities.ValidateApiSession(user))
                {
                    response.Success = false;
                    response.Message = "Not authorized";
                    return Unauthorized();
                }

                if (Door == "CashContainer")
                    _kioskoCoreService.OpenBilleteros();
                else
                    _kioskoCoreService.OpenDoor(Door);

                return Ok(response);
                
            }
            catch(Exception e)
            {
                return NotFound();
            }
            
        }

    }
}