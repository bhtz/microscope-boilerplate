namespace Microscope.Boilerplate.Services.TodoApp.Application.Services;

public interface IAIService
{
    public Task<AITodoListResult> GetResult(string prompt);
}

public record AITodoListResult(string Result);