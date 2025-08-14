using EasyPark.Application.UseCases.Dto;
using EasyPark.Domain;
using FluentValidation;

namespace EasyPark.Application.UseCases.Validators;

public class CreatePayPalOrderRequestValidator : AbstractValidator<CreatePayPalOrderRequest>
{
    public CreatePayPalOrderRequestValidator()
    {
        RuleFor(p => p.Amount).NotNull().PrecisionScale(5, 2, true);
        RuleFor(p => p.Currency).NotEmpty().Must(p => Enum.TryParse<EasyParkCurrencies>(p, true, out _));
    }
}