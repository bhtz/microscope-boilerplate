namespace Microscope.Boilerplate.Framework.EventSourcing;

public interface IRepository
{
}

public interface IRepository<T> : IRepository<T, Guid> where T : class, IAggregateRoot<Guid>
{

}

public interface IRepository<T, TId> : IRepository where T : class, IAggregateRoot<TId>
{

}
