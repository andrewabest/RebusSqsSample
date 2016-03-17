using System;
using ConfigInjector.QuickAndDirty;
using Sample.Service.ConfigurationSettings;
using Sample.Service.Infrastructure.ConfigInjector;
using Sample.Service.Infrastructure.Logging;
using Serilog;

namespace Sample.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DefaultSettingsReader.SetStrategy(new CustomStaticReaderStrategy(Constants.ServiceAssembly));

            var environmentName = DefaultSettingsReader.Get<EnvironmentNameSetting>();
            var environmentType = DefaultSettingsReader.Get<EnvironmentTypeSetting>();
            var minimumLogLevel = DefaultSettingsReader.Get<MinimumLogLevelSetting>();
            var seqServerUri = DefaultSettingsReader.Get<SeqServerUriSetting>();

            Log.Logger = new LoggerFactory(environmentName, environmentType, minimumLogLevel, seqServerUri)
                .CreateLogger();

           
            var service = new SampleService().Start();

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();

            service.Stop();
        }
    }
}
