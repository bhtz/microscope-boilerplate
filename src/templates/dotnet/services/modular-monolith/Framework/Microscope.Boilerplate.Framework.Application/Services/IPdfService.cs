namespace Microscope.Boilerplate.Framework.Application.Services;

public interface IPdfService
{
    Task<byte[]> GenerateAndDownloadAsync(string template, string filename, object data);
    
    Task GenerateAsync(string template, string filename, object data);
}
