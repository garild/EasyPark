using System.ComponentModel.DataAnnotations;
using EasyPark.Domain;

namespace EasyPark.Application.UseCases.Dto;

//TODO: Validation model int .net is flunky - impl. fluent validation
public class CreatePayPalOrderRequest
{
    /// <summary>
    /// The amount to charge for the order
    /// </summary>
    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
    public decimal Amount { get; set; }

    /// <summary>
    /// The currency code for the payment (e.g., USD, EUR, GBP)
    /// </summary>
    [Required]
    [EnumDataType(typeof(EasyParkCurrencies))]
    public string? Currency { get; set; }
}