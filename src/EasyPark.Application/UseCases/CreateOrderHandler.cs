using EasyPark.Application.CQRS;
using EasyPark.Application.UseCases.Dto;
using EasyPark.Contracts;
using EasyPark.Contracts.Models;

namespace EasyPark.Application.UseCases;

public class CreateOrderHandler : IHandler<CreatePayPalOrderRequest, PayPalOrderResponse>
{
    private readonly IPaypalClient _paypalClient;

    public CreateOrderHandler(IPaypalClient paypalClient)
    {
        _paypalClient = paypalClient;
    }

    public async Task<PayPalOrderResponse> Handle(CreatePayPalOrderRequest request, CancellationToken ct = default)
    {
        var result =
           await _paypalClient.CreateOrderAsync(new OrderPaymentHttpRequest(request.Currency!, request.Amount, "Authorize"), ct);

        if (!Enum.TryParse<OrderStatus>(result.Status, true, out var orderStatus) &&
            orderStatus == OrderStatus.Created)
            throw new InvalidOperationException("The payment was not created due the issue.");


        return new PayPalOrderResponse
        {
            Status = orderStatus,
            Id = result.Id,
            CreateAt = DateTimeOffset.UtcNow
        };
    }
}

public enum OrderStatus
{
    Created,
    Saved,
    Approved,
    Completed
}