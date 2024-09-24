using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTemplate.Domain.DTOs
{
    public class BaseRequestDto
    {
        public string RequestId { get; set; } = string.Empty;

        public string ClientIp { get; set; } = string.Empty;


    }
}
