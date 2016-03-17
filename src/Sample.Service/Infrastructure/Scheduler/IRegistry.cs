using FluentScheduler;

namespace Sample.Service.Infrastructure.Scheduler
{
    public interface IRegistry
    {
        void AddSchedule(Registry registry);
    }
}