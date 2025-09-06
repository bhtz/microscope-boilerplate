using Microscope.Boilerplate.Framework.Domain.Exceptions;

namespace Microscope.Boilerplate.Todo.Domain.TodoListAggregate.Exceptions;

public class TodoListDomainException : DomainException
{
    public TodoListDomainException()
    { }

    public TodoListDomainException(string message): base(message)
    { }

    public TodoListDomainException(string message, Exception innerException): base(message, innerException)
    { }
}