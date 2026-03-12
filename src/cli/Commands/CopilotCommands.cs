using Cocona;
using GitHub.Copilot.SDK;
using Microsoft.Extensions.AI;
using Spectre.Console;

public class CopilotCommands
{
    [Command("code")]
    public async Task RunCopilot()
    {
        AnsiConsole.Write(
            new FigletText("MICROSCOPE")
                .Color(Color.Aqua)
                .Centered());

        // Create and start client
        await using var client = new CopilotClient();
        await client.StartAsync();

        // Create a session with permission handler
        await using var session = await client.CreateSessionAsync(new SessionConfig
        {
            Model =  "gpt-4o-mini", //"gpt-5",
            OnPermissionRequest = PermissionHandler.ApproveAll,
            Streaming = true,
            // SystemMessage = new SystemMessageConfig
            // {
            //     Mode = SystemMessageMode.Append,
            //     Content = """
            //               You are a programming assistant specialized in .NET.
            //               """
            // },
        });
        
        var done = new TaskCompletionSource();
        
        session.On(evt =>
        {
            switch (evt)
            {
                // case AssistantMessageDeltaEvent delta:
                //     AnsiConsole.MarkupLine($"[bold aqua]{delta.Data.DeltaContent}[/]");
                //     break;
                case AssistantMessageEvent msg:
                    AnsiConsole.MarkupLine($"[bold aqua]{msg.Data.Content}[/]");
                    break;
                case SessionIdleEvent:
                    done.TrySetResult();
                    break;
                case SessionErrorEvent err:
                    done.TrySetException(new Exception(err.Data.Message));
                    break;
            }
        });
        
        while (true)
        {
            AnsiConsole.MarkupLine("\n [bold green]Enter your prompt (type 'exit' to quit) :[/]");
            var prompt = Console.ReadLine();
            
            if (prompt == "exit") break;
            if (string.IsNullOrEmpty(prompt)) continue;
            
            var response = await session.SendAsync(new MessageOptions { Prompt = prompt });
            await done.Task;
        }
    }
}
