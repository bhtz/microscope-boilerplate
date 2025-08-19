using Microscope.Boilerplate.Framework.Domain.DDD;

namespace Microscope.Boilerplate.Todo.Domain.TodoListAggregate.ValueObjects;

public record Tag(string Label, string Color) : ValueObject;