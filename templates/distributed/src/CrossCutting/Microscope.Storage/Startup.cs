using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Microscope.Storage;

public static class Startup
{
    public static IServiceCollection AddStorage(this IServiceCollection services)
    {
        var option = services
            .BuildServiceProvider()
            .GetRequiredService<IOptions<StorageOptions>>()
            .Value;

        switch (option.Adapter)
        {
            case StorageOptions.FILE_SYSTEM_ADAPTER :
                services.AddScoped<IStorageService, FileSystemStorageService>();
                break;

            case StorageOptions.AZURE_ADAPTER :
                services.AddScoped<IStorageService, BlobStorageService>();
                break;

            case StorageOptions.AWS_ADAPTER :
                services.AddScoped<IStorageService, AwsStorageService>();
                break;

            case StorageOptions.MINIO_ADAPTER :
                services.AddScoped<IStorageService, MinioStorageService>();
                break;

            default:
                services.AddScoped<IStorageService, FileSystemStorageService>();
                break;
        }
        
        return services;
    }
}