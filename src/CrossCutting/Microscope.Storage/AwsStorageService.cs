namespace Microscope.Storage;

public class AwsStorageService : IStorageService
{
    public AwsStorageService()
    {
        
    }

    public Task DeleteBlobAsync(string containerName, string blobName)
    {
        throw new NotImplementedException();
    }

    public Task<Stream> GetBlobAsync(string bucketName, string objectName)
    {
        throw new NotImplementedException();
    }

    public Task SaveBlobAsync(string containerName, string blobName, Stream data)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<string>> ListContainersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<string>> ListBlobsAsync(string containerName)
    {
        throw new NotImplementedException();
    }

    public Task CreateContainerAsync(string containerName)
    {
        throw new NotImplementedException();
    }

    public Task DeleteContainerAsync(string containerName)
    {
        throw new NotImplementedException();
    }
}
