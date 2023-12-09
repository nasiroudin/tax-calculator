namespace TaxCalculator.WebApp.Infrastructure.ApiService.Models;

public class CalculateTaxRequest
{
    public string PostalCode { get; set; }
    public decimal AnnualIncome { get; set; }
}