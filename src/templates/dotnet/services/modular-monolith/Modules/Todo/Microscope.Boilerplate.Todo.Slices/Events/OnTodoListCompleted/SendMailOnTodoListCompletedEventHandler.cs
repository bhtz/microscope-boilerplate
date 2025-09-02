using Microscope.Boilerplate.Todo.Domain.TodoListAggregate.Events;
using Microsoft.Extensions.Logging;

namespace Microscope.Boilerplate.Todo.Slices.Events.OnTodoListCompleted;

public class SendMailOnTodoListCompletedEventHandler(ILogger logger) : INotificationHandler<OnTodoListCompletedEvent>
{
    public Task Handle(OnTodoListCompletedEvent notification, CancellationToken cancellationToken)
    {
        // implement mail sending logic here
        
        logger.LogInformation("DOMAIN EVENT HANDLER");
        throw new NotImplementedException();
    }
}
