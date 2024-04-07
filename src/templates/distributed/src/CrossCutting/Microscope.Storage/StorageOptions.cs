using FluentValidation;

namespace Microscope.Storage;

public class StorageOptions
{
    public const string ConfigurationKey = "Storage";
    
    public const string FILE_SYSTEM_ADAPTER = "filesystem";
    public const string AZURE_ADAPTER = "azure";
    public const string AWS_ADAPTER = "aws";
    public const string MINIO_ADAPTER = "minio";
    
    public string Adapter { get; set; }
    public string Host { get; set; }
    public string Key { get; set; }
    public string Secret { get; set; }
    public string DefaultStorageContainer { get; set; } = "Uploads";
}

public class StorageOptionsValidator : AbstractValidator<StorageOptions>
{
    public StorageOptionsValidator()
    {
        RuleFor(x => x.Adapter)
            .NotNull()
            .NotEmpty()
            .Must(x => x 
                is StorageOptions.FILE_SYSTEM_ADAPTER 
                or StorageOptions.AZURE_ADAPTER 
                or StorageOptions.AWS_ADAPTER
                or StorageOptions.MINIO_ADAPTER)
            .WithMessage($"Adapter must be : {StorageOptions.FILE_SYSTEM_ADAPTER} | {StorageOptions.AZURE_ADAPTER} | {StorageOptions.AWS_ADAPTER} | {StorageOptions.MINIO_ADAPTER}, not empty or null string");

        When(x => x.Adapter is not StorageOptions.FILE_SYSTEM_ADAPTER, () =>
        {
            RuleFor(x => x.Host)
                .NotNull()
                .NotEmpty()
                .WithMessage("If Storage adapter is not filesystem, Host configuration is required");
            
            RuleFor(x => x.Key)
                .NotNull()
                .NotEmpty()
                .WithMessage("If Storage adapter is not filesystem, Key configuration is required");
            
            RuleFor(x => x.Secret)
                .NotNull()
                .NotEmpty()
                .WithMessage("If Storage adapter is not filesystem, Secret configuration is required");
        });
    }
}
