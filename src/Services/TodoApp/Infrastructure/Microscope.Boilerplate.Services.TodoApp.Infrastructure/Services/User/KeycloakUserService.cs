using System.Net.Http.Json;
using Microscope.Boilerplate.Services.TodoApp.Application.Services;
using Microsoft.Extensions.Configuration;

namespace Microscope.Boilerplate.Services.TodoList.Infrastructure.Services.User;

public class KeycloakUserService : IUserService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly IIdentityService _identityService;

    public KeycloakUserService(HttpClient httpClient, IIdentityService identityService, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _identityService = identityService;
        _configuration = configuration;
    }

    public async Task<IEnumerable<DomainUser>> GetUsersAsync(int limit)
    {
        var token = _identityService.GetToken();
        var userServiceEndpoint = _configuration.GetValue<string>("Users:UserServiceEndpoint");
        
        try
        {
            _httpClient.DefaultRequestHeaders.Add("Authorization", token);
            var users = await _httpClient.GetFromJsonAsync<IEnumerable<DomainUser>>(userServiceEndpoint);
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
