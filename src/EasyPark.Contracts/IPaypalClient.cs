using EasyPark.Contracts.Models;

namespace EasyPark.Contracts;

public interface IPaypalClient
{
    Task<OrderPaymentHttpResponse> CreateOrderAsync(OrderPaymentHttpRequest httpRequest,
        CancellationToken ct = default);
}