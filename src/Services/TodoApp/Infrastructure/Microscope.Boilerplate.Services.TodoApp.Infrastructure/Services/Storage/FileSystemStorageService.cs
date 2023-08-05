using Microscope.Boilerplate.Services.TodoApp.Application.Services;
using Microsoft.Extensions.Options;

namespace Microscope.Boilerplate.Services.TodoApp.Infrastructure.Services.Storage;

public class FileSystemStorageService : IStorageService
{
    private readonly StorageOptions _options;

    public FileSystemStorageService(IOptions<StorageOptions> options)
    {
        _options = options.Value;
        this.CreateRootDirectoryIfNotExist();
    }

    public Task DeleteBlobAsync(string containerName, string blobName)
    {
        var containerPath = this.GetContainerPath(containerName);
        var filePath = Path.Combine(containerPath, blobName);

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
        else
        {
            throw new Exception("File not found");
        }

        return Task.CompletedTask;
    }

    public Task<Stream> GetBlobAsync(string containerName, string blobName)
    {
        var containerPath = this.GetContainerPath(containerName);
        var filePath = Path.Combine(containerPath, blobName);

        if (File.Exists(filePath))
        {
            var fileBytes = File.ReadAllBytes(filePath);
            Stream stream = new MemoryStream(fileBytes);

            return Task.FromResult(stream);
        }
        else
        {
            throw new Exception("File not found");
        }
    }

    public async Task SaveBlobAsync(string containerName, string blobName, Stream data)
    {
        await this.CreateContainerAsync(containerName);
        
        var path = Path.Combine(this.GetContainerPath(containerName), blobName);

        using (var fs = new FileStream(path, FileMode.Create))
        {
            await data.CopyToAsync(fs);
        }
    }

    public Task<IEnumerable<string>> ListContainersAsync()
    {
        var path = this.GetRootDirectoryPath();
        var directories = Directory.EnumerateDirectories(path);

        return Task.FromResult(directories);
    }

    public Task<IEnumerable<string>> ListBlobsAsync(string containerName)
    {
        var path = this.GetContainerPath(containerName);
        var files = Directory.EnumerateFiles(path);

        return Task.FromResult(files);
    }

    public Task CreateContainerAsync(string containerName)
    {
        var rootPath = this.GetRootDirectoryPath();

        var path = Path.Combine(rootPath, containerName);
        
        if(!Directory.Exists(path)) 
        {
            Directory.CreateDirectory(path);
        }

        return Task.CompletedTask;
    }

    public Task DeleteContainerAsync(string containerName)
    {
        var path = this.GetContainerPath(containerName);

        if (Directory.Exists(path))
        {
            Directory.Delete(path, true);
        }

        return Task.CompletedTask;
    }

    private void CreateRootDirectoryIfNotExist()
    {
        var path = this.GetRootDirectoryPath();

        if(!Directory.Exists(path)) 
        {
            Directory.CreateDirectory(path);
        }
    }

    private string GetRootDirectoryPath()
    {
        var rootDirectory = this._options.Host ?? "Uploads";
        return Path.Combine(Directory.GetCurrentDirectory(), rootDirectory);
    }
    
    private string GetContainerPath(string containerName)
    {
        return Path.Combine(this.GetRootDirectoryPath(), containerName);
    }
}
