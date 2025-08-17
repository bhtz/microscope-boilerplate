using FluentValidation;
using Microscope.Boilerplate.Framework.Domain.CQRS;

namespace Microscope.Boilerplate.Todo.Slices.Features.CreateTodoList;

public record CreateTodoListCommand(string Name) : ICommand<Guid>;

public class CreateTodoListCommandValidator : AbstractValidator<CreateTodoListCommand>
{
    public CreateTodoListCommandValidator()
    {
        RuleFor(v => v.Name).NotEmpty();
    }
}