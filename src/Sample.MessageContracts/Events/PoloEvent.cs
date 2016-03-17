using System;

namespace Sample.MessageContracts.Events
{
    public class PoloEvent
    {
        public PoloEvent(string id, DateTimeOffset timestamp)
        {
            Id = id;
            Timestamp = timestamp;
        }

        public string Id { get; }

        public DateTimeOffset Timestamp { get; }
    }
}
