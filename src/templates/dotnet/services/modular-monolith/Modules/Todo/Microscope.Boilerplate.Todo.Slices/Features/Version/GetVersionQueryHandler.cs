using MediatR;
using Microscope.Boilerplate.Framework.CQRS;

namespace Microscope.Boilerplate.Todo.Slices.Features.Version;

public record GetVersionQuery() : IQuery<string>;

public class GetVersionQueryHandler : IRequestHandler<GetVersionQuery, string>
{
    public async Task<string> Handle(GetVersionQuery request, CancellationToken cancellationToken)
    {
        return "1.0.0";
    }
}