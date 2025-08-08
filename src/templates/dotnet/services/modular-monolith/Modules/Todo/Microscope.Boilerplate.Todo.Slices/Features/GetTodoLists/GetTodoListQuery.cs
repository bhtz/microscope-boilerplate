using Microscope.Framework.Domain.CQRS;

namespace Microscope.Boilerplate.Todo.Slices.Features.GetTodoLists;

public record GetTodoListQuery(): IQuery<IEnumerable<GetTodoListQueryResult>>;

public record GetTodoListQueryResult(Guid Id, string Name, bool IsCompleted)
{

}

