using NetDevPack.Messaging;

namespace Mc2.CrudTest.DomainCore.Events
{
    public class StoredEvent : Event
    {
        public StoredEvent(Message theEvent, string data, string user)
        {
            Id = Guid.NewGuid();
            AggregateId = theEvent.AggregateId;
            MessageType = theEvent.MessageType;
            Data = data;
            User = user;
        }

        // EF Constructor
        protected StoredEvent() { }

        private Guid Id { get; set; }

        private string Data { get; set; }

        private string User { get; set; }
    }
}
