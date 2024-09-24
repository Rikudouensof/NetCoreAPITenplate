using ApiTemplate.Application.Helpers;
using ApiTemplate.Domain.Models.ApiModels;
using Microsoft.AspNetCore.Mvc;

namespace ApiTemplate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private ILoggerHelper _logger;
        private IEncryptionHelper _encryptionHelper;
        public AuthenticationController(ILoggerHelper logger, IEncryptionHelper encryptionHelper)
        {
            _logger = logger;
            _encryptionHelper = encryptionHelper;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                //Uncomment when you are ready to secure password in transit
                //var decyptedPassword = _encryptionHelper.DecryptPassword(input.Password);
                //input.Password = decyptedPassword;

            }
            catch (Exception ex)
            {

                throw;
            }

            return Ok();
        }
    }
}
