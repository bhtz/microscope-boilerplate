namespace Microscope.Boilerplate.Services.TodoApp.Application.Services;

public interface IPDFService
{
    Task<byte[]> GenerateAndDownloadAsync(string template, string filename, object data);
    
    Task GenerateAsync(string template, string filename, object data);
}
