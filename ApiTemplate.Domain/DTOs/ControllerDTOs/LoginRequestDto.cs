using ApiTemplate.Domain.Models.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTemplate.Domain.DTOs.ControllerDTOs
{
    public class LoginRequestDto : BaseRequestDto
    {
        public LoginModel  Request { get; set; }
    }
}
