using System.Linq.Expressions;
using Microscope.Boilerplate.Services.TodoApp.Domain.Aggregates.TodoListAggregate.Repositories;
using Microscope.Boilerplate.Services.TodoApp.Infrastructure.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Microscope.Boilerplate.Services.TodoApp.Infrastructure.Persistence.Repositories;

public class TodoListRepository : EFRepositoryBase<TodoApp.Domain.Aggregates.TodoListAggregate.TodoList>, ITodoListRepository
{
    public TodoListRepository(TodoAppDbContext context) : base(context)
    {
    }

    public async Task<Domain.Aggregates.TodoListAggregate.TodoList> GetByIdAsync(string tenantId, Guid id)
    {
        return await this.DbSet
            .Include(x => x.TodoItems)
            .SingleOrDefaultAsync(x => x.TenantId == tenantId && x.Id == id);
    }

    public async Task<IEnumerable<Domain.Aggregates.TodoListAggregate.TodoList>> GetAllByTenantAsync(string tenantId, Expression<Func<TodoApp.Domain.Aggregates.TodoListAggregate.TodoList, bool>> filters, bool hydrated)
    {
        return await this.DbSet.Where(x => x.TenantId == tenantId).ToListAsync();
    }

    public async Task<IEnumerable<Domain.Aggregates.TodoListAggregate.TodoList>> GetCreatedBy(string tenantId, Guid userId)
    {
        return await this.DbSet.Where(x => x.TenantId == tenantId && x.CreatedBy == userId).ToListAsync();
    }
}