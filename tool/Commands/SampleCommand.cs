using Cocona;

namespace Microscope.Boilerplate.Tool.CLI.Commands;

public class SampleCommands
{
    [Command("sample")]
    public async Task Sample(string name = "world")
    {
        Console.WriteLine($"Hello {name} !");
        Console.ReadKey();
    }
}
