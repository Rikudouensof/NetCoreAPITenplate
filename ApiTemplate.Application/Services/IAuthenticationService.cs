using ApiTemplate.Domain.DTOs.ControllerDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTemplate.Application.Services
{
    public interface IAuthenticationService
    {


        Task<LoginResponseDto> LoginUser(LoginRequestDto input);
    }
}
