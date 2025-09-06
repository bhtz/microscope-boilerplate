using Microscope.Boilerplate.Framework.Domain.CQRS;
using Microsoft.FeatureManagement;

namespace Microscope.Boilerplate.Todo.Slices.Features.Version;

public record GetTodoVersionQuery() : IQuery<string>;

public class GetTodoModuleVersionQueryHandler(IFeatureManager featureManager) : IRequestHandler<GetTodoVersionQuery, string>
{
    public async Task<string> Handle(GetTodoVersionQuery request, CancellationToken cancellationToken)
    {
        var result = await featureManager.IsEnabledAsync(nameof(GetTodoVersionQuery));

        // TODO: move feature management check reliable to a protocol (grpc, graphql, rest)
        return !result 
            ? throw new NotImplementedException($"Feature {nameof(GetTodoVersionQuery)} not implemented") 
            : "1.0.0";
    }
}