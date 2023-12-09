using TaxCalculator.Api.Tax.Domain;
using TaxCalculator.Api.Tax.Domain.Interfaces;

namespace TaxCalculator.Api.UnitTests.Tax.Domain;

public class FlatRateTaxCalculatorTests
{
    private ITaxCalculator _taxCalculator;
    
    [SetUp]
    public void Setup()
    {
        _taxCalculator = new FlatRateTaxCalculator();
    }

    [TestCase(10000, 1750)]
    [TestCase(1, 0.175)]
    [TestCase(0, 0)]
    [TestCase(2022, 353.85)]
    [TestCase(123, 21.525)]
    [TestCase(9004, 1575.7)]
    [TestCase(10005, 1750.875)]
    [TestCase(89006, 15576.050)]
    [TestCase(34007, 5951.225)]
    [TestCase(898, 157.15)]
    [TestCase(109, 19.075)]
    [TestCase(10000.23, 1750.04025)]
    public void GivenAnAnnualIncome_ThenReturnsCalculatedTax(decimal annualIncome, decimal expectedTaxCalculated)
    {
        var calculatedTax = _taxCalculator.Calculate(annualIncome);
        Assert.That(expectedTaxCalculated, Is.EqualTo(calculatedTax));
    }
    
    [TestCase(-1)]
    public void GivenANegativeAnnualIncome_ThenThrowException(decimal annualIncome)
    {
        Assert.Throws(Is.TypeOf<Exception>()
                .And.Message.EqualTo("Annual Income cannot be a negative value"),
            () =>
            {
                _taxCalculator.Calculate(annualIncome); 
            });
    }

    [TearDown]
    public void TearDown()
    {
        _taxCalculator = null;
    }
}