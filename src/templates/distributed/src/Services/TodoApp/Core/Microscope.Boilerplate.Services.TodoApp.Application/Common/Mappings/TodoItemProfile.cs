using AutoMapper;
using Microscope.Boilerplate.Services.TodoApp.Application.Features.TodoLists.Queries.GetTodoLists;
using Microscope.Boilerplate.Services.TodoApp.Application.Features.TodoLists.Queries.GetTodoListsById;
using Microscope.Boilerplate.Services.TodoApp.Domain.Aggregates.TodoListAggregate.Entities;

namespace Microscope.Boilerplate.Services.TodoApp.Application.Common.Mappings;

public class TodoItemProfile : Profile
{
    public TodoItemProfile()
    {
        CreateMap<TodoItem, TodoItemResult>()
            .ReverseMap();
    }
}