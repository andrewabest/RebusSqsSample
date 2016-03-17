using System.Collections.Generic;
using System.Linq;
using Autofac;
using FluentScheduler;

namespace Sample.Service.Infrastructure.Scheduler
{
    public class AutofacTaskFactory : ITaskFactory
    {
        private readonly ILifetimeScope _lifetimeScope;

        public AutofacTaskFactory(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
        }

        public ITask GetTaskInstance<T>() where T : ITask
        {
            return _lifetimeScope.Resolve<IEnumerable<T>>().Single();
        }
    }
}