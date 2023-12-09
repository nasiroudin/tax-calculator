using FluentValidation;
using TaxCalculator.Api.Extensions;
using TaxCalculator.Shared.Models.Calculate;

namespace TaxCalculator.Api.Tax.Calculate;

public class CalculateTaxValidator: AbstractValidator<CalculateTaxRequest>
{
    public CalculateTaxValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.AnnualIncome)
            .GreaterThan(0);

        RuleFor(x => x.PostalCode)
            .Length(4)
            .Must(x => x.IsAlphaNumeric()).WithMessage("Postal code should be contains only alphanumeric (A-Z, a-z, 0-9)");
    }
}