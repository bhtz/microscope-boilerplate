using System.Text.Json.Serialization;

namespace Microscope.Boilerplate.Framework.EventSourcing;

public abstract class AggregateRoot : AggregateRoot<Guid>, IAggregateRoot
{
}

public abstract class AggregateRoot<TId> : IAggregateRoot<TId>
{
    public TId Id { get; protected set; } = default!;
    
    public string? TenantId { get; protected set; }
    
    [JsonIgnore]
    private readonly List<IEvent> _domainEvents = [];

    [JsonIgnore]
    public IReadOnlyCollection<IEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(DomainEvent eventItem)
    {
        _domainEvents.Add(eventItem);
    }

    public void RemoveDomainEvent(IEvent eventItem)
    {
        _domainEvents.Remove(eventItem);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}