using Microscope.Boilerplate.Todo.Domain.TodoListAggregate;
using Microscope.Boilerplate.Todo.Domain.TodoListAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microscope.Boilerplate.Todo.Infrastructure.Persistence.EFcore.Configurations.TodoListConfigurations;

public class TodoListConfiguration : IEntityTypeConfiguration<TodoList>
{
    public void Configure(EntityTypeBuilder<TodoList> builder)
    {
        builder.Ignore(b => b.DomainEvents);
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedNever();
        
        builder.Property(e => e.CreatedBy).IsRequired();
        builder.Property(e => e.CreatedAt).IsRequired();
        builder.Property(e => e.UpdatedAt).IsRequired();
        
        builder.HasMany(b => b.TodoItems)
            .WithOne(x => x.TodoList)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Metadata
            .FindNavigation(nameof(TodoItem))
            ?.SetPropertyAccessMode(PropertyAccessMode.Field);
        
        builder.OwnsMany(a => a.Tags, map =>
        {
            map.HasKey("Id");
            map.Property<Guid>("Id");

            map.WithOwner().HasForeignKey("TodoListId");
            map.UsePropertyAccessMode(PropertyAccessMode.Field);

            map.Property(e => e.Label).IsRequired();
            // map.Property(e => e.Color).IsRequired();
        });
    }
}