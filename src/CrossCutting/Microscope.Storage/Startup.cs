using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microscope.Storage;

public static class Startup
{
    public static IServiceCollection AddStorage(this IServiceCollection services, IConfiguration configuration)
    {
        var option = new StorageOptions();
        var section = configuration.GetSection(StorageOptions.Name);
        section.Bind(option);
        
        services.AddOptions<StorageOptions>()
            .Bind(section)
            .ValidateDataAnnotations()
            .ValidateOnStart();

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