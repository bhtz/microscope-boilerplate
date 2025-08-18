using FluentValidation;
using Microscope.Boilerplate.Framework.Domain.CQRS;

namespace Microscope.Boilerplate.Todo.Slices.Features.DeleteTodoItem;

public record DeleteTodoItemCommand(Guid TodoListId, Guid TodoItemId) : ICommand<bool>;

public class DeleteTodoItemCommandValidator : AbstractValidator<DeleteTodoItemCommand>
{
    public DeleteTodoItemCommandValidator()
    {
        RuleFor(v => v.TodoListId).NotEmpty();
        RuleFor(v => v.TodoItemId).NotEmpty();
    }
}
