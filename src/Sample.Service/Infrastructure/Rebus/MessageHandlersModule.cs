using Autofac;
using Rebus.Handlers;

namespace Sample.Service.Infrastructure.Rebus
{
    public class MessageHandlersModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Constants.ServiceAssembly)
                .AsClosedTypesOf(typeof (IHandleMessages<>));
        }
    }
}