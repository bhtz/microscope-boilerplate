using Microscope.Boilerplate.Todo.Domain.TodoListAggregate.Entities;
using Microscope.Boilerplate.Framework.Domain.DDD;

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
