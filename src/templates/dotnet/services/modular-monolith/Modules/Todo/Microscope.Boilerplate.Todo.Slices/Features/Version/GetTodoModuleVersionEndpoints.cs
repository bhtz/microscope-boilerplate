using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.FeatureManagement.AspNetCore;

namespace Microscope.Boilerplate.Todo.Slices.Features.Version;

public class GetTodoModuleVersionEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/v{apiVersion:apiVersion}/todo/version", GetApiVersion)
            .WithApiVersionSet(TodoRestConfiguration.GetTodoModuleVersionSet(app))
            .MapToApiVersion(1)
            .AllowAnonymous()
            .WithFeatureGate(nameof(GetTodoVersionQuery));
    }

    private async Task<IResult> GetApiVersion([FromServices] IMediator mediator)
    {
        var resp = await mediator.Send(new GetTodoVersionQuery());
        return Results.Ok(resp);
    }
}
