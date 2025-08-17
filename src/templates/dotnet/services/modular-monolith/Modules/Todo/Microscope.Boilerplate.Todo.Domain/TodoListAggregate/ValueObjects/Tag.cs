using Microscope.Boilerplate.Framework.Domain.DDD;

namespace Microscope.Boilerplate.Todo.Domain.TodoListAggregate.ValueObjects;

public record Tag : ValueObject
{
    public string Label { get; private set; } = default!;
    public string Color { get; private set; } = default!;

    protected Tag()
    {
        
    }

    public Tag(string label, string color)
    {
        Label = label;
        Color = color;
    }
}