using System.Threading.Tasks;

namespace TaxCalculator.Api.Tax.Infrastructure.Persistence.SqlServer.Interfaces;

public interface ITaxConfigurationStore
{
    Task<TaxConfiguration> GetTaxConfigurationAsync(string postalCode);
}