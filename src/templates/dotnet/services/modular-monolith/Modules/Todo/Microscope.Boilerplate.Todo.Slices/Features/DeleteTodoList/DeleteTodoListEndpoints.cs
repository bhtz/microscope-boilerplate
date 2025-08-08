using Carter;
using MediatR;
using Microscope.Boilerplate.Todo.Slices.Features.CreateTodoItem;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Microscope.Boilerplate.Todo.Slices.Features.DeleteTodoItem;

public class DeleteTodoItemEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/v{apiVersion:apiVersion}/todo/todo-lists/{id:guid}/items", DeleteTodoItem)
            .WithApiVersionSet(Extensions.GetModuleVersionSet(app))
            .MapToApiVersion(1)
            .AllowAnonymous(); // Todo: to remove
    }
    
    private async Task<IResult> DeleteTodoItem([FromServices] IMediator mediator, [FromRoute]Guid id, [FromBody]DeleteTodoItemCommand command)
    {
        if (id != command.TodoListId)
        {
            return Results.BadRequest();
        }
        
        var resp = await mediator.Send(command);
        return Results.Ok(resp);
    }
}
