namespace TaxCalculator.WebApp.Infrastructure.ApiService.Models;

public class TaxDetail
{
    public string PostalCode { get; set; }
    public decimal AnnualIncome { get; set; }
    public string TaxCalculationType { get; set; }
    public decimal CalculatedTax { get; set; }
    public DateTime CreatedOn { get; set; }
}