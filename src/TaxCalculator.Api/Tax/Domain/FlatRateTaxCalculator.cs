using System;
using TaxCalculator.Api.Tax.Domain.Interfaces;

namespace TaxCalculator.Api.Tax.Domain;

public class FlatRateTaxCalculator: ITaxCalculator
{
    private const decimal TaxRate = 0.175M;
    
    public decimal Calculate(decimal annualIncome)
    {
        return annualIncome switch
        {
            < 0 => throw new Exception("Annual Income cannot be a negative value"),
            _ => annualIncome * TaxRate
        };
    }
}