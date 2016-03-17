using System;

namespace Sample.MessageContracts.Commands
{
    public class MarcoCommand
    {
        public MarcoCommand(string id, DateTimeOffset timestamp)
        {
            Id = id;
            Timestamp = timestamp;
        }

        public string Id { get; }

        public DateTimeOffset Timestamp { get; }
    }
}