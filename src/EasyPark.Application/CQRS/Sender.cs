using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EasyPark.Application.CQRS;

public class Sender : ISender
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<Sender> _logger;

    // ReSharper disable once ConvertToPrimaryConstructor
    public Sender(IServiceProvider serviceProvider, ILogger<Sender> logger)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<TResponse> SendAsync<TRequest, TResponse>(TRequest request, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var requestType = typeof(TRequest);
        var responseType = typeof(TResponse);
        
        _logger.LogDebug("Sending request of type {RequestType} expecting response of type {ResponseType}", 
            requestType.Name, responseType.Name);

        try
        {
            // Resolve handler using generic method for better performance.
            // Let it throw to get more information about not registered handler
            var innerHandler = _serviceProvider.GetRequiredService<IHandler<TRequest, TResponse>>();
            
            _logger.LogDebug("Handler {HandlerType} resolved successfully", innerHandler.GetType().Name);

            // Call handler
            var result = await innerHandler.Handle(request, ct);
            
            _logger.LogDebug("Request of type {RequestType} handled successfully", requestType.Name);
            
            return result;
        }
        catch (InvalidOperationException)
        {
            // Re-throw InvalidOperationException as-is (handler not found)
            // TODO: Required global exception handler
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error handling request of type {RequestType}", requestType.Name);
            throw;
        }
    }
}