using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Microscope.Boilerplate.Todo.Slices.Features.CreateTodoList;

public class CreateTodoListEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/v{apiVersion:apiVersion}/todo/todolist", CreateTodoList)
            .AllowAnonymous()
            .WithApiVersionSet(Extensions.GetModuleVersionSet(app))
            .MapToApiVersion(1);
    }
    
    private async Task<IResult> CreateTodoList([FromServices] IMediator mediator, CreateTodoListCommand command)
    {
        var resp = await mediator.Send(command);
        return Results.Ok(resp);
    }
}
