using Microscope.SharedKernel;

namespace Microscope.Boilerplate.Services.TodoApp.Domain.Aggregates.TodoListAggregate.Events;

public class OnTodoListCreatedEvent : DomainEvent
{
    public TodoList TodoList { get; set; }
    
    public OnTodoListCreatedEvent(TodoList todoList)
    {
        TodoList = todoList;
        CreatedAt = DateTime.Now;
    }
}
