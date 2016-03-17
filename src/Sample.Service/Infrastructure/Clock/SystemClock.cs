using System;

namespace Sample.Service.Infrastructure.Clock
{
    public class SystemClock : IClock
    {
        public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
        public DateTime Now => DateTime.Now;
    }
}