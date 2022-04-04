using MediatR;
using Microsoft.Extensions.Logging;

namespace Mc2.CrudTest.Application.Cqrs.Customers.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> :
        IPipelineBehavior<TRequest, TResponse> where TResponse : class where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                _logger.LogInformation($"Handling {typeof(TRequest).Name}");
                TResponse response = await next();
                _logger.LogInformation($"Handled {typeof(TResponse).Name}");

                return response;
            }
            catch (Exception e)
            {
                _logger.LogError("Exception Occurred!!");
                _logger.LogError(e, e.Message);
                return default!;
            }
        }
    }
}
