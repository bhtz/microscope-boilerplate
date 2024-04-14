using Microscope.Boilerplate.Services.TodoApp.Domain.Aggregates.TodoListAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microscope.Boilerplate.Services.TodoApp.Infrastructure.Persistence.Configurations.TodoListConfigurations;

public class TodoItemConfiguration : IEntityTypeConfiguration<TodoItem>
{
    public void Configure(EntityTypeBuilder<TodoItem> builder)
    {
        builder.Ignore(b => b.DomainEvents);
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedNever();
        
        builder.Property(e => e.Label).IsRequired();
        builder.Property(e => e.IsCompleted).IsRequired();
    }
}