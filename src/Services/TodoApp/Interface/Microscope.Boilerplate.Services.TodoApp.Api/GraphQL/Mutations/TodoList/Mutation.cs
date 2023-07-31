using MediatR;
using Microscope.Boilerplate.Services.TodoApp.Application.Features.TodoLists.Commands.CreateTodoList;

namespace Microscope.Boilerplate.Services.TodoApp.Api.GraphQL.Mutations;

public partial class Mutation
{
    public async Task<Guid> CreateTodoList([Service]IMediator mediator, CreateTodoListCommand command)
    {
        return await mediator.Send(command);
    }
    
    public async Task<Guid> AddTodoItem([Service]IMediator mediator, CreateTodoItemCommand command)
    {
        return await mediator.Send(command);
    }
    
    public async Task<bool> ToggleTodoItem([Service]IMediator mediator, ToggleTodoItemCommand command)
    {
        return await mediator.Send(command);
    }
    
    public async Task<bool> DeleteTodoItem([Service]IMediator mediator, DeleteTodoItemCommand command)
    {
        return await mediator.Send(command);
    }
    
    public async Task<bool> DeleteTodoList([Service]IMediator mediator, DeleteTodoListCommand command)
    {
        return await mediator.Send(command);
    }
}
