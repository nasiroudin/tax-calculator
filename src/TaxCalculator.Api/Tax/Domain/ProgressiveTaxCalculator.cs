using System;
using System.Collections.Generic;
using System.Linq;
using TaxCalculator.Api.Tax.Domain.Interfaces;

namespace TaxCalculator.Api.Tax.Domain;

public class ProgressiveTaxCalculator: ITaxCalculator
{
    private readonly TaxBracket[] _taxBrackets = new[]
    {
        new TaxBracket(8350, 0.1M),
        new TaxBracket(25600, 0.15M),
        new TaxBracket(48300, 0.25M),
        new TaxBracket(89300, 0.28M),
        new TaxBracket(201400, 0.33M)
    };
    
    public decimal Calculate(decimal annualIncome)
    {
        if (annualIncome < 0)
            throw new Exception("Annual Income cannot be a negative value");
        
        decimal calculatedTax = 0;

        var totalBracketAmount = _taxBrackets.Sum(x => x.Amount);
        if (annualIncome > totalBracketAmount)
            calculatedTax = (annualIncome - totalBracketAmount) * 0.35M;

        foreach (var bracket in _taxBrackets)
        {
            if (annualIncome > bracket.Amount)
            {
                calculatedTax += bracket.Amount * bracket.Rate;
                annualIncome -= bracket.Amount;
            }
            else
            {
                calculatedTax += annualIncome * bracket.Rate;
                break;
            }
        }
        
        return calculatedTax;
    }
}
