using FluentValidation;
using Microscope.Boilerplate.Framework.Domain.CQRS;

namespace Microscope.Boilerplate.Todo.Slices.Features.DeleteTodoList;

public record DeleteTodoListCommand(Guid TodoListId) : ICommand<bool>;

public class DeleteTodoListCommandValidator : AbstractValidator<DeleteTodoListCommand>
{
    public DeleteTodoListCommandValidator()
    {
        RuleFor(v => v.TodoListId).NotEmpty();
    }
}
