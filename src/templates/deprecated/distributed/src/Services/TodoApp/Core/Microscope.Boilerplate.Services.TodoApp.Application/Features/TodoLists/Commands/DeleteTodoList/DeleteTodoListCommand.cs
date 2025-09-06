using FluentValidation;
using Microscope.SharedKernel;

namespace Microscope.Boilerplate.Services.TodoApp.Application.Features.TodoLists.Commands.DeleteTodoList;

public class DeleteTodoListCommand : ICommand<bool>
{
    public Guid TodoListId { get; set; }

    public DeleteTodoListCommand(Guid todoListId)
    {
        TodoListId = todoListId;
    }
}

public class DeleteTodoListCommandValidator : AbstractValidator<DeleteTodoListCommand>
{
    public DeleteTodoListCommandValidator()
    {
        RuleFor(v => v.TodoListId).NotEmpty();
    }
}
