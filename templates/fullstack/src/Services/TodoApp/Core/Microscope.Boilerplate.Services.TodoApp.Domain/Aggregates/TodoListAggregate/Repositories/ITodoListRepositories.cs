using System.Linq.Expressions;
using Microscope.SharedKernel;

namespace Microscope.Boilerplate.Services.TodoApp.Domain.Aggregates.TodoListAggregate.Repositories;

public interface ITodoListRepository
{
    public Task<IEnumerable<TodoList>> GetCreatedByAsync(string tenantId, Guid userId);
    public Task<TodoList?> GetByIdAsync(string tenantId, Guid id);
    public Task<TodoList> AddAsync(TodoList entity);
    public Task UpdateAsync(TodoList entity);
    public Task DeleteAsync(TodoList entity);
}
