using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ConfigInjector;
using ConfigInjector.Configuration;
using ConfigInjector.Infrastructure.ValueParsers;
using ConfigInjector.QuickAndDirty;
using Sample.Service.Infrastructure.Extensions;

namespace Sample.Service.Infrastructure.ConfigInjector
{
    public class CustomStaticReaderStrategy : IStaticSettingReaderStrategy
    {
        private readonly Assembly[] _assemblies;
        private readonly Lazy<IList<IConfigurationSetting>> _settings;

        public CustomStaticReaderStrategy(params Assembly[] assemblies)
        {
            _assemblies = assemblies;
            _settings = new Lazy<IList<IConfigurationSetting>>(ReadSettings);
        }

        private IList<IConfigurationSetting> ReadSettings()
        {
            var settings = new List<IConfigurationSetting>();

            var valueParsers = _assemblies
                .SelectMany(a => a.GetExportedTypes())
                .Where(t => typeof(IValueParser).IsAssignableFrom(t))
                .Where(t => t.IsInstantiable())
                .Select(t => (IValueParser)Activator.CreateInstance(t))
                .ToArray();

            ConfigurationConfigurator.RegisterConfigurationSettings()
                                     .FromAssemblies(_assemblies)
                                     .RegisterWithContainer(settings.Add)
                                     .WithCustomValueParsers(valueParsers)
                                     .AllowConfigurationEntriesThatDoNotHaveSettingsClasses(true)
                                     .DoYourThing();

            return settings;
        }

        public T Get<T>()
        {
            return _settings.Value.OfType<T>().First();
        }
    }
}