using Microscope.SharedKernel;

namespace Microscope.Boilerplate.Services.TodoApp.Domain.Aggregates.TodoListAggregate.Exceptions;

public class TodoListNotFoundDomainException : NotFoundException
{
    public TodoListNotFoundDomainException()
    { }

    public TodoListNotFoundDomainException(string message): base(message)
    { }

    public TodoListNotFoundDomainException(string message, Exception innerException): base(message, innerException)
    { }
}