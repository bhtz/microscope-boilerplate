using Cocona;
using Spectre.Console;
using MassTransit;
using Microscope.Boilerplate.Clients.CLI.Consumers;

var rabbitMqHost = "rabbitmq://localhost";
var rabbitMqUsername = "guest";
var rabbitMqPassword = "guest";

var builder = CoconaApp.CreateBuilder();

builder.Services
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
    });
 
var app = builder.Build();

app.AddCommand(() =>
{
    AnsiConsole.Write(
        new FigletText("MICROSCOPE BOILERPLATE")
            .Color(Color.Aqua)
            .Centered());
    
    Console.ReadKey();
});

app.Run();
