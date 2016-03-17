using Autofac;
using Rebus.Bus;

namespace Sample.Service.Infrastructure.Sqs
{
    public class SqsRebusModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SqsRebusFactory>()
                .AsSelf();

            builder.Register(c => c.Resolve<SqsRebusFactory>().Create())
                .As<IBus>()
                .AutoActivate()
                .SingleInstance();
        }
    }
}