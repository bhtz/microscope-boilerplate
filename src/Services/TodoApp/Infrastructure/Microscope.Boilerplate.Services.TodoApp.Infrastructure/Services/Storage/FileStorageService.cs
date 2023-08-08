using Microscope.Boilerplate.Services.TodoApp.Application.Services;
using Microscope.Storage;

namespace Microscope.Boilerplate.Services.TodoApp.Infrastructure.Services.Storage;

public class FileStorageService : IFileStorageService
{
    private const string ContainerName = "todoapp";
    private readonly IStorageService _storageService;

    public FileStorageService(IStorageService storageService)
    {
        _storageService = storageService;
    }

    public async Task UploadTodoFileAsync(string blobName, Stream data)
    {
        await _storageService.SaveBlobAsync(ContainerName, blobName, data);
    }
}