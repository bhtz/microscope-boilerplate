using MediatR;
using Microscope.Framework.Domain.CQRS;
using Microscope.Framework.Domain.DDD;
using Microsoft.Extensions.Logging;

namespace Microscope.Framework.Application.CQRS.Behaviors;

public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : ICommand<TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
    private readonly IUnitOfWork _unitOfWork;
    
    public TransactionBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var name = typeof(TRequest).Name;

        try
        {
            if (_unitOfWork.HasActiveTransaction)
            {
                return await next();
            }

            _logger.LogInformation("----- Begin transaction for command {CommandName} ", name);

            var response  = await _unitOfWork.EncapsulateInTransaction<TResponse>(async () => 
            {
                return await next();
            }, name);

            _logger.LogInformation("----- End transaction for command {CommandName} ", name);

            return response;
        }
        catch (System.Exception)
        {
            throw;
        }
    }
}
