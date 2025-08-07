using Marten;
using MediatR;
using Microscope.Boilerplate.Todo.Domain.TodoListAggregate;
using Microscope.Boilerplate.Todo.Domain.TodoListAggregate.Repositories;
using Microscope.Framework.Domain.DDD;

namespace Microscope.Boilerplate.Todo.Infrastructure.Persistence.Marten.Repositories;

public class MartenTodoListRepository(IDocumentSession session, IMediator mediator) : ITodoListRepository
{
    public async Task<IEnumerable<TodoList>> GetCreatedByAsync(string tenantId, Guid userId)
    {
        return session.Query<TodoList>()
            .Where(x => x.CreatedBy == userId)
            .ToList();
    }

    public async Task<TodoList?> GetByIdAsync(string tenantId, Guid id)
    {
        return await session.LoadAsync<TodoList>(id);
    }

    public async Task<TodoList> AddAsync(TodoList entity)
    {
        session.Store(entity);
        return entity;
    }

    public async Task UpdateAsync(TodoList entity)
    {
        session.Update(entity);
    }

    public async Task DeleteAsync(TodoList entity)
    {
        session.Delete(entity);
    }

    public async Task Save(CancellationToken cancellationToken = default)
    {
        await session.SaveChangesAsync(cancellationToken);
    }

    public async Task SaveAndPublish(TodoList aggregate, CancellationToken cancellationToken = default)
    {

    }

    public async Task SaveAsync(CancellationToken cancellationToken = default)
    {
        await session.SaveChangesAsync(cancellationToken);
    }

    public async Task SaveAndPublishAsync(TodoList aggregate, CancellationToken cancellationToken = default)
    {
        var domainEvents = aggregate.DomainEvents;
        
        aggregate.ClearDomainEvents();
        
        await Save(cancellationToken);
        
        foreach (var domainEvent in domainEvents)
        {
            await mediator.Publish(domainEvent, cancellationToken);
        }
    }

    public async Task<TodoList?> Get(Guid aggregateId, CancellationToken cancellationToken = default)
    {
        return await session.LoadAsync<TodoList>(aggregateId, cancellationToken);
    }
}