using System.Text.Json.Serialization;

namespace EasyPark.Contracts.Models;

public record PaymentAmount
{
    [JsonPropertyName("currency_code")] public required string CurrencyCode { get; init; }
    public required string Value { get; init; }
}