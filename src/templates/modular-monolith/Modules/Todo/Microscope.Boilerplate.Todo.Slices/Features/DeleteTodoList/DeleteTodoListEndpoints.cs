using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Microscope.Boilerplate.Todo.Slices.Features.DeleteTodoList;

public class DeleteTodoListEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/v{apiVersion:apiVersion}/todo/todo-lists/{id:guid}", DeleteTodoList)
            .WithApiVersionSet(TodoRestConfiguration.GetTodoModuleVersionSet(app))
            .MapToApiVersion(1)
            .RequireAuthorization();

    }
    
    private async Task<IResult> DeleteTodoList([FromServices] IMediator mediator, [FromRoute]Guid id, [FromBody]DeleteTodoListCommand command)
    {
        if (id != command.TodoListId)
        {
            return Results.BadRequest();
        }
        
        var resp = await mediator.Send(command);
        return Results.Ok(resp);
    }
}
