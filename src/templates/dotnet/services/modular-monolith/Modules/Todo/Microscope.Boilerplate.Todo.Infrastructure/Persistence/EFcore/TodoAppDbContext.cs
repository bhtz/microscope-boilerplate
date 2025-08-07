using System.Reflection;
using MediatR;
using Microscope.Boilerplate.Todo.Domain.TodoListAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Microscope.Boilerplate.Todo.Infrastructure.Persistence.EFcore;

public class TodoAppDbContext : DbContext
{
    private readonly IMediator _mediator;
    private readonly ILogger<TodoAppDbContext> _logger;
    private readonly IOptions<PersistenceOptions> _persistenceOptions;

    #region DbSets

    public virtual DbSet<TodoList> TodoLists { get; set; }

    #endregion

    public TodoAppDbContext(DbContextOptions<TodoAppDbContext> options, IMediator mediator, ILogger<TodoAppDbContext> logger, IOptions<PersistenceOptions> persistenceOptions) : base(options)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger;
        _persistenceOptions = persistenceOptions;
    }

    public TodoAppDbContext()
    {
        
    }

    public async Task Migrate()
    {
        var strategy = this.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(() => this.Database.MigrateAsync());
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        var schema = _persistenceOptions.Value.Schema;
        builder.HasDefaultSchema(schema);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}