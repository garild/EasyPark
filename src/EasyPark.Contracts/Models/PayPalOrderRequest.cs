using System.Text.Json.Serialization;

namespace EasyPark.Contracts.Models;

public record PayPalOrderRequest
{
    public required string Intent { get; init; }
    [JsonPropertyName("purchase_units")] public required PurchaseUnit[] PurchaseUnits { get; set; } = [];
}