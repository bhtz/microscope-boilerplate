using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Microscope.Storage;

public class BlobStorageService : IStorageService
{
    private readonly StorageOptions _options;
    private readonly CloudBlobClient _client;

    public BlobStorageService(IOptions<StorageOptions> options)
    {
        _options = options.Value;
        this._client = CloudStorageAccount.Parse(_options.Host).CreateCloudBlobClient();
    }

    public async Task DeleteBlobAsync(string containerName, string blobName)
    {
        var container = await this.GetContainerByNameAsync(containerName);
        CloudBlockBlob blob = container.GetBlockBlobReference(blobName);

        await blob.DeleteAsync();
    }

    public async Task<Stream> GetBlobAsync(string containerName, string blobName)
    {
        var container = await this.GetContainerByNameAsync(containerName);
        CloudBlockBlob blob = container.GetBlockBlobReference(blobName);

        await blob.FetchAttributesAsync();

        byte[] data = new byte[blob.Properties.Length];
        await blob.DownloadToByteArrayAsync(data, 0);

        return new MemoryStream(data);
    }

    public async Task SaveBlobAsync(string containerName, string blobName, Stream data)
    {
        var container = await this.GetContainerByNameAsync(containerName);
        CloudBlockBlob blob = container.GetBlockBlobReference(blobName);

        await blob.UploadFromStreamAsync(data);
    }

    public async Task<IEnumerable<string>> ListContainersAsync()
    {
        BlobContinuationToken continuationToken = null;
        var data = await this._client.ListContainersSegmentedAsync(continuationToken);

        return data
            .Results
            .Select(x => x.Name);
    }

    public async Task<IEnumerable<string>> ListBlobsAsync(string containerName)
    {
        var container = await this.GetContainerByNameAsync(containerName);

        var data = await container.ListBlobsSegmentedAsync(null);

        return data.Results
            .Select(x => x.Uri.ToString().Split('/').Last())
            .ToArray();
    }

    public async Task CreateContainerAsync(string containerName)
    {
        var container = await this.GetContainerByNameAsync(containerName);
        await container.CreateIfNotExistsAsync();
    }

    public async Task DeleteContainerAsync(string containerName)
    {
        var container = await this.GetContainerByNameAsync(containerName);
        await container.DeleteAsync();
    }

    private async Task<CloudBlobContainer> GetContainerByNameAsync(string containerName)
    {
        var container = this._client.GetContainerReference(containerName);
        bool exist = await container.ExistsAsync();

        if (exist)
        {
            return container;
        }
        else
        {
            throw new Exception("container does not exist");
        }
    }

    private async Task<string> GetBlobUri(string containerName, string blobName)
    {
        var container = await this.GetContainerByNameAsync(containerName);
        CloudBlockBlob blob = container.GetBlockBlobReference(blobName);

        return blob.Uri.ToString();
    }
}
