using Autofac;
using FluentScheduler;

namespace Sample.Service.Infrastructure.Scheduler
{
    public class SchedulerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AutofacTaskFactory>()
                .AsSelf()
                .SingleInstance();

            builder.RegisterType<RegistryProvider>()
                .AsSelf()
                .SingleInstance();

            builder.RegisterAssemblyTypes(Constants.ServiceAssembly)
                .As<IRegistry>()
                .SingleInstance();

            builder.RegisterAssemblyTypes(Constants.ServiceAssembly)
                .As<ITask>()
                .AsSelf();
        }
    }
}