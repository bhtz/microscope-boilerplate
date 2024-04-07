using Microscope.SharedKernel;

namespace Microscope.Boilerplate.Services.TodoApp.Domain.Aggregates.TodoListAggregate.Exceptions;

public class TodoListDomainException : DomainException
{
    public TodoListDomainException()
    { }

    public TodoListDomainException(string message): base(message)
    { }

    public TodoListDomainException(string message, Exception innerException): base(message, innerException)
    { }
}