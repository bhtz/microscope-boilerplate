using Asp.Versioning;
using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Microscope.Boilerplate.Todo.Slices;

public static class TodoRestConfiguration
{
    public static ApiVersionSet GetTodoModuleVersionSet(IEndpointRouteBuilder app)
    {
        return app.NewApiVersionSet()
            .HasApiVersion(new ApiVersion(1))
            .Build();
    }
}