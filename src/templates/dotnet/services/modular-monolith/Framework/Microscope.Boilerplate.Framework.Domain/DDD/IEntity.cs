namespace Microscope.Boilerplate.Framework.Domain.DDD;

public interface IEntity<TId>
{
    TId Id { get; }
}

public interface IEntity : IEntity<Guid>
{
    
}
