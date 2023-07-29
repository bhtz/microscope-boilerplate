using Microscope.Boilerplate.Services.TodoApp.Domain.Aggregates.TodoListAggregate;

namespace Microscope.Boilerplate.Services.TodoApp.UnitTests.Mock;

public static class TodoListMock
{
    public static TodoList MockTodoList()
    {
        var tl = TodoList.Create("test-tenant",Guid.NewGuid(), Guid.NewGuid(), "todolist test");
        tl.AddTodoItem("Cook the lunch");
        tl.AddTodoItem("Clean the kitchen");
        tl.AddTodoItem("Cook the solution architecture");
        var code = tl.AddTodoItem("Clean the code");
        
        tl.ToggleItem(code);

        return tl;
    }
}
