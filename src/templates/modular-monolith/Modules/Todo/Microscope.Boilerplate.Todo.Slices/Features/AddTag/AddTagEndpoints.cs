using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Microscope.Boilerplate.Todo.Slices.Features.AddTag;

public class AddTagEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/v{apiVersion:apiVersion}/todo/todo-lists/{id:guid}/tags", AddTag)
            .WithApiVersionSet(TodoRestConfiguration.GetTodoModuleVersionSet(app))
            .MapToApiVersion(1)
            .RequireAuthorization();
    }
    
    private async Task<IResult> AddTag([FromServices] IMediator mediator, [FromRoute]Guid id, [FromBody]AddTagCommand command)
    {
        if (id != command.TodoListId)
        {
            return Results.BadRequest();
        }
        
        var resp = await mediator.Send(command);
        return Results.Ok(resp);
    }
}
