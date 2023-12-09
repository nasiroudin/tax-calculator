using System;
using Microsoft.Extensions.DependencyInjection;
using TaxCalculator.Api.Tax.Domain.Enums;
using TaxCalculator.Api.Tax.Domain.Interfaces;

namespace TaxCalculator.Api.Tax.Domain;

public class TaxCalculatorFactory(IServiceProvider serviceProvider) : ITaxCalculatorFactory
{
    public ITaxCalculator GetTaxCalculator(TaxCalculationType type)
    {
        return type switch
        {
            TaxCalculationType.Progressive => serviceProvider.GetService<ProgressiveTaxCalculator>(),
            TaxCalculationType.FlatRate => serviceProvider.GetService<FlatRateTaxCalculator>(),
            TaxCalculationType.FlatValue => serviceProvider.GetService<FlatValueTaxCalculator>(),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, "Unknown Tax Calculation Type")
        };
    }
}