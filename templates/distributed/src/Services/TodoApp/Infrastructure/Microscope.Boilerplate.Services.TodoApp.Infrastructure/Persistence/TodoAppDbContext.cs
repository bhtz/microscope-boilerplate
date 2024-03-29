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

    public virtual DbSet<Domain.Aggregates.TodoListAggregate.TodoList> TodoLists { get; set; }

    #endregion

    public TodoAppDbContext(DbContextOptions<TodoAppDbContext> options, IMediator mediator, ILogger<TodoAppDbContext> logger) : base(options)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger;
    }

    public async Task Migrate()
    {
        var strategy = this.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(() => this.Database.MigrateAsync());
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("boilerplate_todoapp");
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}