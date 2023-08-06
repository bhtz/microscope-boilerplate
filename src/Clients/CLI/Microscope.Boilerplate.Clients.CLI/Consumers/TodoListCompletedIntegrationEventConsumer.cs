using MassTransit;
using Microscope.Boilerplate.Services.TodoApp.Application.Features.TodoLists.Events;

namespace Microscope.Boilerplate.Clients.CLI.Consumers;

public class TodoListCompletedIntegrationEventConsumer : IConsumer<OnTodoListCompletedIntegrationEventHandler.OnTodoListCompletedIntegrationEvent>
{
    public Task Consume(ConsumeContext<OnTodoListCompletedIntegrationEventHandler.OnTodoListCompletedIntegrationEvent> context)
    {
        Console.WriteLine($"OnTodoListCompletedIntegrationEvent received : {context.Message.TodoListId} - {context.Message.CreatedAt}");
        return Task.CompletedTask;
    }
}