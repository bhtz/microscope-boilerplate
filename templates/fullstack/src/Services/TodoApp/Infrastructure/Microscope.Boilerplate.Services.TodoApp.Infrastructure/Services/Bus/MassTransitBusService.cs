using MassTransit;
using Microscope.Boilerplate.Services.TodoApp.Application.Services;

namespace Microscope.Boilerplate.Services.TodoApp.Infrastructure.Services.Bus;

public class MassTransitBusService : IBusService
{
    private readonly IPublishEndpoint _publishEndpoint;

    public MassTransitBusService(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task Publish<T>(T message) where T : class
    {
        await _publishEndpoint.Publish<T>(message);
    }
}