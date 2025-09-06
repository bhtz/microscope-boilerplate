using Cocona;
using Spectre.Console;

namespace Microscope.Boilerplate.CLI.Commands;

public class PromptCommands
{
    [Command("prompt")]
    public void Prompt()
    {
        var name = AnsiConsole.Ask<string>("Enter your name");
        Console.WriteLine($"Hello {name} !");
    }
}
