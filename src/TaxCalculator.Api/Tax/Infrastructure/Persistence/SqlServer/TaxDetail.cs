using System;

namespace TaxCalculator.Api.Tax.Infrastructure.Persistence.SqlServer;

public class TaxDetail
{
    public string PostalCode { get; set; }
    public decimal AnnualIncome { get; set; }
    public string TaxCalculationType { get; set; }
    public decimal CalculatedTax { get; set; }
    public DateTime CreatedOn { get; set; }
}