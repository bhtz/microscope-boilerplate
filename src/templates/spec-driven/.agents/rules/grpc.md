---
apply: always
---

# gRPC Services Coding Rules

Spec-driven development rules for gRPC service definitions and implementations.

## Architecture Principles

- **Contract-First Design**: Define `.proto` files before implementation
- **Code Generation**: Use protobuf compiler to generate C# code
- **Service Organization**: One service per module domain
- **Streaming Support**: Support unary, server streaming, client streaming, and bidirectional streaming
- **Error Handling**: Use gRPC status codes for error communication
- **Performance**: Leverage binary serialization and HTTP/2 multiplexing

## Proto File Structure

Location: `Modules/{FeatureName}/Slices/Grpc/{FeatureName}.proto`

### Basic Service Definition

```protobuf
syntax = "proto3";

option csharp_namespace = "Microscope.Boilerplate.Todo.Slices.Grpc";

package todoservice;

// Service principal Todo
service TodoService {
  // Opérations sur les TodoLists
  rpc GetTodoLists (GetTodoListsRequest) returns (GetTodoListsResponse);
  rpc GetTodoListById (GetTodoListByIdRequest) returns (GetTodoListByIdResponse);
  rpc CreateTodoList (CreateTodoListRequest) returns (CreateTodoListResponse);
  rpc UpdateTodoList (UpdateTodoListRequest) returns (UpdateTodoListResponse);
  rpc DeleteTodoList (DeleteTodoListRequest) returns (DeleteTodoListResponse);

  // Opérations sur les TodoItems
  rpc CreateTodoItem (CreateTodoItemRequest) returns (CreateTodoItemResponse);
  rpc ToggleTodoItem (ToggleTodoItemRequest) returns (ToggleTodoItemResponse);
  rpc DeleteTodoItem (DeleteTodoItemRequest) returns (DeleteTodoItemResponse);

  // Opération sur les Tags
  rpc AddTag (AddTagRequest) returns (AddTagResponse);
  rpc RemoveTag (RemoveTagRequest) returns (RemoveTagResponse);

  // Version du module
  rpc GetTodoModuleVersion (GetTodoModuleVersionRequest) returns (GetTodoModuleVersionResponse);
}

// Messages pour GetTodoLists
message GetTodoListsRequest {}

message GetTodoListsResponse {
  repeated TodoListDto todoLists = 1;
}

// Messages pour GetTodoListById
message GetTodoListByIdRequest {
  string id = 1;
}

message GetTodoListByIdResponse {
  TodoListDto todoList = 1;
}

// Messages pour CreateTodoList
message CreateTodoListRequest {
  string name = 1;
}

message CreateTodoListResponse {
  string id = 1;
}

// Messages pour UpdateTodoList
message UpdateTodoListRequest {
  string id = 1;
  string name = 2;
}

message UpdateTodoListResponse {
  bool success = 1;
}

// Messages pour DeleteTodoList
message DeleteTodoListRequest {
  string id = 1;
}

message DeleteTodoListResponse {
  bool success = 1;
}

// Messages pour CreateTodoItem
message CreateTodoItemRequest {
  string todoListId = 1;
  string title = 2;
  string description = 3;
}

message CreateTodoItemResponse {
  string id = 1;
}

// Messages pour ToggleTodoItem
message ToggleTodoItemRequest {
  string todoListId = 1;
  string todoItemId = 2;
}

message ToggleTodoItemResponse {
  bool success = 1;
}

// Messages pour DeleteTodoItem
message DeleteTodoItemRequest {
  string todoListId = 1;
  string todoItemId = 2;
}

message DeleteTodoItemResponse {
  bool success = 1;
}

// Messages pour AddTag
message AddTagRequest {
  string label = 1;
  string todoListId = 2;
  string color = 3;
}

message AddTagResponse {
  bool success = 1;
}

// Messages pour RemoveTag
message RemoveTagRequest {
  string label = 1;
  string todoListId = 2;
  string color = 3;
}

message RemoveTagResponse {
  bool success = 1;
}

// Messages pour GetTodoModuleVersion
message GetTodoModuleVersionRequest {}

message GetTodoModuleVersionResponse {
  string version = 1;
}

// DTOs
message TodoListDto {
  string id = 1;
  string name = 2;
  bool isCompleted = 3;
  repeated TodoItemDto items = 4;
  repeated TagDto tags = 5;
}

message TodoItemDto {
  string id = 1;
  string title = 2;
  string description = 3;
  bool isCompleted = 4;
}

message TagDto {
  string label = 1;
  string color = 2;
}

```

## gRPC Service Implementation

Location: `Modules/{ModuleName}/{Namespace}.Slices/Features/{UseCase}/{UseCase}RpcService.cs`

### Basic Service

```csharp
using Grpc.Core;
using Microscope.Boilerplate.Todo.Slices.Features.GetTodoLists;
using Microscope.Boilerplate.Todo.Slices.Grpc;
using Microsoft.AspNetCore.Authorization;

namespace Microscope.Boilerplate.Todo.Slices.Services;

public partial class TodoGrpcService
{
    [Authorize]
    public override async Task<GetTodoListsResponse> GetTodoLists(
        GetTodoListsRequest request,
        ServerCallContext context)
    {
        var result = await _mediator.Send(new GetTodoListQuery());

        var response = new GetTodoListsResponse();
        response.TodoLists.AddRange(result.Select(x => new TodoListDto
        {
            Id = x.Id.ToString(),
            Name = x.Name,
            IsCompleted = x.IsCompleted
        }));

        return response;
    }
}
```

## Best Practices

- **Proto Evolution**: Never remove fields, mark as deprecated instead
- **Message Size**: Keep messages under 4MB for performance
- **Metadata**: Use gRPC metadata for headers and context
- **Compression**: Enable gzip compression for large payloads
- **Deadlines**: Always set timeout/deadline on client calls
- **Status Codes**: Use appropriate status codes for different error scenarios
- **Logging**: Log all gRPC calls for audit trail
- **Security**: Enable TLS/SSL in production
- **Testing**: Use `GrpcTestContext` for unit testing gRPC services

## Common gRPC Status Codes

| Code | Usage |
|------|-------|
| OK | Successful completion |
| CANCELLED | Operation cancelled by client |
| INVALID_ARGUMENT | Invalid request parameters |
| NOT_FOUND | Resource not found |
| ALREADY_EXISTS | Resource already exists |
| PERMISSION_DENIED | Authentication/authorization failure |
| RESOURCE_EXHAUSTED | Resource limit exceeded |
| FAILED_PRECONDITION | Operation precondition failed |
| ABORTED | Operation aborted (conflict) |
| INTERNAL | Internal server error |
| UNAVAILABLE | Service unavailable |
| DEADLINE_EXCEEDED | Request deadline exceeded |
