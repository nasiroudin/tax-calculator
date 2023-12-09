using System.Text.RegularExpressions;

namespace TaxCalculator.Api.Extensions;

public static class StringExtensions
{
    /// <summary>
    /// Check whether a given string contains only alphabets (A-Z or a-z) or numbers (0-9)
    /// </summary>
    /// <param name="strToCheck">Input</param>
    /// <returns>Either true or false</returns>
    public static bool IsAlphaNumeric(this string strToCheck)
    {
        if (string.IsNullOrWhiteSpace(strToCheck))
            return false;
        
        var rg = new Regex(@"^[a-zA-Z0-9\s,]*$");
        return rg.IsMatch(strToCheck);
    }
}