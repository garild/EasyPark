namespace EasyPark.Application.CQRS;

public interface ISender
{
    Task<TResponse> SendAsync<TRequest, TResponse>(TRequest request, CancellationToken ct = default);
}