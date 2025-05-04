using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace BookingSystem.Application.Behaviors;

public class LoggingBehavior < TRequest, TResponse> :IPipelineBehavior<TRequest, TResponse>
{
    public async Task<TResponse> Handle(
        TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        Console.WriteLine($"[START] {requestName}");
        
        var response = await next();
        
        Console.WriteLine($"[END] {requestName}");
        return response;
    }
    
}