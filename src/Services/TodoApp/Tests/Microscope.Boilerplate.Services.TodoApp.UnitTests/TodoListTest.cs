using Microscope.Boilerplate.Services.TodoApp.UnitTests.Mock;

namespace Microscope.Boilerplate.Services.TodoApp.UnitTests;

public class TodoListTest
{
    [Fact]
    public void IsCompleted()
    {
        var todolist = TodoListMock.MockTodoList();
        
        Assert.False(todolist.IsCompleted);
    }
    
    [Fact]
    public void Count4Items()
    {
        var todolist = TodoListMock.MockTodoList();
        var count = todolist.TodoItems.Count();
        
        Assert.Equal(4, count);
    }
    
    [Fact]
    public void CompleteAll()
    {
        var todolist = TodoListMock.MockTodoList();
        todolist.CompleteAll();
        
        Assert.True(todolist.IsCompleted);
        Assert.NotEmpty(todolist.DomainEvents);
    }
}