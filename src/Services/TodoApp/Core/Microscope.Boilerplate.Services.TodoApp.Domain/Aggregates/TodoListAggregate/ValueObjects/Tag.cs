using Microscope.SharedKernel;

namespace Microscope.Boilerplate.Services.TodoApp.Domain.Aggregates.TodoListAggregate.ValueObjects;

public class Tag : ValueObject
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
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Label;
        yield return Color;
    }
}