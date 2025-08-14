using System.ComponentModel.DataAnnotations;

namespace EasyPark.Infrastructure.Options;

public record PayPalOptions
{
    public const string SectionName = "PayPal";
    
    [Required]
    public string? ClientId { get; init; }
    [Required]
    public string? ClientSecret { get; init; }
    [Required]
    public string? Environment { get; init; }
}