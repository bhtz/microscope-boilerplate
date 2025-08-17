using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Microscope.Boilerplate.Todo.Slices.Features.Version;

public class GetTodoModuleVersionEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/v{apiVersion:apiVersion}/todo/version", GetApiVersion)
            .WithApiVersionSet(Extensions.GetModuleVersionSet(app))
            .MapToApiVersion(1)
            .AllowAnonymous();
    }

    private async Task<IResult> GetApiVersion([FromServices] IMediator mediator)
    {
        var resp = await mediator.Send(new GetTodoVersionQuery());
        return Results.Ok(resp);
    }
}
