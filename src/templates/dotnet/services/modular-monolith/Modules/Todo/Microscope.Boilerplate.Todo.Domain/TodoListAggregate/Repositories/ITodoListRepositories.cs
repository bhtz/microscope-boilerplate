using Microscope.Boilerplate.Framework.Domain.DDD;

namespace Microscope.Boilerplate.Todo.Domain.TodoListAggregate.Repositories;

public interface ITodoListRepository : IRepository<TodoList>
{
    public Task<IEnumerable<TodoList>> GetCreatedByAsync(string tenantId, Guid userId);
    public Task<TodoList?> GetByIdAsync(string tenantId, Guid id);
    public Task<TodoList> AddAsync(TodoList entity);
    public Task UpdateAsync(TodoList entity);
    public Task DeleteAsync(TodoList entity);
}
