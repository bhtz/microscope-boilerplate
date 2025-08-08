using System.Data;
using Marten;
using MediatR;
using Microscope.Boilerplate.Todo.Infrastructure.Persistence.EFcore;
using Microscope.Framework.Domain.DDD;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Microscope.Boilerplate.Todo.Infrastructure.Persistence.Marten;

public class MartenUnitOfWork(IDocumentSession session, IMediator mediator) : IUnitOfWork
{
    private readonly IDocumentSession _session = session;
    
    public bool HasActiveTransaction => false;

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
        // _session.BeginTransaction();
        return await action();
    }

    public void Dispose()
    {
        this._session.Dispose();
    }
}