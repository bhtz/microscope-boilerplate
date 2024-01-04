using System.Diagnostics;
using MediatR;
using Microscope.Boilerplate.Services.TodoApp.Application.Features.TodoLists.Commands.AddTag;
using Microscope.Boilerplate.Services.TodoApp.Application.Features.TodoLists.Commands.CreateTodoItem;
using Microscope.Boilerplate.Services.TodoApp.Application.Features.TodoLists.Commands.CreateTodoList;
using Microscope.Boilerplate.Services.TodoApp.Application.Features.TodoLists.Commands.DeleteTodoItem;
using Microscope.Boilerplate.Services.TodoApp.Application.Features.TodoLists.Commands.DeleteTodoList;
using Microscope.Boilerplate.Services.TodoApp.Application.Features.TodoLists.Commands.RemoveTag;
using Microscope.Boilerplate.Services.TodoApp.Application.Features.TodoLists.Commands.ToggleTodoItem;
using Microscope.Boilerplate.Services.TodoApp.Application.Features.TodoLists.Commands.UpdateTodoList;

namespace Microscope.Boilerplate.Services.TodoApp.Api.GraphQL.Mutations;

public partial class Mutation
{
    public async Task<Guid> CreateTodoList([Service]IMediator mediator, CreateTodoListCommand command)
    {
        // ActivitySource source = new ActivitySource("TodoApp.Api.REST");
        // using var activity = source.StartActivity("CreateTodoList", ActivityKind.Server);
        // activity.SetTag("test", "test");
        
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
    
    public async Task<bool> UpdateTodoList([Service]IMediator mediator, UpdateTodoListCommand command)
    {
        return await mediator.Send(command);
    }
    
    public async Task<bool> AddTag([Service]IMediator mediator, AddTagCommand command)
    {
        return await mediator.Send(command);
    }
    
    public async Task<bool> RemoveTag([Service]IMediator mediator, RemoveTagCommand command)
    {
        return await mediator.Send(command);
    }
}
