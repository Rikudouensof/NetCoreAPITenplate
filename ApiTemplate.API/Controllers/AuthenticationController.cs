using ApiTemplate.Application.Helpers;
using ApiTemplate.Application.Services;
using ApiTemplate.Domain.Constants;
using ApiTemplate.Domain.DTOs.ControllerDTOs;
using ApiTemplate.Domain.Models.ApiModels;
using ApiTemplate.Infrastructure.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ApiTemplate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private ILoggerHelper _logger;
        //private IEncryptionHelper _encryptionHelper;
        private IAuthenticationService _authService;
        private string className = nameof(AuthenticationController);
        public AuthenticationController(ILoggerHelper logger, IAuthenticationService authService)
        {
            _logger = logger;
           // _encryptionHelper = encryptionHelper;
            _authService = authService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel input)
        {

            var requestid = GeneralGeneratorHelpers.GetNewRequestId();
            var clientIp = GetClientIpAddress(HttpContext);
            var methodName = $"{className}/{nameof(Login)}";
            _logger.LogInformation(requestid, "new", clientIp, methodName);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                //Uncomment when you are ready to secure password in transit
                //var decyptedPassword = _encryptionHelper.DecryptPassword(input.Password);
                //input.Password = decyptedPassword;

                var serviceResponse = await _authService.LoginUser(new LoginRequestDto()
                {
                    RequestId = requestid,
                    ClientIp = clientIp,
                    Request = input

                });
                
                if (serviceResponse.ResponseCode == APIResponseConstants.sucessCode)
                {
                    _logger.LogInformation(requestid, $"User:{input.UserName} logged in successfully", clientIp, methodName);
                    return Ok(serviceResponse);
                }
                else
                {
                    _logger.LogInformation(requestid, $"User:{input.UserName} failed log in", clientIp, methodName);
                    return Unauthorized();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(requestid, $"User:{input.UserName} had an error", clientIp, methodName,ex);
                return BadRequest();
            }

          
        }

        private string GetClientIpAddress(HttpContext httpContext)
        {
            var ipAddress = httpContext.Connection.RemoteIpAddress;
            return ipAddress?.ToString();
        }
    }
}
