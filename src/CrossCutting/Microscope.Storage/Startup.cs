using Microsoft.Extensions.Configuration;
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
            case "filesystem":
                services.AddScoped<IStorageService, FileSystemStorageService>();
                break;

            case "azure":
                services.AddScoped<IStorageService, BlobStorageService>();
                break;

            case "aws":
                services.AddScoped<IStorageService, AwsStorageService>();
                break;

            case "minio":
                services.AddScoped<IStorageService, MinioStorageService>();
                break;

            default:
                services.AddScoped<IStorageService, FileSystemStorageService>();
                break;
        }
        
        return services;
    }
}