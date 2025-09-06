using Microscope.Boilerplate.Framework.Domain.DDD;

namespace Microscope.Boilerplate.Todo.Domain.TodoListAggregate.Entities;

public class TodoItem : Entity
{
    public string Label { get; private set; } = string.Empty;
    public bool IsCompleted { get; private set; }
    
    protected TodoItem()
    {
        
    }

    private TodoItem(Guid id, string label) : this()
    {
        Id = id;
        Label = label;
        IsCompleted = false;
    }

    public static TodoItem Create(Guid id, string label)
    {
        return new TodoItem(id, label);
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