using AutoMapper;
using Microscope.Boilerplate.Services.TodoApp.Application.Features.TodoLists.Queries.GetTodoLists;
using Microscope.Boilerplate.Services.TodoApp.Application.Features.TodoLists.Queries.GetTodoListsById;
using Microscope.Boilerplate.Services.TodoApp.Domain.Aggregates.TodoListAggregate.Entities;
using Microscope.Boilerplate.Services.TodoApp.Domain.Aggregates.TodoListAggregate.ValueObjects;

namespace Microscope.Boilerplate.Services.TodoApp.Application.Common.Mappings;

public class TagProfile : Profile
{
    public TagProfile()
    {
        CreateMap<Tag, TagResult>()
            .ReverseMap();
    }
}