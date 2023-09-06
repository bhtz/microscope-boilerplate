using Microscope.SharedKernel;

namespace Microscope.Boilerplate.Services.TodoApp.Application.Features.TodoLists.Queries.GetTodoLists;

public class GetTodoListQuery : IQuery<IEnumerable<GetTodoListQueryResult>>
{
    public GetTodoListQuery()
    {
        
    }
}

public record GetTodoListQueryResult
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public bool IsCompleted { get; init; }
}

