namespace Microscope.Boilerplate.Framework.Application.Services;

public interface IUserService
{
    Task<IEnumerable<DomainUser>> GetUsersAsync(int limit);
    Task<IEnumerable<DomainUser>> SearchUsersAsync(string search);
}

public record DomainUser(Guid Id, string Email, string UserName);
