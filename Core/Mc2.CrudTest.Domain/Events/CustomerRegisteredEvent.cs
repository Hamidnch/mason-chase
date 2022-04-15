using NetDevPack.Messaging;

namespace Mc2.CrudTest.Domain.Events
{
    public class CustomerRegisteredEvent : Event
    {
        public CustomerRegisteredEvent(Guid id, string name, string email, DateTime birthDate)
        {
            Id = id;
            Email = email;
            BirthDate = birthDate;
            AggregateId = id;
        }

        private Guid Id { get; set; }

        private string? Email { get; set; }

        private DateTime BirthDate { get; set; }
    }
}
