using Microscope.Boilerplate.Framework.Domain.DDD;

namespace Microscope.Boilerplate.Todo.Domain.TodoListAggregate.Events;

public sealed record OnTodoListCompletedEvent : DomainEvent
{
    public TodoList TodoList { get; set; }
    
    public OnTodoListCompletedEvent(TodoList todoList)
    {
        TodoList = todoList;
        CreatedAt = DateTimeOffset.UtcNow;
    }
}
