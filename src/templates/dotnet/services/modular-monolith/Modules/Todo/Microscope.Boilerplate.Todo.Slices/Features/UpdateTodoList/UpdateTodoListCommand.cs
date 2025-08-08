using FluentValidation;
using Microscope.Framework.Domain.CQRS;

namespace Microscope.Boilerplate.Todo.Slices.Features.UpdateTodoList;

public record UpdateTodoListCommand(string Name, Guid TodoListId) : ICommand<bool>
{

}

public class UpdateTodoListCommandValidator : AbstractValidator<UpdateTodoListCommand>
{
    public UpdateTodoListCommandValidator()
    {
        RuleFor(v => v.Name).NotEmpty();
        RuleFor(v => v.TodoListId).NotEmpty();
    }
}
