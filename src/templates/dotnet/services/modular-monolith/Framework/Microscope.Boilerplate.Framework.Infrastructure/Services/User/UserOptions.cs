using FluentValidation;

namespace Microscope.Boilerplate.Services.TodoList.Infrastructure.Services.User;

public class UserOptions
{
    public const string ConfigurationKey = "Users";
    public const string KEYCLOAK_ADAPTER = "keycloak";
    public string Adapter { get; set; }
    public string UserServiceEndpoint { get; set; }
}

public class UserOptionsValidator : AbstractValidator<UserOptions>
{
    public UserOptionsValidator()
    {
        RuleFor(x => x.Adapter)
            .NotNull()
            .NotEmpty()
            .Must(x => x is UserOptions.KEYCLOAK_ADAPTER)
            .WithMessage("Adapter must be : keycloak, not empty or null string");

        RuleFor(x => x.UserServiceEndpoint)
            .NotNull()
            .NotEmpty()
            .WithMessage("UserServiceEndpoint ApiKey configuration is required");
    }
}