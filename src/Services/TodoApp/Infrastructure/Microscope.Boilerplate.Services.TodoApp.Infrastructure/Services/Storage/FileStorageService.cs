using Microscope.Boilerplate.Services.TodoApp.Application.Services;
using Microscope.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Microscope.Boilerplate.Services.TodoApp.Infrastructure.Services.Storage;

public class FileStorageService : IFileStorageService
{
    private string _containerName;
    private readonly IStorageService _storageService;
    private readonly IConfiguration _configuration;

    public FileStorageService(IStorageService storageService, IConfiguration configuration)
    {
        _storageService = storageService;
        _configuration = configuration;
        _containerName = _configuration.GetValue<string>("DefaultStorageContainer") ?? throw new ArgumentNullException("StorageContainer");
    }

    public async Task UploadTodoFileAsync(string blobName, Stream data)
    {
        await _storageService.SaveBlobAsync(_containerName, blobName, data);
    }
}