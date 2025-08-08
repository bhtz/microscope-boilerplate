using FluentValidation;
using Microscope.Boilerplate.Todo.Slices.Features.AddTag;
using Microscope.Framework.Domain.CQRS;

namespace Microscope.Boilerplate.Todo.Slices.Features.RemoveTag;

public record RemoveTagCommand(string Label, Guid TodoListId, string Color) : ICommand<bool>
{
}

public class AddTagCommandValidator : AbstractValidator<AddTagCommand>
{
    public AddTagCommandValidator()
    {
        RuleFor(v => v.TodoListId).NotEmpty();
        RuleFor(v => v.Label).NotEmpty();
        RuleFor(v => v.Color).NotEmpty();
    }
}
