using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTemplate.Domain.Models.CoreModels
{
    public class JWTSettings
    {
        public string? JWTIssuer { get; set; } = string.Empty;
        public string? JWTAudience { get; set; } = string.Empty;
    }
}
