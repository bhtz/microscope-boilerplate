using Microscope.Boilerplate.Framework.Domain.Exceptions;

namespace Microscope.Boilerplate.Todo.Domain.TodoListAggregate.Exceptions;

public class TodoListNotFoundDomainException : NotFoundException
{
    public TodoListNotFoundDomainException()
    { }

    public TodoListNotFoundDomainException(string message): base(message)
    { }

    public TodoListNotFoundDomainException(string message, Exception innerException): base(message, innerException)
    { }
}