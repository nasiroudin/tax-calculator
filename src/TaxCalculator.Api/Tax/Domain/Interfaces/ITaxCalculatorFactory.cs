using TaxCalculator.Api.Tax.Domain.Enums;

namespace TaxCalculator.Api.Tax.Domain.Interfaces;

public interface ITaxCalculatorFactory
{
    ITaxCalculator GetTaxCalculator(TaxCalculationType type);
}