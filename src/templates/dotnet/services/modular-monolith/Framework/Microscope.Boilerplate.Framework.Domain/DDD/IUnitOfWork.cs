namespace Microscope.Boilerplate.Framework.Domain.DDD;

public interface IUnitOfWork : IDisposable
{
    Task SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    Task SaveChangesAndDispatchEventsAsync(CancellationToken cancellationToken = default(CancellationToken));
    Task<T> EncapsulateInTransaction<T>(Func<Task<T>> action, string typeName);
    bool HasActiveTransaction { get; }
}