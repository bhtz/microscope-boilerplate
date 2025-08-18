using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Microscope.Boilerplate.Todo.Slices.Features.GetTodoListsById;

public class GetTodoListByIdEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/v{apiVersion:apiVersion}/todo/todo-lists/{id:guid}", GetTodoListById)
            .WithApiVersionSet(TodoRestConfiguration.GetTodoModuleVersionSet(app))
            .MapToApiVersion(1)
            .RequireAuthorization();
    }
    
    private async Task<IResult> GetTodoListById([FromServices] IMediator mediator, [FromRoute]Guid id)
    {
        var resp = await mediator.Send(new GetTodoListByIdQuery(id));
        return Results.Ok(resp);
    }
}
