using System.Reflection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Microscope.Boilerplate.Services.TodoApp.Infrastructure.Persistence;

public class TodoAppDbContext : DbContext
{
    private readonly IMediator _mediator;
    private readonly ILogger<TodoAppDbContext> _logger;

    #region DbSets

    public virtual DbSet<TodoApp.Domain.Aggregates.TodoListAggregate.TodoList> TodoLists { get; set; }

    #endregion

    public TodoAppDbContext(DbContextOptions<TodoAppDbContext> options, IMediator mediator, ILogger<TodoAppDbContext> logger) : base(options)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger;
    }

    public void Migrate()
    {
        this.Database.Migrate();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("boilerplate_todoapp");
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}