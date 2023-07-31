using FluentValidation;
using Microscope.SharedKernel;

namespace Microscope.Boilerplate.Services.TodoApp.Application.Features.TodoLists.Commands.CreateTodoList;

public class ToggleTodoItemCommand : ICommand<bool>
{
    public Guid TodoListId { get; set; }
    public Guid TodoItemId { get; set; }

    public ToggleTodoItemCommand(Guid todoListId, Guid todoItemId)
    {
        TodoItemId = todoItemId;
        TodoListId = todoListId;
    }
}

public class ToggleTodoItemCommandValidator : AbstractValidator<ToggleTodoItemCommand>
{
    public ToggleTodoItemCommandValidator()
    {
        RuleFor(v => v.TodoListId).NotEmpty();
        RuleFor(v => v.TodoItemId).NotEmpty();
    }
}
