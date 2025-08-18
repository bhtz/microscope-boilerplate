using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Microscope.Boilerplate.Todo.Slices.Features.ToggleTodoItem;

public class ToggleTodoItemEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/v{apiVersion:apiVersion}/todo/todo-lists/{id:guid}/items/{itemId:guid}", ToggleTodoItem)
            .WithApiVersionSet(TodoRestConfiguration.GetTodoModuleVersionSet(app))
            .MapToApiVersion(1)
            .RequireAuthorization();

    }
    
    private async Task<IResult> ToggleTodoItem([FromServices] IMediator mediator, [FromRoute]Guid id, [FromRoute]Guid itemId, [FromBody]ToggleTodoItemCommand command)
    {
        if (id != command.TodoListId)
        {
            return Results.BadRequest();
        }
        
        var resp = await mediator.Send(command);
        return Results.Ok(resp);
    }
}
