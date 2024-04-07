using Microscope.Boilerplate.Services.TodoApp.Domain.Aggregates.TodoListAggregate.Entities;
using Microscope.SharedKernel;

namespace Microscope.Boilerplate.Services.TodoApp.Domain.Aggregates.TodoListAggregate.Events;

public class OnTodoItemCreatedEvent : DomainEvent
{
    public TodoItem TodoItem { get; set; }
    
    public OnTodoItemCreatedEvent(TodoItem todoItem)
    {
        TodoItem = todoItem;
        CreatedAt = DateTime.Now;
    }
}
