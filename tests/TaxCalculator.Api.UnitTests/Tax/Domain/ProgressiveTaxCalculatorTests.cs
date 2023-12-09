using TaxCalculator.Api.Tax.Domain;
using TaxCalculator.Api.Tax.Domain.Interfaces;

namespace TaxCalculator.Api.UnitTests.Tax.Domain;

public class ProgressiveTaxCalculatorTests
{
    private ITaxCalculator _taxCalculator;
    
    [SetUp]
    public void Setup()
    {
        _taxCalculator = new ProgressiveTaxCalculator();
    }

    [TestCase(2000, 200)]
    [TestCase(8350, 835)]
    [TestCase(9000, 932.5)]
    [TestCase(25600, 3422.5)]
    [TestCase(35000, 4937.5)]
    [TestCase(48300, 8262.5)]
    [TestCase(60000, 11187.5)]
    [TestCase(89300, 18724)]
    [TestCase(100000, 21720)]
    [TestCase(201400, 51604.5)]
    [TestCase(250000, 67642.5)]
    [TestCase(400000, 117683.5)]
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