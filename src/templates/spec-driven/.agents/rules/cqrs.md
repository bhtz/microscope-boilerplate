---
apply: always
---

# CQRS Pattern Coding Rules

Spec-driven development rules for Command Query Responsibility Segregation (CQRS) pattern using MediatR and vertical slices.

## Architecture Principles

- **Separation of Concerns**: Queries (read) and Commands (write) are completely separate
- **MediatR Integration**: Use MediatR for request/response pipeline
- **Validators**: Implement FluentValidation validators alongside requests
- **Vertical Slices**: Each use case is self-contained in its own folder
- **Feature Organization**: Group related queries/commands in `/Features/{FeatureName}/` folders
- **Single Responsibility**: Each handler does one thing well
- **Dependency Injection**: Constructor injection for repositories, services, and identity

## Query Pattern

Queries represent read operations that return data without modifying state.

### Query File

Location: `Modules/{ModuleName}/Microscope.Boilerplate.{ModuleName}.Slices/Features/{FeatureName}/{FeatureName}Query.cs`

```csharp
using Microscope.Boilerplate.Framework.Domain.CQRS;

namespace Microscope.Boilerplate.Todo.Slices.Features.GetTodoLists;

/// Query request
public record GetTodoListQuery() : IQuery<IEnumerable<GetTodoListQueryResult>>;

/// Query result DTO
public record GetTodoListQueryResult(Guid Id, string Name, bool IsCompleted);
```

**Key Points:**
- Use `record` for immutability
- Implement `IQuery<T>` where T is the return type
- Define result DTOs alongside the query
- Queries should have no parameters or minimal parameters
- Use descriptive names: `Get{Resource}Query`, `List{Resources}Query`, `Search{Resource}Query`

### Query Handler

Location: `Modules/{ModuleName}/Microscope.Boilerplate.{ModuleName}.Slices/Features/{FeatureName}/{FeatureName}QueryHandler.cs`

```csharp
using Microscope.Boilerplate.Todo.Domain.TodoListAggregate.Repositories;
using MediatR;

namespace Microscope.Boilerplate.Todo.Slices.Features.GetTodoLists;

public class GetTodoListQueryHandler(
    ITodoListRepository todoListRepository,
    IIdentityService identityService)
    : IRequestHandler<GetTodoListQuery, IEnumerable<GetTodoListQueryResult>>
{
    public async Task<IEnumerable<GetTodoListQueryResult>> Handle(
        GetTodoListQuery request,
        CancellationToken cancellationToken)
    {
        // Get tenant/user context
        var userId = identityService.GetUserId();
        var tenantId = identityService.GetTenantId();

        // Fetch data from repository
        var todoLists = await todoListRepository.GetCreatedByAsync(tenantId, userId);

        // Map to result DTO
        return todoLists
            .Select(x => new GetTodoListQueryResult(x.Id, x.Name, x.IsCompleted))
            .ToList();
    }
}
```

**Key Points:**
- Implement `IRequestHandler<TRequest, TResponse>`
- Use constructor injection for repositories, services
- No modification of state - read-only operations
- Return mapped DTOs, not domain entities
- Use async/await for I/O operations
- Get identity context (userId, tenantId) for multi-tenancy

---

## Command Pattern

Commands represent write operations that modify state and return a result.

### Command File with Validator

Location: `Modules/{ModuleName}/Microscope.Boilerplate.{ModuleName}.Slices/Features/{FeatureName}/{FeatureName}Command.cs`

```csharp
using FluentValidation;
using Microscope.Boilerplate.Framework.Domain.CQRS;

namespace Microscope.Boilerplate.Todo.Slices.Features.CreateTodoList;

/// Command request
public record CreateTodoListCommand(string Name) : ICommand<Guid>;

/// Command validator - registered automatically via assembly scan
public class CreateTodoListCommandValidator : AbstractValidator<CreateTodoListCommand>
{
    public CreateTodoListCommandValidator()
    {
        RuleFor(cmd => cmd.Name)
            .NotEmpty()
            .WithMessage("Todo list name is required")
            .MaximumLength(200)
            .WithMessage("Name cannot exceed 200 characters");
    }
}
```

