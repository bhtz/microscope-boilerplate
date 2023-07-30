using AutoMapper;
using Microscope.Boilerplate.Services.TodoApp.Application.Features.TodoLists.Queries.GetTodoLists;
using Microscope.Boilerplate.Services.TodoApp.Domain.Aggregates.TodoListAggregate;

namespace Microscope.Boilerplate.Services.TodoApp.Application.Common.Mappings;

public class TodoListMapping : Profile
{
    public TodoListMapping()
    {
        CreateMap<TodoList, TodoListQueryResult>()
            .ReverseMap();
        
        CreateMap<TodoList, TodoListByIdQueryResult>()
            .ReverseMap();
    }
}