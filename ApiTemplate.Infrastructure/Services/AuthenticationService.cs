using ApiTemplate.Application.Helpers;
using ApiTemplate.Application.Services;
using ApiTemplate.Domain.Constants;
using ApiTemplate.Domain.DTOs.ControllerDTOs;
using Newtonsoft.Json;

namespace ApiTemplate.Infrastructure.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IGenerateJwtTokenHelper _generateJwtTokenHelper;
        private readonly ILoggerHelper _logger;
        private string className = nameof(AuthenticationService);
        public AuthenticationService(IGenerateJwtTokenHelper generateJwtTokenHelper, ILoggerHelper logger)
        {
            _generateJwtTokenHelper = generateJwtTokenHelper;
            _logger = logger;
        }
        public async Task<LoginResponseDto> LoginUser(LoginRequestDto input)
        {
            var methodName = $"{className}/{nameof(LoginUser)}";
            _logger.LogInformation(input.RequestId,"new",input.ClientIp,methodName);
            var output = new LoginResponseDto()
            {
                ResponseDescription = APIResponseConstants.failureMessage,
                ResponseCode = APIResponseConstants.failureCode,
                UserName = input.Request.UserName
            };
            try
            {
                var request = input.Request;
                //Please update this. Typically a login system is here 
                if (request.UserName.Equals("vbnoeirwfg4f") && request.Password.Equals("A1b2c3d4#E"))
                {
                    var token = _generateJwtTokenHelper.GenerateJwtToken(request.UserName);
                    output.Token = token;
                    output.ResponseCode = APIResponseConstants.sucessCode;
                    output.ResponseDescription = APIResponseConstants.sucessMessage;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(input.RequestId, "Log Error", input.ClientIp, methodName,ex);
            }
            _logger.LogInformation(input.RequestId, $"Final Response = {JsonConvert.SerializeObject(output)}", input.ClientIp, methodName);
            return output;
        }
    }
}
