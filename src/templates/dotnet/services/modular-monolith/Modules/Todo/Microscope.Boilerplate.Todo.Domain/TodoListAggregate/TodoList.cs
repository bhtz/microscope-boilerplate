using Microscope.Boilerplate.Framework.EventSourcing;
using Microscope.Boilerplate.Framework.Exceptions;
using Microscope.Boilerplate.Todo.Domain.TodoListAggregate.Entities;
using Microscope.Boilerplate.Todo.Domain.TodoListAggregate.Events;
using Microscope.Boilerplate.Todo.Domain.TodoListAggregate.Exceptions;
using Microscope.Boilerplate.Todo.Domain.TodoListAggregate.ValueObjects;

namespace Microscope.Boilerplate.Todo.Domain.TodoListAggregate;

public class TodoList : AggregateRoot, IAuditableEntity
{
    public string Name { get; private set; } = string.Empty;

    public bool IsCompleted
    {
        get { return _todoItems.Count() is not 0 && _todoItems.All(x => x.IsCompleted); }
    }

    private readonly List<TodoItem> _todoItems;
    public IReadOnlyCollection<TodoItem> TodoItems => _todoItems;
    
    private readonly List<Tag> _tags;
    public IReadOnlyCollection<Tag> Tags => _tags;

    protected TodoList()
    {
        _todoItems = new List<TodoItem>();
        _tags = new List<Tag>();
    }

    protected TodoList(string tenantId, Guid id, Guid userId, string creatorMail, string name): this()
    {
        Id = id;
        TenantId = tenantId;
        CreatedBy = userId;
        CreatorMail = creatorMail;
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
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
        UpdatedAt = DateTime.Now;
    }

    public Guid AddTodoItem(string label)
    {
        var id = Guid.NewGuid();
        var item = TodoItem.Create(id, label, this);
        _todoItems.Add(item);
        UpdatedAt = DateTime.Now;
        
        return id;
    }
    
    public void RemoveTodoItem(Guid id)
    {
        var item = _todoItems.SingleOrDefault(x => x.Id == id);
        
        if (item is null)
        {
            throw new TodoListDomainException("Todo list item not found");
        }
        
        UpdatedAt = DateTime.Now;
        
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
        UpdatedAt = DateTime.Now;
    }

    public void RemoveTag(Tag tag)
    {       
        _tags.Remove(tag);
        UpdatedAt = DateTime.Now;
    }
     
    public void ToggleItem(Guid id)
    {
        var item = _todoItems.SingleOrDefault(x => x.Id == id);
        
        if (item is null)
        {
            throw new TodoListDomainException($"todo list item {id.ToString()} not found");
        }

        if (item.IsCompleted)
            item.UnComplete();
        else
            item.Complete();
        
        if (IsCompleted)
        {
            this.AddDomainEvent(new OnTodoListCompletedEvent(this));
        }
        
        UpdatedAt = DateTime.Now;
    }

    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public string? CreatorMail { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid UpdatedBy { get; set; }
}
