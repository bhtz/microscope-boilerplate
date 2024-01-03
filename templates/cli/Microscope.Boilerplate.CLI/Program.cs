using Cocona;
using Microscope.Boilerplate.CLI.Commands;
using Spectre.Console;

var builder = CoconaApp.CreateBuilder();

var app = builder.Build();

app.AddCommands<SampleCommands>();
app.AddCommand(() =>
{
    AnsiConsole.Write(
        new FigletText("MICROSCOPE BOILERPLATE")
            .Color(Color.Aqua)
            .Centered());
    
    Console.ReadKey();
});

app.Run();
