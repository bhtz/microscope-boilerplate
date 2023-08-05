using MediatR;
using Microscope.Boilerplate.Services.TodoApp.Domain.Aggregates.TodoListAggregate.Events;

namespace Microscope.Boilerplate.Services.TodoApp.Application.Features.TodoLists.Events;

public class IntegrationOnTodoListCompletedEventHandler : INotificationHandler<OnTodoListCompletedEvent>
{
    public async Task Handle(OnTodoListCompletedEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine("Not implemented yet");
    }
}