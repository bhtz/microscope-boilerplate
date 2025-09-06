using Cocona;
using Microscope.Boilerplate.CLI.Commands;
using Spectre.Console;

var builder = CoconaApp.CreateBuilder();

var app = builder.Build();

app.AddCommands<SampleCommands>();
app.AddCommands<PromptCommands>();
app.AddCommand(() =>
{
    AnsiConsole.Write(
        new FigletText("Microscope")
            .Color(Color.Aqua)
            .Centered());
    
    Console.ReadKey();
});

app.Run();
