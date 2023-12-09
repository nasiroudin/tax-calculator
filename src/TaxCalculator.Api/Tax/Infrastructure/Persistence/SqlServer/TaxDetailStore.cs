using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using TaxCalculator.Api.Tax.Infrastructure.Persistence.SqlServer.Interfaces;

namespace TaxCalculator.Api.Tax.Infrastructure.Persistence.SqlServer;

public class TaxDetailStore(IConfiguration configuration) : ITaxDetailStore
{
    private readonly string _connectionString = configuration.GetConnectionString("SqlServer");
    
    public async Task InsertAsync(TaxDetail taxDetail)
    {
        await using var connection = new SqlConnection(_connectionString);
        await connection.ExecuteAsync(
            """
                                
                INSERT INTO TaxDetail 
                (
                    PostalCode,
                    AnnualIncome,
                    TaxCalculationType,
                    CalculatedTax,
                    CreatedOn
                )
                VALUES
                (
                    @PostalCode,
                    @AnnualIncome,
                    @TaxCalculationType,
                    @CalculatedTax,
                    @CreatedOn
                );
            
            """, taxDetail);
    }

    public async Task<List<TaxDetail>> GetTaxDetailListAsync()
    {
        await using var connection = new SqlConnection(_connectionString);
        var taxDetailList = (await connection.QueryAsync<TaxDetail>(
            """

            SELECT
                PostalCode,
                AnnualIncome,
                TaxCalculationType,
                CalculatedTax,
                CreatedOn
            FROM
                TaxDetail
                        
            """)).ToList();
        return taxDetailList;
    }
}