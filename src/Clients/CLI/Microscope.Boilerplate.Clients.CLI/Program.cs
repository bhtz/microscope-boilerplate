using Spectre.Console;
using MassTransit;
using Microscope.Boilerplate.Clients.CLI.Consumers;
using Microsoft.Extensions.DependencyInjection;

class Program
{
    static async Task Main(string[] args)
    {
        AnsiConsole.Write(
            new FigletText("MICROSCOPE BOILERPLATE")
                .Color(Color.Aqua)
                .Centered());
        
        var rabbitMqHost = "rabbitmq://localhost";
        var rabbitMqUsername = "guest";
        var rabbitMqPassword = "guest";

        var serviceProvider = new ServiceCollection()
            .AddMassTransit(x =>
            {
                x.AddConsumer<OnTodoListCompletedIntegrationEventConsumer>();
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri(rabbitMqHost), h =>
                    {
                        h.Username(rabbitMqUsername);
                        h.Password(rabbitMqPassword);
                    });

                    cfg.ReceiveEndpoint("todo-list-completed-listener", ep =>
                    {
                        ep.ConfigureConsumer<OnTodoListCompletedIntegrationEventConsumer>(context);
                    });
                });
            })
            .BuildServiceProvider();

        var bus = serviceProvider.GetRequiredService<IBusControl>();

        await bus.StartAsync();
        Console.WriteLine("Listening for Enterprise Service Bus messages ...");
        Console.WriteLine("Press any key to exit");
        
        Console.ReadKey();
        await bus.StopAsync();
    }
}
