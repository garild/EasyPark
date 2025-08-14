namespace EasyPark.Infrastructure.Paypal;

public static class PayPalRouteV2
{
    public const string CreateOrder = "/v2/checkout/orders";
    public const string ConfirmOrder = "{0}/{1}/confirm-payment-source";
}