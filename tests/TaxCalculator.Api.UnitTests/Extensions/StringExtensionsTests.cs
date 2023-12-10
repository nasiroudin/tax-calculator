using TaxCalculator.Api.Extensions;

namespace TaxCalculator.Api.UnitTests.Extensions;

public class StringExtensionsTests
{
    [TestCase("AbC123", true)]
    [TestCase("abc123@", false)]
    [TestCase(null, false)]
    [TestCase("", false)]
    [TestCase("   ", false)]
    [TestCase("Hello, World 123", true)]
    public void GivenAStringValue_ThenCheckIfAlphanumeric(string stringValue, bool expected)
    {
        var isAlphaNumeric = stringValue.IsAlphaNumeric();
        Assert.That(expected, Is.EqualTo(isAlphaNumeric));
    }
}