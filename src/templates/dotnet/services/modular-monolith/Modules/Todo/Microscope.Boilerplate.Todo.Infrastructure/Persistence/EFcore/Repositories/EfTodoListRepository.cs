using MediatR;
using Microscope.Boilerplate.Todo.Domain.TodoListAggregate;
using Microscope.Boilerplate.Todo.Domain.TodoListAggregate.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Microscope.Boilerplate.Todo.Infrastructure.Persistence.EFcore.Repositories;

public class EfTodoListRepository : ITodoListRepository
{
    private readonly TodoAppDbContext _context;
    private readonly DbSet<TodoList> _todoLists;
    private readonly IMediator _mediatr;

    public EfTodoListRepository(TodoAppDbContext context, IMediator mediatr)
    {
        ArgumentNullException.ThrowIfNull(context);
        _context = context;
        _mediatr = mediatr;
        _todoLists = context.TodoLists;
    }

    public async Task<TodoList?> GetByIdAsync(string tenantId, Guid id)
    {
        return await _todoLists
            .Include(x => x.TodoItems)
            .Include(x => x.Tags)
            .SingleOrDefaultAsync(x => x.TenantId == tenantId && x.Id == id);
    }

    public async Task<IEnumerable<TodoList>> GetCreatedByAsync(string tenantId, Guid userId)
    {
        return await _todoLists.Where(x => x.TenantId == tenantId && x.CreatedBy == userId).ToListAsync();
    }

    public async Task DeleteAsync(TodoList entity)
    {
        AttachIfNot(entity);
        _todoLists.Remove(entity);

        await Task.CompletedTask;
    }

    public async Task UpdateAsync(TodoList entity)
    {
        AttachIfNot(entity);
        _context.Entry(entity).State = EntityState.Modified;

        await Task.CompletedTask;
    }

    public async Task<TodoList> AddAsync(TodoList entity)
    {
        var added = await _todoLists.AddAsync(entity);
        return added.Entity;
    }
    
    private void AttachIfNot(TodoList entity)
    {
        var entry = _context.ChangeTracker.Entries().FirstOrDefault(ent => ent.Entity == entity);
        if (entry != null)
        {
            return;
        }

        _context.TodoLists.Attach(entity);
    }

    public async Task SaveAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task SaveAndPublishAsync(TodoList aggregate, CancellationToken cancellationToken = default)
    {
        var domainEvents = aggregate.DomainEvents;
        
        aggregate.ClearDomainEvents();
        
        await SaveAsync(cancellationToken);
        
        foreach (var domainEvent in domainEvents)
        {
            await _mediatr.Publish(domainEvent, cancellationToken);
        }
    }

    public async Task<TodoList?> Get(Guid aggregateId, CancellationToken cancellationToken = default)
    {
        return await _todoLists
            .Include(x => x.TodoItems)
            .Include(x => x.Tags)
            .SingleOrDefaultAsync(x => x.Id == aggregateId, cancellationToken: cancellationToken);
    }
}
