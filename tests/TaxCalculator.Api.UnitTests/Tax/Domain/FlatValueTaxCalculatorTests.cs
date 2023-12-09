using TaxCalculator.Api.Tax.Domain;
using TaxCalculator.Api.Tax.Domain.Interfaces;

namespace TaxCalculator.Api.UnitTests.Tax.Domain;

public class FlatValueTaxCalculatorTests
{
    private ITaxCalculator _taxCalculator;
    
    [SetUp]
    public void Setup()
    {
        _taxCalculator = new FlatValueTaxCalculator();
    }

    [TestCase(10000, 500)]
    [TestCase(200000, 10000)]
    [TestCase(200001, 10000)]
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