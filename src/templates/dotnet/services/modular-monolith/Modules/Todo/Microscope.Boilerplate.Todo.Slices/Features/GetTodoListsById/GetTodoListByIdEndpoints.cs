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
            .WithApiVersionSet(Extensions.GetModuleVersionSet(app))
            .MapToApiVersion(1)
            .RequireCommonAuthorization();
    }
    
    private async Task<IResult> GetTodoListById([FromServices] IMediator mediator, [FromRoute]Guid id)
    {
        var resp = await mediator.Send(new GetTodoListByIdQuery(id));
        return Results.Ok(resp);
    }
}
