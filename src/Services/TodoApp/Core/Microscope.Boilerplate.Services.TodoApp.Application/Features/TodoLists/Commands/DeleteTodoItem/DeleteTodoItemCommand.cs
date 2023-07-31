using FluentValidation;
using Microscope.SharedKernel;

namespace Microscope.Boilerplate.Services.TodoApp.Application.Features.TodoLists.Commands.CreateTodoList;

public class DeleteTodoItemCommand : ICommand<bool>
{
    public Guid TodoListId { get; set; }
    public Guid TodoItemId { get; set; }

    public DeleteTodoItemCommand(Guid todoListId, Guid todoItemId)
    {
        TodoItemId = todoItemId;
        TodoListId = todoListId;
    }
}

public class DeleteTodoItemCommandValidator : AbstractValidator<DeleteTodoItemCommand>
{
    public DeleteTodoItemCommandValidator()
    {
        RuleFor(v => v.TodoListId).NotEmpty();
        RuleFor(v => v.TodoItemId).NotEmpty();
    }
}
