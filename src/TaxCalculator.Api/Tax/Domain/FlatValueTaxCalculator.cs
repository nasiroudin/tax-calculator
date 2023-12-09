using System;
using TaxCalculator.Api.Tax.Domain.Interfaces;

namespace TaxCalculator.Api.Tax.Domain;

public class FlatValueTaxCalculator: ITaxCalculator
{
    private const decimal TaxRate = 0.05M;
    
    public decimal Calculate(decimal annualIncome)
    {
        return annualIncome switch
        {
            < 0 => throw new Exception("Annual Income cannot be a negative value"),
            <= 200000 => annualIncome * TaxRate,
            _ => 10000
        };
    }
}