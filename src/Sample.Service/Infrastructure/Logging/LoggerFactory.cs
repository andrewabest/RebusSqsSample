using System;
using Sample.Service.ConfigurationSettings;
using Serilog;

namespace Sample.Service.Infrastructure.Logging
{
    public class LoggerFactory : ILoggerFactory
    {
        private readonly EnvironmentNameSetting _environmentName;
        private readonly EnvironmentTypeSetting _environmentType;
        private readonly MinimumLogLevelSetting _minimumLogLevel;
        private readonly SeqServerUriSetting _seqServerUri;

        public LoggerFactory(EnvironmentNameSetting environmentName, EnvironmentTypeSetting environmentType, MinimumLogLevelSetting minimumLogLevel, SeqServerUriSetting seqServerUri)
        {
            _environmentName = environmentName;
            _environmentType = environmentType;
            _minimumLogLevel = minimumLogLevel;
            _seqServerUri = seqServerUri;
        }

        public ILogger CreateLogger()
        {
            var assemblyName = GetType().Assembly.GetName().Name;
            var assemblyVersion = GetType().Assembly.GetName().Version;

            var loggerConfig = new LoggerConfiguration()
                .MinimumLevel.Is(_minimumLogLevel)
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithThreadId()
                .Enrich.WithProcessId()
                .Enrich.WithProperty("ApplicationName", assemblyName)
                .Enrich.WithProperty("ApplicationVersion", assemblyVersion)
                .Enrich.WithProperty("EnvironmentName", _environmentName)
                .Enrich.WithProperty("EnvironmentType", _environmentType)
                .Enrich.WithProperty("OSVersion", Environment.OSVersion)
                .WriteTo.LiterateConsole()
                .WriteTo.Seq(_seqServerUri.ToString());

            return loggerConfig.CreateLogger();
        }
    }
}