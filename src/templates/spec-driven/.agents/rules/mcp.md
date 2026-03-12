---
apply: always
title: Model Context Protocol (MCP) Implementation
category: AI & Integration
description: Implement and expose tools via Model Context Protocol for AI agent integration
---

# Model Context Protocol (MCP) Rules

Implement the Model Context Protocol to expose application functionality to AI agents securely and efficiently.

## Tool Definition Pattern

```csharp
using System.ComponentModel;
using Microsoft.AspNetCore.Authorization;
using ModelContextProtocol.Server;

namespace Microscope.Boilerplate.Todo.Slices.Features.CreateTodoList;

[McpServerToolType]
public static class CreateTodoListMcpTool
{
    [Authorize]
    [McpServerTool, Description("Create a new todo list")]
    public static async Task<Guid> CreateTodoList(
        IMediator mediator,
        [Description("Todo list name")] string name)
    {
        return await mediator.Send(new CreateTodoListCommand(name));
    }
}

```

## Best Practices

✅ **Do:**
- Document tools with clear descriptions and schemas
- Handle all error cases with meaningful messages
- Use CQRS queries/commands for business logic
- Test tools with success and error scenarios
- Implement proper authorization for sensitive operations
- Paginate large result sets

❌ **Avoid:**
- Tools without clear input schemas
- Complex business logic directly in tools
- Unhandled exceptions
- Returning sensitive data without access control
- Ambiguous or poorly typed schemas
