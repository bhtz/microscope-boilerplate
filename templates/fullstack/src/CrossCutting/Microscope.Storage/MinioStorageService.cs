using System.Reactive.Linq;
using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel;
using Minio.Exceptions;

namespace Microscope.Storage;

public class MinioStorageService : IStorageService
{
    private readonly MinioClient _client;
    private readonly StorageOptions _options;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    public MinioStorageService(IOptions<StorageOptions> options)
    {
        _options = options.Value;
        this._client = new MinioClient()
            .WithEndpoint(_options.Host)
            .WithCredentials(_options.Key, _options.Secret).Build();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="containerName"></param>
    /// <param name="prefix"></param>
    /// <param name="recursive"></param>
    /// <returns></returns>
    public async Task<Stream> GetBlobAsync(string containerName, string blobName)
    {
        try
        {
            Stream fileStream = new MemoryStream();

            await this._client.GetObjectAsync(containerName, blobName, (stream) =>
            {
                stream.CopyTo(fileStream);
            });

            return fileStream;
        }
        catch (Exception e)
        {
            throw new Exception($"[Bucket]  Exception: {e}");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="containerName"></param>
    /// <returns></returns>
    public async Task<IEnumerable<string>> ListBlobsAsync(string containerName)
    {
        IList<Item> blobs = new List<Item>();

        try
        {
            blobs = await _client.ListObjectsAsync(containerName).ToList();
            return blobs.Select(x => x.Key);
        }
        catch (MinioException e)
        {
            throw new Exception($"[Bucket]  Exception: {e}");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="containerName"></param>
    /// <param name="blobName"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    public async Task SaveBlobAsync(string containerName, string blobName, Stream data)
    {
        data.Position = 0;
        await this._client.PutObjectAsync(containerName, blobName, data, data.Length, "application/octet-stream");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="containerName"></param>
    /// <param name="blobName"></param>
    /// <returns></returns>
    public async Task DeleteBlobAsync(string containerName, string blobName)
    {
        await this._client.RemoveObjectAsync(containerName, blobName);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="containerName"></param>
    /// <param name="blobName"></param>
    /// <returns></returns>
    private async Task<bool> BlobExistsAsync(string containerName, string blobName)
    {
        if (await this._client.BucketExistsAsync(containerName))
        {
            try
            {
                await this._client.StatObjectAsync(containerName, blobName);
            }
            catch (Exception e)
            {
                if (e is ObjectNotFoundException)
                {
                    return false;
                }

                throw;
            }

            return true;
        }

        return false;
    }

    /// <summary>
    /// List all containers
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<string>> ListContainersAsync()
    {
        var result = await this._client.ListBucketsAsync();
        return result.Buckets.Select(x => x.Name);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="containerName"></param>
    /// <returns></returns>
    public async Task CreateContainerAsync(string containerName)
    {
        await this._client.MakeBucketAsync(containerName);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="containerName"></param>
    /// <returns></returns>
    public async Task DeleteContainerAsync(string containerName)
    {
        await this._client.RemoveBucketAsync(containerName);
    }
}