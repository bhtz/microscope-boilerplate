using Microscope.Boilerplate.Framework.Domain.DDD;

namespace Microscope.Boilerplate.Todo.Domain.TodoListAggregate.Entities;

public class TodoItem : Entity
{
    public string Label { get; private set; } = string.Empty;
    public bool IsCompleted { get; private set; }
    
    public TodoList TodoList { get; private set; }

    protected TodoItem()
    {
        
    }

    protected TodoItem(Guid id, string label, TodoList todoList)
    {
        Id = id;
        Label = label;
        TodoList = todoList;
        IsCompleted = false;
    }

    public static TodoItem Create(Guid id, string label, TodoList todoList)
    {
        return new TodoItem(id, label, todoList);
    }

    public void Complete()
    {
        IsCompleted = true;
    }
    
    public void UnComplete()
    {
        IsCompleted = false;
    }
}