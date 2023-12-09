using System.ComponentModel.DataAnnotations;

namespace TaxCalculator.WebApp.Components.Pages.Home
{
    public class TaxInput
    {
        [Required] public string PostalCode { get; set; } 
        [Required] public decimal AnnualIncome { get; set; }
    }
}
