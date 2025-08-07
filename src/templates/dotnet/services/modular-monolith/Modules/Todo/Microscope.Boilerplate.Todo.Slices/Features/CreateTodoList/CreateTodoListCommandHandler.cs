using FluentValidation;
using MediatR;
using Microscope.Boilerplate.Todo.Domain.TodoListAggregate;
using Microscope.Boilerplate.Todo.Domain.TodoListAggregate.Exceptions;
using Microscope.Boilerplate.Todo.Domain.TodoListAggregate.Repositories;
using Microscope.Framework.Application.Services;
using Microscope.Framework.Domain.CQRS;

namespace Microscope.Boilerplate.Todo.Slices.Features.CreateTodoList;

public record CreateTodoListCommand(string Name) : ICommand<Guid>;

public class CreateTodoListCommandValidator : AbstractValidator<CreateTodoListCommand>
{
    public CreateTodoListCommandValidator()
    {
        RuleFor(v => v.Name).NotEmpty();
    }
}

public class CreateTodoListCommandHandler(
    ITodoListRepository todoListRepository,
    IIdentityService identityService) : IRequestHandler<CreateTodoListCommand, Guid>
{
    public async Task<Guid> Handle(CreateTodoListCommand request, CancellationToken cancellationToken)
    {
        // Todo : remove fake id
        var userId = Guid.NewGuid(); // identityService.GetUserId();
        var userMail = "heintz.benjamin@gmail.com"; // identityService.GetUserMail();
        var tenantId =  Guid.NewGuid().ToString(); //identityService.GetTenantId();

        var todoList = TodoList.Create(tenantId, Guid.NewGuid(), userId, userMail, request.Name);
        
        if (todoList == null)
            throw new TodoListNotFoundDomainException("Todolist not found");
        
        // Todo : need refactoring
        await todoListRepository.AddAsync(todoList);
        await todoListRepository.SaveAndPublishAsync(todoList, cancellationToken);

        return todoList.Id;
    }
}