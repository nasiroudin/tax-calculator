using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaxCalculator.Api.Tax.Infrastructure.Persistence.SqlServer.Interfaces;

public interface ITaxDetailStore
{
    Task InsertAsync(TaxDetail taxDetail);
    Task<List<TaxDetail>> GetTaxDetailListAsync();
}