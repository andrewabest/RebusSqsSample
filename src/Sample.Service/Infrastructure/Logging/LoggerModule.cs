using Autofac;
using Serilog;

namespace Sample.Service.Infrastructure.Logging
{
    public class LoggerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => Log.Logger)
                .As<ILogger>();
        }
    }
}