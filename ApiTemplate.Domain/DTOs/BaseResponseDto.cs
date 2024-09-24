using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTemplate.Domain.DTOs
{
    public class BaseResponseDto
    {

        public string ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
    }
}
