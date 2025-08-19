namespace Microscope.Boilerplate.Framework.Domain.DDD;

public abstract record DomainEvent : IEvent
{
    public DateTimeOffset CreatedAt { get; set; }
}