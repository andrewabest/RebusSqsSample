using System;
using System.Threading.Tasks;
using Rebus.Handlers;
using Sample.MessageContracts.Events;

namespace Sample.Service.BusMessageHandlers
{
    public class ReplyWithPolo : IHandleMessages<PoloEvent>
    {
        public Task Handle(PoloEvent message)
        {
            Console.WriteLine("Polo!");

            return Task.FromResult(0);
        }
    }
}