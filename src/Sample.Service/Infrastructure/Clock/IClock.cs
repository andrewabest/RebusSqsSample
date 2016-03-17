using System;

namespace Sample.Service.Infrastructure.Clock
{
    public interface IClock
    {
        DateTimeOffset UtcNow { get; }
        DateTime Now { get; }
    }
}