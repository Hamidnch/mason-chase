using NetDevPack.Messaging;

namespace Mc2.CrudTest.DomainCore.Events;

public interface IEventStore
{
    void Save<T>(T theEvent) where T : Event;
}