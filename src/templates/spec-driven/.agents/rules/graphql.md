---
apply: always
---

# GraphQL Resolvers Coding Rules

Spec-driven development rules for HotChocolate-based GraphQL resolvers with authorization.

## Architecture Principles

- **Type-Safe Resolvers**: Use HotChocolate's attribute-based approach for clarity
- **Query Organization**: Group related queries by domain (Query types per module)
- **Federation Support**: Design for potential module federation
- **Authorization**: Apply [Authorize] at query/mutation/field level
- **Performance**: Use DataLoader for batch operations to avoid N+1 queries
- **Error Handling**: Return structured errors with proper messages

## Query Type Pattern

Location: `Modules/{ModuleName}/{Namespace}.Slices/Features/{UseCase}/{UseCase}Resolver.cs`

```csharp
using HotChocolate.Authorization;

namespace Microscope.Boilerplate.Todo.Slices.Features.GetTodoLists;

[QueryType]
public static class GetTodoListResolver
{
    [Authorize]
    public static async Task<IEnumerable<GetTodoListQueryResult>> GetTodoLists([Service]IMediator mediator)
    {
        return await mediator.Send(new GetTodoListQuery());
    }
}
```

## Mutation Type Pattern

Location: `Modules/{ModuleName}/{ModuleNamespace}.Slices/Features/{UseCase}/{UseCase}Resolver.cs`

```csharp
using HotChocolate.Authorization;

namespace Microscope.Boilerplate.Todo.Slices.Features.CreateTodoList;

[MutationType]
public static class CreateTodoListResolver
{
    [Authorize]
    public static async Task<Guid> CreateTodoList(IMediator mediator, CreateTodoListCommand command)
    {
        return await mediator.Send(command);
    }
}
```

## Setup in Module Extensions

Location: `Modules/{ModuleName}/{ModuleNamespace}.Slices/ITodoModule.cs`

```csharp
[assembly: Module("TodoTypes")]

namespace Microscope.Boilerplate.Todo.Slices;

public interface ITodoModule;
```

Location: `Modules/{ModuleName}/{ModuleNamespace}.Slices/Extensions.cs`

```csharp
services
    .AddGraphQL()
    .AddTodoTypes() // 
    .AddProjections()
    .AddFiltering()
    .AddSorting();
```


## Best Practices

- **Naming**: Use Dto suffix for output types, Input suffix for input types
- **Null Safety**: Use proper nullable types for optional fields
- **Async**: Always use async/await with CancellationToken
- **Validation**: Validate input types using FluentValidation or custom logic
- **Error Messages**: Provide clear, actionable error messages
- **Documentation**: Use XML comments visible in GraphQL documentation
- **Performance**: Use DataLoader for batch operations
- **Security**: Always validate authorization requirements