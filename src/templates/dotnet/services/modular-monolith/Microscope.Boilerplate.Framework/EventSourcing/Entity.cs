namespace Microscope.Boilerplate.Framework.EventSourcing;

public abstract class Entity<TId> : IEntity<TId>
{
    public TId Id { get; protected set; } = default!;
}

public abstract class Entity : Entity<Guid>, IEntity
{
    
}
