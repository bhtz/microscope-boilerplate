using HotChocolate.Authorization;
using MediatR;

namespace Microscope.Boilerplate.Ai.Slices.Features.QnA;

[QueryType]
public static class QnAResolver
{
    [Authorize]
    public static async Task<QnAResult> QnA([Service] IMediator mediator, QnAQuery query, CancellationToken cancellationToken = default)
    {
        return await mediator.Send(query, cancellationToken);
    }
}
