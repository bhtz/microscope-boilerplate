using Microscope.SharedKernel;

namespace Microscope.Boilerplate.Services.TodoApp.Application.Features.TodoLists.Queries.GetTodoListsById;

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
    public bool IsCompleted { get; init; }
    public IEnumerable<TodoItemResult> TodoItems { get; init; }
    public IEnumerable<TagResult> Tags { get; init; }
}

public record TodoItemResult
{
    public Guid Id { get; init; }
    public string Label { get; init; }
    public bool IsCompleted { get; init; }
}

public record TagResult
{
    public string Label { get; init; }
    public string Color { get; init; }
}
