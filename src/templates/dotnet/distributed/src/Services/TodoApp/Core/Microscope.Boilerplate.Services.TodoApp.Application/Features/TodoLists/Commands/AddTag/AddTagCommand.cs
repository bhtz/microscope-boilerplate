using FluentValidation;
using Microscope.SharedKernel;

namespace Microscope.Boilerplate.Services.TodoApp.Application.Features.TodoLists.Commands.AddTag;

public class AddTagCommand : ICommand<bool>
{
    public Guid TodoListId { get; set; }
    public string Label { get; set; }
    public string Color { get; set; }
    
    public AddTagCommand(string label, Guid todoListId, string color)
    {
        TodoListId = todoListId;
        Label = label;
        Color = color;
    }
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
