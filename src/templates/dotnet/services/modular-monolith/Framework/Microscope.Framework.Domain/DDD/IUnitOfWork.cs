namespace Microscope.SharedKernel;

public interface IUnitOfWork : IDisposable
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    Task<bool> SaveChangesAndDispatchEventsAsync(CancellationToken cancellationToken = default(CancellationToken));
    Task<T> EncapsulateInTransaction<T>(Func<Task<T>> action, string typeName);
    bool HasActiveTransaction { get; }
}