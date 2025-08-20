using System.Text.Json.Serialization;

namespace Microscope.Boilerplate.Framework.Domain.DDD;

public abstract class AggregateRoot : AggregateRoot<Guid>, IAggregateRoot
{
}

public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot<TId>
{
    public string? TenantId { get; protected set; }
    
    [JsonIgnore]
    private readonly List<IEvent> _domainEvents = [];

    [JsonIgnore]
    public IReadOnlyCollection<IEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(IEvent eventItem)
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