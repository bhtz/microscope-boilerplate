using Carter;
using MediatR;
using Microscope.Boilerplate.Todo.Slices.Features.DeleteTodoList;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Microscope.Boilerplate.Todo.Slices.Features.GetTodoLists;

public class GetTodoListEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/v{apiVersion:apiVersion}/todo/todo-lists", GetTodoLists)
            .WithApiVersionSet(Extensions.GetTodoModuleVersionSet(app))
            .MapToApiVersion(1)
            .RequireAuthorization();

    }
    
    private async Task<IResult> GetTodoLists([FromServices] IMediator mediator)
    {
        var resp = await mediator.Send(new GetTodoListQuery());
        return Results.Ok(resp);
    }
}
