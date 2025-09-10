using Microscope.Boilerplate.Todo.Domain.TodoListAggregate;

namespace Microscope.Boilerplate.Todo.Domain.Tests.Unit.Mock;

public static class Given
{
    public static TodoList BasicTodoList()
    {
        var tl = TodoList.Create("test-tenant",Guid.NewGuid(), Guid.NewGuid(), "heintz.benjamin@gmail.com", "todolist test");
        tl.AddTodoItem("Cook the lunch");
        tl.AddTodoItem("Clean the kitchen");
        
        return tl;
    }
    
    public static TodoList EmptyTodoList()
    {
        var tl = TodoList.Create("test-tenant",Guid.NewGuid(), Guid.NewGuid(), "heintz.benjamin@gmail.com", "todolist test");
        
        return tl;
    }
}