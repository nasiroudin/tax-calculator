using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using TaxCalculator.Api.Tax.Infrastructure.Persistence.SqlServer.Interfaces;

namespace TaxCalculator.Api.Tax.Infrastructure.Persistence.SqlServer;

public class TaxConfigurationStore(IConfiguration configuration, IMemoryCache memoryCache) : ITaxConfigurationStore
{
    private readonly string _connectionString = configuration.GetConnectionString("SqlServer");

    public async Task<TaxConfiguration> GetTaxConfigurationAsync(string postalCode)
    {
        var taxConfigurations = await memoryCache.GetOrCreateAsync("TaxConfiguration_Cache", async entry =>
        {
            entry.AbsoluteExpiration = DateTimeOffset.Now.Add(TimeSpan.FromHours(2));
            await using var connection = new SqlConnection(_connectionString);
            var taxConfigurations = (await connection.QueryAsync<TaxConfiguration>(
                """

                SELECT
                    PostalCode,
                    TaxCalculationType
                FROM
                    TaxConfiguration
                            
                """)).ToList();
            return taxConfigurations;
        });

        return taxConfigurations.FirstOrDefault(x => x.PostalCode.Equals(postalCode));
    }
}