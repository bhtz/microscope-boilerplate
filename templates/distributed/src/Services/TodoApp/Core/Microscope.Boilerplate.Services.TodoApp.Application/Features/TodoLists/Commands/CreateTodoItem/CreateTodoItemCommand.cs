using FluentValidation;
using Microscope.SharedKernel;

namespace Microscope.Boilerplate.Services.TodoApp.Application.Features.TodoLists.Commands.CreateTodoItem;

public class CreateTodoItemCommand : ICommand<Guid>
{
    public Guid TodoListId { get; set; }
    public string Label { get; set; }
    
    public CreateTodoItemCommand(string label, Guid todoListId)
    {
        Label = label;
        TodoListId = todoListId;
    }
}

public class CreateTodoItemCommandValidator : AbstractValidator<CreateTodoItemCommand>
{
    public CreateTodoItemCommandValidator()
    {
        RuleFor(v => v.TodoListId).NotEmpty();
        RuleFor(v => v.Label).NotEmpty();
    }
}
