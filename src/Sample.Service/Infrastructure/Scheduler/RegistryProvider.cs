using System.Collections.Generic;

namespace Sample.Service.Infrastructure.Scheduler
{
    public class RegistryProvider : FluentScheduler.Registry
    {
        public RegistryProvider(IEnumerable<IRegistry> registries)
        {
            foreach (var registry in registries)
            {
                registry.AddSchedule(this);
            }
        }
    }
}