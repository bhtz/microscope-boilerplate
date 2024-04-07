using FluentValidation;
using Microscope.SharedKernel;

namespace Microscope.Boilerplate.Services.TodoApp.Application.Features.TodoLists.Commands.CreateTodoList;

public class CreateTodoListCommand : ICommand<Guid>
{
    public string Name { get; set; }
    
    public CreateTodoListCommand(string name)
    {
        Name = name;
    }
}

public class CreateTodoListCommandValidator : AbstractValidator<CreateTodoListCommand>
{
    public CreateTodoListCommandValidator()
    {
        RuleFor(v => v.Name).NotEmpty();
    }
}
