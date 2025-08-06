using System.Text.Json.Serialization;

namespace Microscope.Boilerplate.Framework.EventSourcing;

public abstract class AggregateRoot : AggregateRoot<Guid>, IAggregateRoot
{
}

public abstract class AggregateRoot<TId> : IAggregateRoot<TId>
{
    public TId Id { get; protected set; } = default!;
    
    public String? TenantId { get; protected set; }
    
    [JsonIgnore]
    private List<DomainEvent> _domainEvents;

    [JsonIgnore]
    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents?.AsReadOnly();

    public void AddDomainEvent(DomainEvent eventItem)
    {
        _domainEvents = _domainEvents ?? new List<DomainEvent>();
        _domainEvents.Add(eventItem);
    }

    public void RemoveDomainEvent(DomainEvent eventItem)
    {
        _domainEvents?.Remove(eventItem);
    }

    public void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }
}