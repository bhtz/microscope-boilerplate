using FluentValidation;
using Microscope.Boilerplate.Framework.Domain.CQRS;

namespace Microscope.Boilerplate.Todo.Slices.Features.ToggleTodoItem;

public record ToggleTodoItemCommand(Guid TodoListId, Guid TodoItemId) : ICommand<bool>;

public class ToggleTodoItemCommandValidator : AbstractValidator<ToggleTodoItemCommand>
{
    public ToggleTodoItemCommandValidator()
    {
        RuleFor(v => v.TodoListId).NotEmpty();
        RuleFor(v => v.TodoItemId).NotEmpty();
    }
}
