using Microscope.Boilerplate.Services.TodoApp.Application.Services;
using Microscope.Storage;
using Microsoft.Extensions.Options;

namespace Microscope.Boilerplate.Services.TodoApp.Infrastructure.Services.Storage;

public class FileStorageService : IFileStorageService
{
    private readonly IStorageService _storageService;
    private readonly IOptions<StorageOptions> _options;

    public FileStorageService(IStorageService storageService, IOptions<StorageOptions> options)
    {
        _storageService = storageService;
        _options = options;
    }

    public async Task UploadTodoFileAsync(string blobName, Stream data)
    {
        await _storageService.SaveBlobAsync(_options.Value.DefaultStorageContainer, blobName, data);
    }
}
