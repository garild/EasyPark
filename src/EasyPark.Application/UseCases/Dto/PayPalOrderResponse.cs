using System.Text.Json.Serialization;

namespace EasyPark.Application.UseCases.Dto;

public class PayPalOrderResponse
{
    public required OrderStatus Status { get; init; }

    public required string Id { get; init; }
    
    [JsonPropertyName("create_time")]
    public DateTimeOffset CreateAt { get; init; }
}