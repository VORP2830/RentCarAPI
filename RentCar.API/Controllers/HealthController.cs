using Microsoft.AspNetCore.Mvc;

namespace RentCar.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    [HttpGet]
        public ActionResult Get()
        {
            try
            {
                var response = new
                {
                    DataAcesso = DateTime.Now.ToLongDateString()
                };
                return Ok(response);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }
}
