using Microscope.SharedKernel;

namespace Microscope.Boilerplate.Services.TodoApp.Application.Features.TodoLists.Queries.GetTodoLists;

public class GetTodoListQuery : IQuery<IEnumerable<TodoListQueryResult>>
{
    public string? Search { get; set; }
    
    public GetTodoListQuery(string search)
    {
        Search = search;
    }

    public GetTodoListQuery()
    {
        
    }
}

public record TodoListQueryResult
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    // Todo: add collections
}

