using Microscope.SharedKernel;

namespace Microscope.Boilerplate.Services.TodoApp.Application.Features.TodoLists.Queries.GetTodoLists;

public class GetTodoListByIdQuery : IQuery<TodoListByIdQueryResult>
{
    public Guid Id { get; set; }
    
    public GetTodoListByIdQuery(Guid id)
    {
        Id = id;
    }
}

public record TodoListByIdQueryResult
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    // Todo: add collections
}

