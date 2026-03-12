using System.Net.Http.Json;
using Microscope.Boilerplate.Framework.Application.Services;
using Microsoft.Extensions.Options;

namespace Microscope.Boilerplate.Services.TodoList.Infrastructure.Services.User;

public class KeycloakUserService(HttpClient httpClient, IIdentityService identityService, IOptions<UserOptions> options)
    : IUserService
{
    public async Task<IEnumerable<DomainUser>> GetUsersAsync(int limit)
    {
        var token = await identityService.GetTokenAsync();

        try
        {
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var users = await httpClient.GetFromJsonAsync<IEnumerable<DomainUser>>(options.Value.UserServiceEndpoint);
            return users;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public Task<IEnumerable<DomainUser>> SearchUsersAsync(string search)
    {
        throw new NotImplementedException();
    }
}
