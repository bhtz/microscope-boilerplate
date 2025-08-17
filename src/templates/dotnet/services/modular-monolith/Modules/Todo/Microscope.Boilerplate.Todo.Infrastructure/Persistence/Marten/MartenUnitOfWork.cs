using System.Data;
using Marten;
using MediatR;
using Microscope.Boilerplate.Framework.Domain.DDD;
using Npgsql;

namespace Microscope.Boilerplate.Todo.Infrastructure.Persistence.Marten;

public class MartenUnitOfWork(IDocumentSession session, IMediator mediator) : IUnitOfWork
{
    private readonly IDocumentSession _session = session;
    private NpgsqlTransaction _currentTransaction;
    
    public bool HasActiveTransaction => _currentTransaction != null;

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await this._session.SaveChangesAsync(cancellationToken);
    }

    public async Task SaveChangesAndDispatchEventsAsync(CancellationToken cancellationToken = default)
    {
        var domainEntities = this._session.PendingChanges
            .AllChangedFor<AggregateRoot>()
            .Where(x => x.DomainEvents.Any())
            .ToList();

        var domainEvents = domainEntities
            .SelectMany(x => x.DomainEvents)
            .ToList();

        domainEntities.ToList()
            .ForEach(entity => entity.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
            await mediator.Publish(domainEvent, cancellationToken);

        await _session.SaveChangesAsync(cancellationToken);
    }

    public async Task<T> EncapsulateInTransaction<T>(Func<Task<T>> action, string typeName)
    {
        T response = default(T);

        // Marten n'a pas d'équivalent direct à CreateExecutionStrategy, 
        // mais nous pouvons implémenter une logique de retry simple si nécessaire
        using (var transaction = await BeginTransactionAsync())
        {
            response = await action();
            await CommitTransactionAsync(transaction);
        }

        return response;
    }

    private async Task<NpgsqlTransaction> BeginTransactionAsync()
    {
        if (_currentTransaction != null) return _currentTransaction;

        // Commencer une transaction avec Marten
        var connection = _session.Connection;
        if (connection.State != ConnectionState.Open)
        {
            await connection.OpenAsync();
        }

        _currentTransaction = await connection.BeginTransactionAsync(IsolationLevel.ReadCommitted);
        //_session.Connection.EnlistTransaction(_currentTransaction);

        return _currentTransaction;
    }

    private async Task CommitTransactionAsync(NpgsqlTransaction transaction)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));
        if (transaction != _currentTransaction) 
            throw new InvalidOperationException($"La transaction fournie n'est pas la transaction courante");

        try
        {
            await _session.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await RollbackTransactionAsync();
            throw;
        }
        finally
        {
            await DisposeCurrentTransactionAsync();
        }
    }

    private async Task RollbackTransactionAsync()
    {
        try
        {
            if (_currentTransaction != null)
                await _currentTransaction.RollbackAsync();
        }
        finally
        {
            await DisposeCurrentTransactionAsync();
        }
    }

    private async Task DisposeCurrentTransactionAsync()
    {
        if (_currentTransaction != null)
        {
            await _currentTransaction.DisposeAsync();
            _currentTransaction = null;
        }
    }

    public void Dispose()
    {
        _currentTransaction?.Dispose();
        this._session.Dispose();
    }
}