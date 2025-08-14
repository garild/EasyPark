namespace EasyPark.Application.CQRS;

// Maker for DI
public interface IHandler;
public interface IHandler<in TRequest, TResponse> : IHandler
{
    Task<TResponse> Handle(TRequest request, CancellationToken ct = default);
}