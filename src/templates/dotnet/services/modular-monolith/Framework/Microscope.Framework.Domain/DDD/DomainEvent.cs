namespace Microscope.Framework.Domain.DDD;

public abstract class DomainEvent : IEvent
{
    public DateTimeOffset CreatedAt { get; set; }
}