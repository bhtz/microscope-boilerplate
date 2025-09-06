using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Microscope.Boilerplate.Todo.Slices.Features.CreateTodoItem;

public class CreateTodoItemEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/v{apiVersion:apiVersion}/todo/todo-lists/{id:guid}/items", CreateTodoItem)
            .WithApiVersionSet(TodoRestConfiguration.GetTodoModuleVersionSet(app))
            .MapToApiVersion(1)
            .RequireAuthorization();
    }
    
    private async Task<IResult> CreateTodoItem([FromServices] IMediator mediator, [FromRoute]Guid id, [FromBody]CreateTodoItemCommand command)
    {
        if (id != command.TodoListId)
        {
            return Results.BadRequest();
        }
        
        var resp = await mediator.Send(command);
        return Results.Ok(resp);
    }
}
