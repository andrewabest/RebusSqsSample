using ConfigInjector;

namespace Sample.Service.ConfigurationSettings
{
    public class MinimumLogLevelSetting : ConfigurationSetting<Serilog.Events.LogEventLevel>
    {
    }
}