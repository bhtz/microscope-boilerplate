using Microscope.SharedKernel;

namespace Microscope.Boilerplate.Services.TodoApp.Domain.Aggregates.TodoListAggregate.Events;

public class OnTodoListCompletedEvent : DomainEvent
{
    public TodoList TodoList { get; set; }
    
    public OnTodoListCompletedEvent(TodoList todoList)
    {
        TodoList = todoList;
        CreatedAt = DateTime.Now;
    }
}
