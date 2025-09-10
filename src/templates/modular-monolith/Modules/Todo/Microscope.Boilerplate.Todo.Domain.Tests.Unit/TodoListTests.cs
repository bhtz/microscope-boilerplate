using Microscope.Boilerplate.Todo.Domain.Tests.Unit.Mock;
using Microscope.Boilerplate.Todo.Domain.TodoListAggregate.ValueObjects;

namespace Microscope.Boilerplate.Todo.Domain.Tests.Unit;

public class TodoListTests
{
    [Fact]
    public void Given_TodoList_When_Created_Then_IsCompleted_Is_False()
    {
        // GIVEN
        var tl = Given.BasicTodoList();
        
        // THEN
        Assert.False(tl.IsCompleted);
    }
    
    [Fact]
    public void Given_TodoList_When_AddTwoTodoItem_Then_TodoItems_Count_Is_4()
    {
        // GIVEN
        var tl = Given.BasicTodoList();
        
        // THEN
        tl.AddTodoItem("Cook the solution architecture");
        tl.AddTodoItem("Clean the code");
        
        // THEN
        Assert.Equal(4, tl.TodoItems.Count);
    }
    
    [Fact]
    public void Given_EmptyTodoList_When_Created_Then_IsCompleted_Is_False()
    {
        // GIVEN
        var tl = Given.EmptyTodoList();
        
        // THEN
        Assert.False(tl.IsCompleted);
    }
    
    [Fact]
    public void Given_EmptyTodoList_When_AddTodoItem_Then_IsCompleted_Is_True()
    {
        // GIVEN
        var tl = Given.EmptyTodoList();
        var id = tl.AddTodoItem("Cook the solution architecture");
        tl.ToggleItem(id);
        
        // THEN
        Assert.True(tl.IsCompleted);
    }
    
    [Fact]
    public void Given_EmptyTodoList_When_AddTag_Then_Tags_Is_NotEmpty()
    {
        // GIVEN
        var tl = Given.EmptyTodoList();
        var id = tl.AddTodoItem("Cook the solution architecture");
        
        // WHEN
        tl.AddTag(new Tag("test", "red"));
        
        // THEN
        Assert.Single(tl.Tags);
    }
    
    [Fact]
    public void Given_EmptyTodoList_When_AddAndRemoveTag_Then_Tags_Is_Empty()
    {
        // GIVEN
        var tl = Given.EmptyTodoList();
        var id = tl.AddTodoItem("Cook the solution architecture");
        
        // WHEN
        var tag = new Tag("test", "red");
        tl.AddTag(tag);
        tl.RemoveTag(tag);
        
        // THEN
        Assert.Empty(tl.Tags);
    }
    
    [Fact]
    public void Given_TodoList_When_Update_Then_Name_Is_Updated()
    {
        // GIVEN
        var tl = Given.BasicTodoList();
        
        // WHEN
        var text = "Cool text";
        tl.Update(text);
        
        // THEN
        Assert.Equal(text, tl.Name);
    }
}