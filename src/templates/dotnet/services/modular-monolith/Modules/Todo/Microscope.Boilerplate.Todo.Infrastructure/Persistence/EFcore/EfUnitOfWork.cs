using System.Data;
using MediatR;
using Microscope.Boilerplate.Framework.Domain.DDD;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Microscope.Boilerplate.Todo.Infrastructure.Persistence.EFcore;

public class EfUnitOfWork : IUnitOfWork
{
    private readonly TodoAppDbContext _context;
    private IDbContextTransaction _currentTransaction;
    private readonly IMediator _mediator;

    public EfUnitOfWork(TodoAppDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }
    public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;

    public bool HasActiveTransaction => _currentTransaction != null;

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await this._context.SaveChangesAsync(cancellationToken);
    }

    public async Task SaveChangesAndDispatchEventsAsync(CancellationToken cancellationToken = default)
    {
        var domainEntities = this._context.ChangeTracker
            .Entries<AggregateRoot>()
            .Where(x => x.Entity.DomainEvents.Any())
            .ToList();

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();

        domainEntities.ToList()
            .ForEach(entity => entity.Entity.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
            await this._mediator.Publish(domainEvent, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<T> EncapsulateInTransaction<T>(Func<Task<T>> action, string typeName)
    {
        T response = default(T);
        var strategy = this._context.Database.CreateExecutionStrategy();

        await strategy.ExecuteAsync(async () =>
        {
            Guid transactionId;

            using (var transaction = await this.BeginTransactionAsync())
            {
                response = await action();
                await this.CommitTransactionAsync(transaction);
                transactionId = transaction.TransactionId;
            }
        });

        return response;
    }

    private async Task CommitTransactionAsync(IDbContextTransaction transaction)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));
        if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

        try
        {
            await _context.SaveChangesAsync();
            transaction.Commit();
        }
        catch
        {
            RollbackTransaction();
            throw;
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }

    private void RollbackTransaction()
    {
        try
        {
            _currentTransaction?.Rollback();
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }

    private async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        if (_currentTransaction != null) return null;

        _currentTransaction = await _context.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

        return _currentTransaction;
    }

    public void Dispose()
    {
        this._context.Dispose();
    }
}