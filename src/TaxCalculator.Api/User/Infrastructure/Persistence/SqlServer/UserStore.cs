using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using TaxCalculator.Api.User.Infrastructure.Persistence.SqlServer.Interfaces;

namespace TaxCalculator.Api.User.Infrastructure.Persistence.SqlServer;

public class UserStore(IConfiguration configuration) : IUserStore
{
    private readonly string _connectionString = configuration.GetConnectionString("SqlServer");

    public async Task InsertAsync(User user)
    {
        await using var connection = new SqlConnection(_connectionString);
        await connection.ExecuteAsync(
            """
                                
                INSERT INTO [User]
                (
                    Username,
                    PasswordHash,
                    CreatedOn
                )
                VALUES
                (
                    @Username,
                    @PasswordHash,
                    @CreatedOn
                );

            """, user);
    }

    public async Task<User> GetByUsernameAsync(string username)
    {
        await using var connection = new SqlConnection(_connectionString);
        var user = (await connection.QueryAsync<User>(
            """

            SELECT
                Username,
                PasswordHash,
                CreatedOn
            FROM
                [User]
            WHERE
                Username = @Username
                        
            """, new
            {
                Username = username
            })).FirstOrDefault();
        return user;
    }
}