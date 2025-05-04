using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
namespace BookingSystem.Application.Behaviors;

public class ExceptionHandlingBehavior < TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    public async Task<TResponse> Handle(
        TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[EXCEPTION] {typeof(TRequest).Name}: {ex.Message}");
            throw; // Skicka vidare felet till API:t
        }
    }
}