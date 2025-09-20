using Marten;
using Microscope.Boilerplate.Todo.Domain.TodoListAggregate;
using Microscope.Boilerplate.Todo.Domain.TodoListAggregate.Repositories;
using Microscope.Boilerplate.Todo.Slices;
using Microsoft.Extensions.DependencyInjection;

namespace Microscope.Boilerplate.Todo.Infrastructure.Persistence.Marten.Repositories;

public class MartenTodoListRepository([FromKeyedServices(nameof(ITodoModule))] IDocumentSession session) : ITodoListRepository
{
    public async Task<IEnumerable<TodoList>> GetCreatedByAsync(string tenantId, Guid userId)
    {
        var results = await session.Query<TodoList>()
            .Where(x => x.CreatedBy == userId)
            .ToListAsync();
        return results;
    }

    public async Task<TodoList?> GetByIdAsync(string tenantId, Guid id)
    {
        return await session.LoadAsync<TodoList>(id);
    }

    public Task<TodoList> AddAsync(TodoList entity)
    {
        session.Store(entity);
        return Task.FromResult(entity);
    }

    public Task UpdateAsync(TodoList entity)
    {
        session.Update(entity);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(TodoList entity)
    {
        session.Delete(entity);
        return Task.CompletedTask;
    }
}