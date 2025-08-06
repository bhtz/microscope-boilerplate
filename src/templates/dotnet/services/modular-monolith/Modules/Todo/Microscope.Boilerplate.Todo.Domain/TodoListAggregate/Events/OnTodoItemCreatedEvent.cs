using Microscope.Boilerplate.Framework.EventSourcing;
using Microscope.Boilerplate.Todo.Domain.TodoListAggregate.Entities;

namespace Microscope.Boilerplate.Todo.Domain.TodoListAggregate.Events;

public class OnTodoItemCreatedEvent : DomainEvent
{
    public TodoItem TodoItem { get; set; }
    
    public OnTodoItemCreatedEvent(TodoItem todoItem)
    {
        TodoItem = todoItem;
        CreatedAt = DateTime.Now;
    }
}
