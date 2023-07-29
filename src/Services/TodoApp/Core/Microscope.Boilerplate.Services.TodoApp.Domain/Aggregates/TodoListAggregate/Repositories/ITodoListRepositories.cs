using System.Linq.Expressions;
using Microscope.SharedKernel;

namespace Microscope.Boilerplate.Services.TodoApp.Domain.Aggregates.TodoListAggregate.Repositories;

public interface ITodoListRepository : IBaseRepository<TodoList>
{
    public Task<TodoList> GetByIdAsync(string tenantId, Guid id);
    public Task<IEnumerable<TodoList>> GetAllByTenantAsync(string tenantId, Expression<Func<TodoList, bool>> filters, bool hydrated);
    public Task<IEnumerable<TodoList>> GetCreatedBy(string tenantId, Guid userId);
}
