using Autofac;

namespace Sample.Service.Infrastructure.Clock
{
    public class ClockModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SystemClock>()
                .As<IClock>();
        }
    }
}