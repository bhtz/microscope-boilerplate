using Microscope.Boilerplate.Todo.Domain.TodoListAggregate.Entities;
using Microscope.Boilerplate.Todo.Domain.TodoListAggregate.Events;
using Microscope.Boilerplate.Todo.Domain.TodoListAggregate.Exceptions;
using Microscope.Boilerplate.Todo.Domain.TodoListAggregate.ValueObjects;
using Microscope.Boilerplate.Framework.Domain.DDD;
using Microscope.Boilerplate.Framework.Domain.Exceptions;

namespace Microscope.Boilerplate.Todo.Domain.TodoListAggregate;

public class TodoList : AuditableAggregateRoot
{
    public string Name { get; private set; } = string.Empty;

    public bool IsCompleted
    {
        get { return _todoItems.Count is not 0 && _todoItems.All(x => x.IsCompleted); }
    }

    private readonly List<TodoItem> _todoItems;
    public IReadOnlyCollection<TodoItem> TodoItems => _todoItems;
    
    private readonly List<Tag> _tags;
    public IReadOnlyCollection<Tag> Tags => _tags;

    protected TodoList()
    {
        _todoItems = [];
        _tags = [];
    }

    private TodoList(string tenantId, Guid id, Guid userId, string creatorMail, string name) : this()
    {
        Id = id;
        TenantId = tenantId;
        CreatedBy = userId;
        CreatorMail = creatorMail;
        CreatedAt = DateTimeOffset.UtcNow;
        UpdatedAt = DateTimeOffset.UtcNow;
        Name = name;
    }

    public static TodoList Create(string tenantId, Guid id, Guid userId, string creatorMail, string name)
    {
        return new TodoList(tenantId, id, userId, creatorMail, name);
    }

    public void Update(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new TodoListDomainException("todolist name cannot by null or empty");
        }

        Name = name;
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    public Guid AddTodoItem(string label)
    {
        var id = Guid.NewGuid();
        var item = TodoItem.Create(id, label);
        _todoItems.Add(item);
        UpdatedAt = DateTimeOffset.UtcNow;
        
        return id;
    }
    
    public void RemoveTodoItem(Guid id)
    {
        var item = _todoItems.SingleOrDefault(x => x.Id == id) 
            ?? throw new TodoListDomainException("Todo list item not found");

        UpdatedAt = DateTimeOffset.UtcNow;

        _todoItems.Remove(item);

        if (IsCompleted)
        {
            this.AddDomainEvent(new OnTodoListCompletedEvent(this));
        }
    }

    public void AddTag(Tag tag)
    {
        if (_tags.Any(x => x.Equals(tag)))
            throw new DomainException("Duplicate label for todolist tags");
        
        _tags.Add(tag);
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    public void RemoveTag(Tag tag)
    {       
        _tags.Remove(tag);
        UpdatedAt = DateTimeOffset.UtcNow;
    }
     
    public void ToggleItem(Guid id)
    {
        var item = _todoItems.SingleOrDefault(x => x.Id == id)
            ?? throw new TodoListDomainException($"todo list item {id.ToString()} not found");

        if (item.IsCompleted)
        {
            item.UnComplete();
        }
        else
        {
            item.Complete();
        }

        if (IsCompleted)
        {
            this.AddDomainEvent(new OnTodoListCompletedEvent(this));
        }
        
        UpdatedAt = DateTimeOffset.UtcNow;
    }
}
