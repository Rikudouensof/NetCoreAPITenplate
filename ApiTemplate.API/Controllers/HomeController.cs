using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiTemplate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HomeController : ControllerBase
    {


        [HttpGet("ConfirmLogin")]
        public async Task<IActionResult> IsLoggedIn()
        {
            return Ok();
        }
    }
}
