using FluentValidation;

namespace Microscope.Boilerplate.Services.TodoApp.Infrastructure.Services.Bus;

public class BusOptions
{
    public const string ConfigurationKey = "Bus";
    public const string INMEMORY_ADAPTER = "inmemory";
    public const string AZURE_ADAPTER = "azure";
    public const string AWS_ADAPTER = "aws";
    public const string RABBITMQ_ADAPTER = "rabbitmq";
    public string Adapter { get; set; }
    public string Host { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}

public class BusOptionsValidator : AbstractValidator<BusOptions>
{
    public BusOptionsValidator()
    {
        RuleFor(x => x.Adapter)
            .NotNull()
            .NotEmpty()
            .Must(x => x 
                is BusOptions.INMEMORY_ADAPTER 
                or BusOptions.AZURE_ADAPTER 
                or BusOptions.AWS_ADAPTER 
                or BusOptions.RABBITMQ_ADAPTER)
            .WithMessage($"Adapter must be : {BusOptions.INMEMORY_ADAPTER} | {BusOptions.AZURE_ADAPTER} | {BusOptions.AWS_ADAPTER} | {BusOptions.RABBITMQ_ADAPTER}, not empty or null string");

        When(x => x.Adapter is not BusOptions.INMEMORY_ADAPTER, () =>
        {
            RuleFor(x => x.Host)
                .NotNull()
                .NotEmpty()
                .WithMessage("If bus adapter is not inmemory, Host configuration is required");
            
            RuleFor(x => x.Username)
                .NotNull()
                .NotEmpty()
                .WithMessage("If bus adapter is not inmemory, Username configuration is required");
            
            RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty()
                .WithMessage("If bus adapter is not inmemory, Password configuration is required");
        });
    }
}