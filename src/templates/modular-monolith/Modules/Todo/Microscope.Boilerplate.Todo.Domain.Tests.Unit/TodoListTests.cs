using Microscope.Boilerplate.Todo.Domain.Tests.Unit.Mock;
using Microscope.Boilerplate.Todo.Domain.TodoListAggregate.ValueObjects;

namespace Microscope.Boilerplate.Todo.Domain.Tests.Unit;

public class TodoListTests
{
    [Fact]
    public void Given_TodoList_When_Created_Then_IsCompleted_Is_False()
    {
        // GIVEN
        var tl = new TodoListBuilder()
            .WithSampleTodoItem()
            .Build();
        
        // THEN
        Assert.False(tl.IsCompleted);
    }
    
    [Fact]
    public void Given_TodoList_When_AddTwoTodoItem_Then_TodoItems_Count_Is_4()
    {
        // GIVEN
        var tl = new TodoListBuilder()
            .WithSampleTodoItem()
            .Build();
        
        // WHEN
        tl.AddTodoItem("Cook the solution architecture");
        tl.AddTodoItem("Clean the code");
        
        // THEN
        Assert.Equal(4, tl.TodoItems.Count);
    }
    
    [Fact]
    public void Given_EmptyTodoList_When_Created_Then_IsCompleted_Is_False()
    {
        // GIVEN
        var tl = new TodoListBuilder()
            .Build();
        
        // THEN
        Assert.False(tl.IsCompleted);
    }
    
    [Fact]
    public void Given_EmptyTodoList_When_AddTodoItem_Then_IsCompleted_Is_True()
    {
        // GIVEN
        var tl = new TodoListBuilder().Build();
        var id = tl.AddTodoItem("Cook the solution architecture");
        
        // WHEN
        tl.ToggleItem(id);
        
        // THEN
        Assert.True(tl.IsCompleted);
    }
    
    [Fact]
    public void Given_EmptyTodoList_When_AddTag_Then_Tags_Is_Incremented_By_One()
    {
        // GIVEN
        var tl = new TodoListBuilder()
            .WithSampleTags()
            .Build();
        
        // THEN
        Assert.Equal(2, tl.Tags.Count);
        
        // AND WHEN
        tl.AddTag(new Tag("test", "red"));
        
        // THEN
        Assert.Equal(3, tl.Tags.Count);
    }
    
    [Fact]
    public void Given_EmptyTodoList_When_AddAndRemoveTag_Then_Tags_Is_Empty()
    {
        // GIVEN
        var tl = new TodoListBuilder().Build();
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
        var tl = new TodoListBuilder()
            .WithSampleTodoItem()
            .Build();
        
        // WHEN
        var text = "Cool text";
        tl.Update(text);
        
        // THEN
        Assert.Equal(text, tl.Name);
    }
}