using System.Globalization;
using EasyPark.Contracts;
using EasyPark.Contracts.Models;
using Microsoft.Extensions.Logging;
using PaypalServerSdk.Standard;
using PaypalServerSdk.Standard.Models;

namespace EasyPark.Infrastructure.Paypal;

public class PayPalClient : IPaypalClient
{
    private readonly PaypalServerSdkClient _client;
    private readonly ILogger<PayPalClient> _logger;

    // ReSharper disable once ConvertToPrimaryConstructor
    public PayPalClient(PaypalServerSdkClient client, ILogger<PayPalClient> logger)
    {
        _client = client;
        _logger = logger;
    }

    public async Task<OrderPaymentHttpResponse> CreateOrderAsync(OrderPaymentHttpRequest request, CancellationToken ct = default)
    {
        try
        {
            if(!Enum.TryParse(request.PaymentIntent, true, out CheckoutPaymentIntent intent))
                throw new ArgumentException($"Intent {request.PaymentIntent} is not valid type.");
            
            var ordersController = _client.OrdersController;

            var createOrder = new CreateOrderInput
            {
                ContentType = "application/json",
                Body = new OrderRequest
                {
                    Intent = CheckoutPaymentIntent.Authorize,
                    PurchaseUnits =
                    [
                        new PurchaseUnitRequest
                        {
                            Amount = new AmountWithBreakdown
                            {
                                CurrencyCode = request.Currency,
                                MValue = request.Amount.ToString("F2", new NumberFormatInfo()),
                            },
                            ReferenceId = "PUHF",
                            Payee = new PayeeBase
                            {
                                EmailAddress = "merchant@example.com",
                            },
                            Shipping = new ShippingDetails
                            {
                                Type = FulfillmentType.PickupInStore,
                                Address = new Address
                                {
                                    CountryCode = "US",
                                    AddressLine1 = "123 Townsend St",
                                    AddressLine2 = "Floor 6",
                                    AdminArea2 = "San Francisco",
                                    AdminArea1 = "CA",
                                    PostalCode = "94107",
                                }
                            }
                        }
                    ],
                    ApplicationContext = new OrderApplicationContext()
                    {
                        ShippingPreference = OrderApplicationContextShippingPreference.NoShipping
                    }
                }
            };

            var result = await ordersController.CreateOrderAsync(createOrder, ct);
            return new OrderPaymentHttpResponse(result.Data.Id, result.Data.Status.ToString()!);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }
}