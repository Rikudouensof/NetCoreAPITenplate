using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTemplate.Infrastructure.Helpers
{
    public static class GeneralGeneratorHelpers
    {
        public static string GetNewRequestId()
        {
            Random rnd = new Random();
            int myRandomNo = rnd.Next(1000000, 9999999);
            var requestId = $"Track:{myRandomNo}";
            return requestId;
        }
    }
}
