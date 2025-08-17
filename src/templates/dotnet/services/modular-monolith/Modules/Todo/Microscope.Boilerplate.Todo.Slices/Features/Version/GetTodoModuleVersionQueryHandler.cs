using MediatR;
using Microscope.Boilerplate.Framework.Domain.CQRS;

namespace Microscope.Boilerplate.Todo.Slices.Features.Version;

public record GetTodoVersionQuery() : IQuery<string>;

public class GetTodoModuleVersionQueryHandler : IRequestHandler<GetTodoVersionQuery, string>
{
    public async Task<string> Handle(GetTodoVersionQuery request, CancellationToken cancellationToken)
    {
        return "1.0.0";
    }
}