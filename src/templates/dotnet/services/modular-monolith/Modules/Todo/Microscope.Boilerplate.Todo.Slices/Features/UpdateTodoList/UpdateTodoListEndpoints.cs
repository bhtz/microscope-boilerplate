using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Microscope.Boilerplate.Todo.Slices.Features.UpdateTodoList;

public class UpdateTodoListEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/v{apiVersion:apiVersion}/todo/todo-lists/{id:guid}", UpdateTodoList)
            .WithApiVersionSet(Extensions.GetModuleVersionSet(app))
            .MapToApiVersion(1)
            .AllowAnonymous(); // Todo: to remove
    }
    
    private async Task<IResult> UpdateTodoList([FromServices] IMediator mediator, [FromRoute]Guid id, [FromBody]UpdateTodoListCommand command)
    {
        if (id != command.TodoListId)
        {
            return Results.BadRequest();
        }
        
        var resp = await mediator.Send(command);
        return Results.Ok(resp);
    }
}
