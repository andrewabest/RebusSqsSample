using System;
using FluentScheduler;
using Rebus.Bus;
using Sample.MessageContracts.Commands;
using Sample.Service.Infrastructure.Clock;
using Sample.Service.Infrastructure.Scheduler;

namespace Sample.Service.Tasks
{
    public class TickTask : ITask, IRegistry
    {
        private readonly IBus _bus;
        private readonly IClock _clock;
        
        public TickTask(IBus bus, IClock clock)
        {
            _bus = bus;
            _clock = clock;
            
        }

        public void AddSchedule(Registry registry)
        {
            registry.Schedule<TickTask>()
                .ToRunEvery(5).Seconds();
        }

        public void Execute()
        {
            var command = new MarcoCommand(Guid.NewGuid().ToString(), _clock.UtcNow);

            _bus.Send(command);
        }
    }
}