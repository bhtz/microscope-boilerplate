using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Microscope.Boilerplate.Services.TodoApp.Application.Common.Behaviors;

public class PerformanceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly Stopwatch _timer;
    private readonly ILogger<TRequest> _logger;

    public PerformanceBehavior(ILogger<TRequest> logger)
    {
        _timer = new Stopwatch();
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _timer.Start();

        var response = await next();

        _timer.Stop();

        var elapsedMilliseconds = _timer.ElapsedMilliseconds;

        if (elapsedMilliseconds > 2000)
        {
            var requestName = typeof(TRequest).Name;
            _logger.LogWarning("Long Running Request {Name} ({ElapsedMilliseconds} milliseconds)", requestName, elapsedMilliseconds);
        }

        return response;
    }
}
