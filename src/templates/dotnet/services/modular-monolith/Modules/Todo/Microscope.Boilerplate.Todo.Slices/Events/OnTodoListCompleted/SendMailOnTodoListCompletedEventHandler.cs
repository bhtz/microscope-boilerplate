using Microscope.Boilerplate.Todo.Domain.TodoListAggregate.Events;
using Microsoft.Extensions.Logging;

namespace Microscope.Boilerplate.Todo.Slices.Events.OnTodoListCompletedEvent;

public class SendMailOnTodoListCompletedEventHandler(ILogger logger) : INotificationHandler<OnTodoListCompletedEvent>
{
    public Task Handle(OnTodoListCompletedEvent notification, CancellationToken cancellationToken)
    {
        // implement mail sending logic here
        logger.LogInformation("TEST DOMAIN EVENT HANDLER");
        
        throw new NotImplementedException();
    }
}
