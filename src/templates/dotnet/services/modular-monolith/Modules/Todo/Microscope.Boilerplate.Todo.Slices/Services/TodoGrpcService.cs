#if (Grpc)
using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microscope.Boilerplate.Todo.Slices.Features.CreateTodoList;
using Microscope.Boilerplate.Todo.Slices.Features.GetTodoLists;
using Microscope.Boilerplate.Todo.Slices.Features.GetTodoListsById;
using Microscope.Boilerplate.Todo.Slices.Features.UpdateTodoList;
using Microscope.Boilerplate.Todo.Slices.Features.DeleteTodoList;
using Microscope.Boilerplate.Todo.Slices.Features.CreateTodoItem;
using Microscope.Boilerplate.Todo.Slices.Features.ToggleTodoItem;
using Microscope.Boilerplate.Todo.Slices.Features.AddTag;
using Microscope.Boilerplate.Todo.Slices.Grpc;

namespace Microscope.Boilerplate.Todo.Slices.Services;

[Authorize]
public class TodoGrpcService : TodoService.TodoServiceBase
{
    private readonly IMediator _mediator;

    public TodoGrpcService(IMediator mediator)
    {
        _mediator = mediator;
    }

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

    public override async Task<GetTodoListByIdResponse> GetTodoListById(
        GetTodoListByIdRequest request, 
        ServerCallContext context)
    {
        if (!Guid.TryParse(request.Id, out var id))
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid ID format"));
        }

        var result = await _mediator.Send(new GetTodoListByIdQuery(id));

        return new GetTodoListByIdResponse
        {
            TodoList = new TodoListDto
            {
                Id = result.Id.ToString(),
                Name = result.Name,
                IsCompleted = result.IsCompleted
            }
        };
    }

    public override async Task<CreateTodoListResponse> CreateTodoList(
        CreateTodoListRequest request, 
        ServerCallContext context)
    {
        var result = await _mediator.Send(new CreateTodoListCommand(request.Name));

        return new CreateTodoListResponse
        {
            Id = result.ToString()
        };
    }

    public override async Task<UpdateTodoListResponse> UpdateTodoList(
        UpdateTodoListRequest request, 
        ServerCallContext context)
    {
        if (!Guid.TryParse(request.Id, out var id))
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid ID format"));
        }

        var result = await _mediator.Send(new UpdateTodoListCommand(request.Name, id));

        return new UpdateTodoListResponse
        {
            Success = result
        };
    }

    public override async Task<DeleteTodoListResponse> DeleteTodoList(
        DeleteTodoListRequest request, 
        ServerCallContext context)
    {
        if (!Guid.TryParse(request.Id, out var id))
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid ID format"));
        }

        var result = await _mediator.Send(new DeleteTodoListCommand(id));

        return new DeleteTodoListResponse
        {
            Success = result
        };
    }

    public override async Task<CreateTodoItemResponse> CreateTodoItem(
        CreateTodoItemRequest request, 
        ServerCallContext context)
    {
        if (!Guid.TryParse(request.TodoListId, out var todoListId))
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid TodoListId format"));
        }

        var result = await _mediator.Send(new CreateTodoItemCommand(
            request.Title, 
            request.Description, 
            todoListId));

        return new CreateTodoItemResponse
        {
            Id = result.ToString()
        };
    }

    public override async Task<ToggleTodoItemResponse> ToggleTodoItem(
        ToggleTodoItemRequest request, 
        ServerCallContext context)
    {
        if (!Guid.TryParse(request.Id, out var id))
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid ID format"));
        }

        var result = await _mediator.Send(new ToggleTodoItemCommand(id));

        return new ToggleTodoItemResponse
        {
            Success = result
        };
    }

    public override async Task<AddTagResponse> AddTag(
        AddTagRequest request, 
        ServerCallContext context)
    {
        if (!Guid.TryParse(request.TodoListId, out var todoListId))
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid TodoListId format"));
        }

        var result = await _mediator.Send(new AddTagCommand(
            request.Label, 
            todoListId, 
            request.Color));

        return new AddTagResponse
        {
            Success = result
        };
    }
}
#endif
