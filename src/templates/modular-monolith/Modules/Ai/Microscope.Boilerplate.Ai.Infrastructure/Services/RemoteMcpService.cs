using FluentValidation;
using Microscope.Boilerplate.Ai.Slices.Services;
using Microscope.Boilerplate.Framework.Application.Services;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ModelContextProtocol.Client;

namespace Microscope.Boilerplate.Ai.Infrastructure.Services;

public class RemoteMcpService(
    IOptions<RemoteMcpOptions> remoteMcpSettings,
    IServiceScopeFactory serviceScopeFactory,
    ILogger<RemoteMcpService> logger) : IRemoteMcpService
{
    private readonly RemoteMcpOptions _remoteMcpOptions = remoteMcpSettings.Value;
    private readonly SemaphoreSlim _toolsLock = new(1, 1);
    private List<AITool>? _cachedTools;

    public async Task<IEnumerable<AITool>> GetToolsAsync(CancellationToken cancellationToken)
    {
        if (_cachedTools is not null)
        {
            return _cachedTools;
        }

        await _toolsLock.WaitAsync(cancellationToken);
        
        try
        {
            if (_cachedTools is not null)
            {
                return _cachedTools;
            }

            _cachedTools = await FetchToolsAsync(cancellationToken);
            return _cachedTools;
        }
        finally
        {
            _toolsLock.Release();
        }
    }

    private async Task<List<AITool>> FetchToolsAsync(CancellationToken cancellationToken)
    {
        var useServerToken = remoteMcpSettings.Value.Servers.Any(server => server.UseServerToken);

        var token = string.Empty;

        if (useServerToken)
        {
            using var scope = serviceScopeFactory.CreateScope();
            var identityService = scope.ServiceProvider.GetRequiredService<IIdentityService>();
            token = await identityService.GetTokenAsync();
        }

        var tools = new List<AITool>();

        foreach (var mcp in remoteMcpSettings.Value.Servers)
        {
            var clientTransportOptions = new HttpClientTransportOptions()
            {
                Endpoint = new Uri(mcp.EndPoint),
                AdditionalHeaders = mcp.AdditionalHeaders
            };

            if (mcp.UseServerToken)
            {
                clientTransportOptions.AdditionalHeaders = new Dictionary<string, string>()
                {
                    { "Authorization", $"Bearer {token}" }
                };
            }

            var transport = new HttpClientTransport(clientTransportOptions);
            var client = await McpClient.CreateAsync(transport, null, null, cancellationToken);
            var remoteTools = await client.ListToolsAsync(cancellationToken: cancellationToken);
            tools.AddRange(remoteTools);
        }

        return tools;
    }
}

public class RemoteMcpOptions
{
    public const string ConfigurationKey = "RemoteMcp";

    public IEnumerable<RemoteMcpServer> Servers { get; set; }
}

public class RemoteMcpServer
{
    public string EndPoint { get; set; } = default!;
    public bool UseServerToken { get; set; }
    public Dictionary<string, string>? AdditionalHeaders { get; set; }
}

public class RemoteMcpOptionsValidator : AbstractValidator<RemoteMcpOptions>
{
    public RemoteMcpOptionsValidator()
    {
        RuleForEach(x => x.Servers)
            .SetValidator(new RemoteMcpServerValidator());
    }
}

public class RemoteMcpServerValidator : AbstractValidator<RemoteMcpServer>
{
    public RemoteMcpServerValidator()
    {
        RuleFor(x => x.EndPoint)
            .NotNull()
            .NotEmpty()
            .WithMessage("Remote MCP option EndPoint must have a value");
    }
}