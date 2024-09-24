using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTemplate.Domain.DTOs.ControllerDTOs
{
    public class LoginResponseDto : BaseResponseDto
    {
        public string UserName { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty ;
    }
}
