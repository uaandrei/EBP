using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerService
{
    public static class Logger
    {
        public static void LogError(string message, Exception exception)
        {
            _logger.Error(message, exception);
        }

        public static void LogInfo(string message, Exception exception)
        {
            _logger.Info(message, exception);
        }

        public static void LogWarn(string message, Exception exception)
        {
            _logger.Warn(message, exception);
        }

        static Logger()
        {
            switch (ConfigurationConstants.Items.LoggerType)
            {
                case "file":
                    _logger = log4net.LogManager.GetLogger("FileAppender");
                    break;
                default:
                    throw new NotImplementedException("Unimplemented logger type: " + ConfigurationConstants.Items.LoggerType);
            }
            log4net.Config.XmlConfigurator.Configure();
        }

        private static log4net.ILog _logger;
    }
}
