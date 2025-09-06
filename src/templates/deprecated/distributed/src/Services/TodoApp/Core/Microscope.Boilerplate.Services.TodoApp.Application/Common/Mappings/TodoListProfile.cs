using AutoMapper;
using Microscope.Boilerplate.Services.TodoApp.Application.Features.TodoLists.Queries.GetTodoLists;
using Microscope.Boilerplate.Services.TodoApp.Application.Features.TodoLists.Queries.GetTodoListsById;
using Microscope.Boilerplate.Services.TodoApp.Domain.Aggregates.TodoListAggregate;

namespace Microscope.Boilerplate.Services.TodoApp.Application.Common.Mappings;

public class TodoListProfile : Profile
{
    public TodoListProfile()
    {
        CreateMap<TodoList, GetTodoListQueryResult>()
            .ForMember(
                destination => destination.IsCompleted,
                opt => opt.MapFrom(x => x.IsCompleted) )
            .ReverseMap();
        
        CreateMap<TodoList, GetTodoListByIdQueryResult>()
            .ReverseMap();
    }
}