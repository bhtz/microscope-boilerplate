using Microscope.Boilerplate.Framework.EventSourcing;

namespace Microscope.Boilerplate.Todo.Domain.TodoListAggregate.Events;

public class OnTodoListCompletedEvent : DomainEvent
{
    public TodoList TodoList { get; set; }
    
    public OnTodoListCompletedEvent(TodoList todoList)
    {
        TodoList = todoList;
        CreatedAt = DateTime.Now;
    }
}
