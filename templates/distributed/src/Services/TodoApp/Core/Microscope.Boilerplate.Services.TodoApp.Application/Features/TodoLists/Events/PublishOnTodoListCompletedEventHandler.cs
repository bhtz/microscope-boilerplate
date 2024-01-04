using AutoMapper;
using MediatR;
using Microscope.Boilerplate.Services.TodoApp.Application.Services;
using Microscope.Boilerplate.Services.TodoApp.Domain.Aggregates.TodoListAggregate.Events;

namespace Microscope.Boilerplate.Services.TodoApp.Application.Features.TodoLists.Events;

public class PublishOnTodoListCompletedEventHandler : INotificationHandler<OnTodoListCompletedEvent>
{
    private readonly IBusService _busService;
    private readonly IMapper _mapper;

    public PublishOnTodoListCompletedEventHandler(IBusService busService, IMapper mapper)
    {
        _busService = busService;
        _mapper = mapper;
    }

    public async Task Handle(OnTodoListCompletedEvent notification, CancellationToken cancellationToken)
    {
        var dto = new OnTodoListCompletedIntegrationEvent(notification.TodoList.Id, notification.CreatedAt);
        await _busService.Publish<OnTodoListCompletedIntegrationEvent>(dto);
    }

    public record OnTodoListCompletedIntegrationEvent(Guid TodoListId, DateTime CreatedAt);
}