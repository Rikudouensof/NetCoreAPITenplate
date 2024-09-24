using ApiTemplate.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace ApiTemplate.Infrastructure.Helpers
{
    internal class LoggerHelper : ILoggerHelper
    {

        private readonly ILogger<LoggerHelper> _logger;
        public LoggerHelper(ILogger<LoggerHelper> logger)
        {
            _logger = logger;
        }
        public void LogError(string requestId, string message, string ip, string methodName, Exception ex)
        {
            _logger.LogError(ex, $"requestId:{requestId}, Method Name:{methodName}, IP:{ip}, Message:{message}, Error:{ex}", "Error");
        }

        public void LogInformation(string requestId, string message, string ip, string methodName)
        {
            _logger.LogInformation($"requestId:{requestId}, Method Name:{methodName}, IP:{ip}, Message:{message}");
        }

        public void logInformationWithArguement(string requestId, string message, string ip, string methodName, object arguemnet)
        {
            _logger.LogInformation($"requestId:{requestId}, Method Name:{methodName}, IP:{ip}, Message:{message}", arguemnet);
        }

        public void LogTrace(string requestId, string message, string ip, string methodName, Exception ex)
        {
            _logger.LogError(ex, $"requestId:{requestId}, Method Name:{methodName}, IP:{ip}, Message:{message}, Error:{ex}", "LogTrace");
        }

        public void logWarning(string requestId, string message, string ip, string methodName)
        {
            _logger.LogWarning($"requestId:{requestId}, Method Name:{methodName}, IP:{ip}, Message:{message}", "logWarning");
        }
    }
}
