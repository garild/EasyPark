namespace EasyPark.Contracts.Models;

public record OrderPaymentHttpRequest(string Currency, decimal Amount, string PaymentIntent);