using FluentValidation;

namespace Microscope.Boilerplate.Services.TodoList.Infrastructure.Services.Mail;

public class MailOptions
{
    public const string SMTP_ADAPTER = "smtp";
    public const string SENDGRID_ADAPTER = "sendgrid";
    
    public const string ConfigurationKey = "Mail";
    
    public string? Adapter { get; set; }
    public string? ApiKey { get; set; }
}

public class MailOptionsValidator : AbstractValidator<MailOptions>
{
    public MailOptionsValidator()
    {
        RuleFor(x => x.Adapter)
            .NotNull()
            .NotEmpty()
            .Must(x => x is MailOptions.SMTP_ADAPTER or MailOptions.SENDGRID_ADAPTER)
            .WithMessage("Adapter must be : smtp|sendgrid, not empty or null string");

        When(x => x.Adapter == MailOptions.SENDGRID_ADAPTER, () =>
        {
            RuleFor(x => x.ApiKey)
                .NotNull()
                .NotEmpty()
                .WithMessage("If mail adapter is not smtp, ApiKey configuration is required");
        });
    }
}