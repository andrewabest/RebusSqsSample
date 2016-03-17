using System;
using System.Linq;
using Autofac;
using ConfigInjector.Configuration;
using ConfigInjector.Infrastructure.ValueParsers;
using Sample.Service.Infrastructure.Extensions;

namespace Sample.Service.Infrastructure.ConfigInjector
{
    public class ConfigurationSettingsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            var valueParsers = new[] { Constants.ServiceAssembly }
                .SelectMany(a => a.GetExportedTypes())
                .Where(t => typeof(IValueParser).IsAssignableFrom(t))
                .Where(t => t.IsInstantiable())
                .Select(t => (IValueParser)Activator.CreateInstance(t))
                .ToArray();

            ConfigurationConfigurator.RegisterConfigurationSettings()
                                     .FromAssemblies(Constants.ServiceAssembly)
                                     .RegisterWithContainer(configSetting => builder.RegisterInstance(configSetting)
                                                                                    .AsSelf()
                                                                                    .SingleInstance())
                                     .WithCustomValueParsers(valueParsers)
                                     .AllowConfigurationEntriesThatDoNotHaveSettingsClasses(true)
                                     .DoYourThing();
        }
    }
}