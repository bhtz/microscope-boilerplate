using MediatR;
using Microscope.Boilerplate.Services.TodoApp.Application.Features.TodoLists.Commands.CreateTodoList;

namespace Microscope.Boilerplate.Services.TodoApp.Api.GraphQL.Mutations;

public partial class Mutation
{
    public async Task<Guid> CreateTodoList([Service]IMediator mediator, CreateTodoListCommand command)
    {
        return await mediator.Send(command);
    }
}
