using System.ComponentModel;

namespace TaxCalculator.WebApp.Components.Pages.Home;

public class TaxDetailTableData
{
    [DisplayName("Postal Code")]
    public string PostalCode { get; set; }

    [DisplayName("Annual Income")]
    public decimal AnnualIncome { get; set; }

    [DisplayName("Tax Calculation Type")]
    public string TaxCalculationType { get; set; }

    [DisplayName("Calculated Tax")]
    public decimal CalculatedTax { get; set; }

    [DisplayName("Created On")]
    public DateTime CreatedOn { get; set; }
}