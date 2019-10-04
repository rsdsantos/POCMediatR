using Communication.API.Domain.Exceptions;
using Communication.API.Infrastructure.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Communication.API.Application.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger) => _logger = logger;

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            TResponse response = default;

            _logger.LogInformation("::::::::::::: Handling command {CommandName} ({@Command})", request.GetGenericTypeName(), request);

            await Policy
              .Handle<DomainException>()
              .RetryAsync(3)
              .ExecuteAsync(async () => { response = await next(); });         

            _logger.LogInformation("::::::::::::: Command {CommandName} handled - response: {@Response}", request.GetGenericTypeName(), response);

            return response;
        }
    }
}
