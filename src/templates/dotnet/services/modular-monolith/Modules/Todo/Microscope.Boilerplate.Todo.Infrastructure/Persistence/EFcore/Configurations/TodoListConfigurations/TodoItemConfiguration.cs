using Microscope.Boilerplate.Todo.Domain.TodoListAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microscope.Boilerplate.Todo.Infrastructure.Persistence.EFcore.Configurations.TodoListConfigurations;

public class TodoItemConfiguration : IEntityTypeConfiguration<TodoItem>
{
    public void Configure(EntityTypeBuilder<TodoItem> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedNever();
        
        builder.Property(e => e.Label).IsRequired();
        builder.Property(e => e.IsCompleted).IsRequired();
    }
}