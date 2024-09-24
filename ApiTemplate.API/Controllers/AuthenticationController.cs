using ApiTemplate.Domain.Models.ApiModels;
using Microsoft.AspNetCore.Mvc;

namespace ApiTemplate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {


        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok();
        }
    }
}
