namespace TaxCalculator.Api.Tax.Domain.Interfaces;

public interface ITaxCalculator
{
    decimal Calculate(decimal annualIncome);
}