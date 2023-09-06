using Microscope.Boilerplate.Services.TodoApp.Domain.Aggregates.TodoListAggregate.Entities;
using Microscope.Boilerplate.Services.TodoApp.Domain.Aggregates.TodoListAggregate.Events;
using Microscope.Boilerplate.Services.TodoApp.Domain.Aggregates.TodoListAggregate.Exceptions;
using Microscope.Boilerplate.Services.TodoApp.Domain.Aggregates.TodoListAggregate.ValueObjects;
using Microscope.SharedKernel;

namespace Microscope.Boilerplate.Services.TodoApp.Domain.Aggregates.TodoListAggregate;

public class TodoList : AuditableEntity<Guid>, IAggregateRoot
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }

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
}
