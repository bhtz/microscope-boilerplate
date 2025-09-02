using System.Net.Http.Json;
using Microscope.Boilerplate.Framework.Application.Services;
using Microsoft.Extensions.Options;

namespace Microscope.Boilerplate.Services.TodoList.Infrastructure.Services.User;

public class KeycloakUserService : IUserService
{
    private readonly HttpClient _httpClient;
    private readonly IIdentityService _identityService;
    private readonly IOptions<UserOptions> _options;

    public KeycloakUserService(HttpClient httpClient, IIdentityService identityService, IOptions<UserOptions> options)
    {
        _httpClient = httpClient;
        _identityService = identityService;
        _options = options;
    }

    public async Task<IEnumerable<DomainUser>> GetUsersAsync(int limit)
    {
        var token = _identityService.GetToken();

        try
        {
            _httpClient.DefaultRequestHeaders.Add("Authorization", token);
            var users = await _httpClient.GetFromJsonAsync<IEnumerable<DomainUser>>(_options.Value.UserServiceEndpoint);
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
