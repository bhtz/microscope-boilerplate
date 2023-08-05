using FluentValidation;
using Microscope.SharedKernel;

namespace Microscope.Boilerplate.Services.TodoApp.Application.Features.TodoLists.Commands.CreateTodoList;

public class UpdateTodoListCommand : ICommand<bool>
{
    public Guid TodoListId { get; set; }
    public string Name { get; set; }
    
    public UpdateTodoListCommand(string name, Guid todoListId)
    {
        Name = name;
        TodoListId = todoListId;
    }
}

public class UpdateTodoListCommandValidator : AbstractValidator<UpdateTodoListCommand>
{
    public UpdateTodoListCommandValidator()
    {
        RuleFor(v => v.Name).NotEmpty();
        RuleFor(v => v.TodoListId).NotEmpty();
    }
}
