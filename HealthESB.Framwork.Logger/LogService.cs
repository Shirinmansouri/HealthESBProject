using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace HealthESB.Framework.Logger
{
    public class LogService : ILogService
    {
        private ILogger _logger;
        public LogService()
        {
            ILoggerFactory loggerFactory = new LoggerFactory()
                .AddNLog();
            _logger = loggerFactory.CreateLogger<LogService>();
        }

        public void LogText(string logText)
        {
            _logger.LogInformation(
                logText);
        }
    }
}
