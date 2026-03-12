using Asp.Versioning;
using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Microscope.Management.Todo.Slices;

public static class AiRestConfiguration
{
    public static ApiVersionSet GetAiModuleVersionSet(IEndpointRouteBuilder app)
    {
        return app.NewApiVersionSet()
            .HasApiVersion(new ApiVersion(1))
            .Build();
    }
}