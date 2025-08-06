namespace Microscope.Boilerplate.Framework.EventSourcing;

public abstract class DomainEvent : IEvent
{
    public DateTimeOffset CreatedAt { get; set; }
}