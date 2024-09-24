using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTemplate.Application.Helpers
{
    public interface ILoggerHelper
    {

        public void LogInformation(string requestId, string message, string ip, string methodName);

        public void LogError(string requestId, string message, string ip, string methodName, Exception ex);


        public void LogTrace(string requestId, string message, string ip, string methodName, Exception ex);

        public void logWarning(string requestId, string message, string ip, string methodName);

        public void logInformationWithArguement(string requestId, string message, string ip, string methodName, Object arguemnet);
    }
}
