using MassTransit;
using Microscope.Boilerplate.Services.TodoApp.Application.Features.TodoLists.Events;

namespace Microscope.Boilerplate.Clients.CLI.Consumers;

public class OnTodoListCompletedIntegrationEventConsumer : IConsumer<PublishOnTodoListCompletedEventHandler.OnTodoListCompletedIntegrationEvent>
{
    public Task Consume(ConsumeContext<PublishOnTodoListCompletedEventHandler.OnTodoListCompletedIntegrationEvent> context)
    {
        Console.WriteLine($"OnTodoListCompletedIntegrationEvent received : {context.Message.TodoListId} - {context.Message.CreatedAt}");
        return Task.CompletedTask;
    }
}