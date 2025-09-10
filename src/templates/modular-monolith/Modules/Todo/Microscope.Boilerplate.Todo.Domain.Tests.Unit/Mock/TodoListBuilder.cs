using Microscope.Boilerplate.Todo.Domain.TodoListAggregate;
using Microscope.Boilerplate.Todo.Domain.TodoListAggregate.ValueObjects;

namespace Microscope.Boilerplate.Todo.Domain.Tests.Unit.Mock;

public sealed class TodoListBuilder
{
    private readonly TodoList _todoList = TodoList.Create(Guid.NewGuid().ToString(), Guid.NewGuid(), Guid.NewGuid(), "heintz.benjamin@gmail.com","test todo list");
        
    public TodoListBuilder WithSampleTodoItem()
    {
        _todoList.AddTodoItem("Cook the lunch");
        _todoList.AddTodoItem("Clean the kitchen");
        
        return this;
    }
    
    public TodoListBuilder WithSampleTags()
    {
        _todoList.AddTag(new Tag("tag", "red"));
        _todoList.AddTag(new Tag("second tag", "blue"));
        
        return this;
    }

    public TodoList Build() => _todoList;
}
