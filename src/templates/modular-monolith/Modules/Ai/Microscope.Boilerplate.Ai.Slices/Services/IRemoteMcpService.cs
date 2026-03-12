using Microsoft.Extensions.AI;

namespace Microscope.Boilerplate.Ai.Slices.Services;

public interface IRemoteMcpService
{
    Task<IEnumerable<AITool>> GetToolsAsync(CancellationToken cancellationToken);
}