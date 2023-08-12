using System.Linq.Expressions;
using Microscope.Boilerplate.Services.TodoApp.Domain.Aggregates.TodoListAggregate.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Microscope.Boilerplate.Services.TodoApp.Infrastructure.Persistence.Repositories;

public class TodoListRepository : ITodoListRepository
{
    private readonly TodoAppDbContext _context;
    private readonly DbSet<Domain.Aggregates.TodoListAggregate.TodoList> _todoLists;

    public TodoListRepository(TodoAppDbContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        _context = context;
        _todoLists = context.TodoLists;
    }

    public async Task<Domain.Aggregates.TodoListAggregate.TodoList> GetByIdAsync(string tenantId, Guid id)
    {
        return await _todoLists
            .Include(x => x.TodoItems)
            .Include(x => x.Tags)
            .SingleOrDefaultAsync(x => x.TenantId == tenantId && x.Id == id);
    }

    public async Task<IEnumerable<Domain.Aggregates.TodoListAggregate.TodoList>> GetAllAsync(string tenantId, Expression<Func<Domain.Aggregates.TodoListAggregate.TodoList, bool>> filters, bool hydrated)
    {
        return await this._todoLists.Where(x => x.TenantId == tenantId).ToListAsync();
    }

    public async Task<IEnumerable<Domain.Aggregates.TodoListAggregate.TodoList>> GetCreatedByAsync(string tenantId, Guid userId)
    {
        return await _todoLists.Where(x => x.TenantId == tenantId && x.CreatedBy == userId).ToListAsync();
    }

    public async Task DeleteAsync(Domain.Aggregates.TodoListAggregate.TodoList entity)
    {
        AttachIfNot(entity);
        _todoLists.Remove(entity);
    }

    public async Task UpdateAsync(Domain.Aggregates.TodoListAggregate.TodoList entity)
    {
        AttachIfNot(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }

    public async Task<Domain.Aggregates.TodoListAggregate.TodoList> AddAsync(Domain.Aggregates.TodoListAggregate.TodoList entity)
    {
        var added = await _todoLists.AddAsync(entity);
        return added.Entity;
    }
    
    private void AttachIfNot(Domain.Aggregates.TodoListAggregate.TodoList entity)
    {
        var entry = _context.ChangeTracker.Entries().FirstOrDefault(ent => ent.Entity == entity);
        if (entry != null)
        {
            return;
        }

        _context.TodoLists.Attach(entity);
    }
}