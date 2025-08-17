using Microscope.Boilerplate.Framework.Domain.CQRS;

namespace Microscope.Boilerplate.Todo.Slices.Features.GetTodoListsById;

public record GetTodoListByIdQuery(Guid Id) : IQuery<GetTodoListByIdQueryResult>
{
    
}

public record GetTodoListByIdQueryResult
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public bool IsCompleted { get; init; }
    public IEnumerable<TodoItemResult> TodoItems { get; init; }
    public IEnumerable<TagResult> Tags { get; init; }
}

public record TodoItemResult(Guid Id, string Label, bool IsCompleted);

public record TagResult(string Label, string Color);
