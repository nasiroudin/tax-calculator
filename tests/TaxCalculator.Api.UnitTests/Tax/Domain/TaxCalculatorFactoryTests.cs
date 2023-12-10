using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using TaxCalculator.Api.Tax.Domain;
using TaxCalculator.Api.Tax.Domain.Enums;

namespace TaxCalculator.Api.UnitTests.Tax.Domain;

public class TaxCalculatorFactoryTests
{
    [Test]
    public void GetTaxCalculator_ProgressiveType_ReturnsProgressiveTaxCalculator()
    {
        var serviceProvider = Substitute.For<IServiceProvider>();
        var progressiveTaxCalculator = Substitute.For<ProgressiveTaxCalculator>();
        serviceProvider.GetService<ProgressiveTaxCalculator>().Returns(progressiveTaxCalculator);
        var factory = new TaxCalculatorFactory(serviceProvider);
        var result = factory.GetTaxCalculator(TaxCalculationType.Progressive);
        Assert.That(result, Is.InstanceOf<ProgressiveTaxCalculator>());
    }

    [Test]
    public void GetTaxCalculator_FlatRateType_ReturnsFlatRateTaxCalculator()
    {
        var serviceProvider = Substitute.For<IServiceProvider>();
        var flatRateTaxCalculator = Substitute.For<FlatRateTaxCalculator>();
        serviceProvider.GetService<FlatRateTaxCalculator>().Returns(flatRateTaxCalculator);
        var factory = new TaxCalculatorFactory(serviceProvider);
        var result = factory.GetTaxCalculator(TaxCalculationType.FlatRate);
        Assert.That(result, Is.InstanceOf<FlatRateTaxCalculator>());
    }

    [Test]
    public void GetTaxCalculator_FlatValueType_ReturnsFlatValueTaxCalculator()
    {
        var serviceProvider = Substitute.For<IServiceProvider>();
        var flatValueTaxCalculator = Substitute.For<FlatValueTaxCalculator>();
        serviceProvider.GetService<FlatValueTaxCalculator>().Returns(flatValueTaxCalculator);
        var factory = new TaxCalculatorFactory(serviceProvider);
        var result = factory.GetTaxCalculator(TaxCalculationType.FlatValue);
        Assert.That(result, Is.InstanceOf<FlatValueTaxCalculator>());
    }

    [Test]
    public void GetTaxCalculator_UnknownType_ThrowsException()
    {
        var serviceProvider = Substitute.For<IServiceProvider>();
        var factory = new TaxCalculatorFactory(serviceProvider);
        Assert.Throws<ArgumentOutOfRangeException>(() => factory.GetTaxCalculator((TaxCalculationType)999));
    }
}