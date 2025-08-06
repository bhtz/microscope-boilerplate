namespace Microscope.Boilerplate.Framework.EventSourcing;

public interface IEntity<TId>
{
    TId Id { get; }
}

public interface IEntity : IEntity<Guid>
{
    
}