**Key Points:**
- Use `record` for immutability
- Implement `ICommand<T>` where T is the return type (typically Guid, bool, or result DTO)
- Define `{CommandName}Validator : AbstractValidator<{CommandName}>`
- Validators are auto-registered via assembly scan in module Extensions
- Use meaningful validation messages
- Return the created entity ID (Guid) for creation commands
- Return bool for deletion/toggle commands
- Return DTO for complex commands

### Command Handler

Location: `Modules/{ModuleName}/Microscope.Boilerplate.{ModuleName}.Slices/Features/{FeatureName}/{FeatureName}CommandHandler.cs`

```csharp
using Microscope.Boilerplate.Todo.Domain.TodoListAggregate;
using Microscope.Boilerplate.Todo.Domain.TodoListAggregate.Repositories;
using MediatR;

namespace Microscope.Boilerplate.Todo.Slices.Features.CreateTodoList;

public class CreateTodoListCommandHandler(
    IUnitOfWork unitOfWork,
    ITodoListRepository todoListRepository,
    IIdentityService identityService)
    : IRequestHandler<CreateTodoListCommand, Guid>
{
    public async Task<Guid> Handle(
        CreateTodoListCommand request,
        CancellationToken cancellationToken)
    {
        // Get identity context
        var userId = identityService.GetUserId();
        var userMail = identityService.GetUserMail();
        var tenantId = identityService.GetTenantId();

        // Create domain entity
        var todoList = TodoList.Create(
            tenantId,
            Guid.NewGuid(),
            userId,
            userMail,
            request.Name);

        // Persist changes
        await todoListRepository.AddAsync(todoList);
        await unitOfWork.SaveChangesAndDispatchEventsAsync(cancellationToken);

        return todoList.Id;
    }
}
```

**Key Points:**
- Implement `IRequestHandler<TCommand, TResponse>`
- Use constructor injection for repositories, UnitOfWork, services
- Get identity context (userId, tenantId) for multi-tenancy and audit
- Create domain entities using factory methods
- Use repositories for persistence
- Call `unitOfWork.SaveChangesAndDispatchEventsAsync()` to persist and dispatch domain events
- Return meaningful results (IDs, DTOs, boolean)
- No mapping back to domain entities in responses

---

## Module Integration

### 1. Extensions Setup

File: `Modules/{ModuleName}/{ModuleNamespace}.Slices/Extensions.cs`

```csharp
using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Microscope.Boilerplate.Todo.Slices;

public static class Extensions
{
    public static IServiceCollection AddTodoApplication(this IServiceCollection services)
    {
        // Get current assembly for auto-registration
        var execAssembly = Assembly.GetExecutingAssembly();

        // Auto-register all validators in assembly
        services.AddValidatorsFromAssembly(execAssembly);

        // Register MediatR with all handlers in assembly
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(execAssembly);
            // Optional: add custom behaviors (logging, validation, etc.)
        });

        return services;
    }
}
```

**Key Points:**
- Use `AddValidatorsFromAssembly()` to auto-register all `AbstractValidator<T>` implementations
- Use `AddMediatR()` to auto-register all `IRequestHandler<>` implementations
- Use reflection assembly scan for automatic discovery
- Extension method should follow naming: `Add{ModuleName}Application()`

### 2. Program.cs Integration

File: `Microscope.Boilerplate.API/Program.cs`

```csharp
// ...existing code...

// Register Todo module
services
    .AddTodoApplication()
    .AddTodoInfrastructure();

// ...existing code...
```

## Best Practices

### Do's ✅
- Use separate Query and Command classes
- Keep handlers focused on single responsibility
- Use validators for all commands
- Include identity context (userId, tenantId) in handlers
- Map domain entities to DTOs in responses
- Use domain factory methods for entity creation
- Return meaningful results from commands
- Use async/await for I/O operations

### Don'ts ❌
- Don't modify state in queries
- Don't return domain entities from queries (use DTOs)
- Don't mix query and command logic in one handler
- Don't skip validation on commands
- Don't use empty validators
- Don't access multiple aggregates in one handler
- Don't hardcode identity values instead of using IIdentityService
