using Microscope.Boilerplate.Framework.Domain.DDD;

namespace Microscope.Boilerplate.Todo.Domain.TodoListAggregate.Events;

public sealed record OnTodoListCreatedEvent : DomainEvent
{
    public TodoList TodoList { get; set; }
    
    public OnTodoListCreatedEvent(TodoList todoList)
    {
        TodoList = todoList;
        CreatedAt = DateTimeOffset.UtcNow;
    }
}
