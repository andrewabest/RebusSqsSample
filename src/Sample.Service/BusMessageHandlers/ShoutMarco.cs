using System;
using System.Threading.Tasks;
using Rebus.Bus;
using Rebus.Handlers;
using Sample.MessageContracts.Commands;
using Sample.MessageContracts.Events;
using Sample.Service.Infrastructure.Clock;

namespace Sample.Service.BusMessageHandlers
{
    public class ShoutMarco : IHandleMessages<MarcoCommand>
    {
        private readonly IBus _bus;
        private readonly IClock _clock;

        public ShoutMarco(IBus bus, IClock clock)
        {
            _bus = bus;
            _clock = clock;
        }

        public Task Handle(MarcoCommand message)
        {
            Console.WriteLine("Marco!");

            return _bus.Publish(new PoloEvent(message.Id, _clock.UtcNow));
        }
    }
}