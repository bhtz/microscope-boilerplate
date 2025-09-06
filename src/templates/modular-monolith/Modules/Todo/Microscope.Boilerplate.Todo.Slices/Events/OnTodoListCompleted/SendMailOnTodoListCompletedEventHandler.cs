using Microscope.Boilerplate.Todo.Domain.TodoListAggregate.Events;

namespace Microscope.Boilerplate.Todo.Slices.Events.OnTodoListCompleted;

public class SendMailOnTodoListCompletedEventHandler : INotificationHandler<OnTodoListCompletedEvent>
{
    public Task Handle(OnTodoListCompletedEvent notification, CancellationToken cancellationToken)
    {
        // implement mail sending logic here
        throw new NotImplementedException();
    }
}
