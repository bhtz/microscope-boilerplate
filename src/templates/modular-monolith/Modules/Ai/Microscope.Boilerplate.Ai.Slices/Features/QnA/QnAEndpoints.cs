using Carter;
using MediatR;
using Microscope.Management.Todo.Slices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Microscope.Boilerplate.Ai.Slices.Features.QnA;

public class QnAEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/v{apiVersion:apiVersion}/QnA", GetQnA)
            .WithApiVersionSet(AiRestConfiguration.GetAiModuleVersionSet(app))
            .MapToApiVersion(1)
            .RequireAuthorization();
    }

    private async Task<IResult> GetQnA([FromServices] IMediator mediator, [FromBody] QnAQuery command)
    {
        var resp = await mediator.Send(command);
        return Results.Ok(resp);
    }
}
