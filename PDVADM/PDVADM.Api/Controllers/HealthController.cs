using Microsoft.AspNetCore.Mvc;

namespace PDVADM.Api.Controllers;

    [ApiController]
    [Route("api/health")]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { 
                status = "PDVADM API online!",
                timestamp = DateTime.UtcNow.ToString("dd/MM/yyyy HH:mm:ss")
            });
        }
    }
