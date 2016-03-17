using Serilog;

namespace Sample.Service.Infrastructure.Logging
{
    public interface ILoggerFactory
    {
        ILogger CreateLogger();
    }
}