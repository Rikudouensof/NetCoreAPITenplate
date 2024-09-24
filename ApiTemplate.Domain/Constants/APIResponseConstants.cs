using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTemplate.Domain.Constants
{
    public static class APIResponseConstants
    {
        public const string sucessCode = "00";
        public const string sucessMessage = "Sucessfull";
        public const string failureCode = "01";
        public const string failureMessage = "Failed";
        public const string timeoutCode = "02";
        public const string timeoutMessage = "Service Timeout";
    }
}
