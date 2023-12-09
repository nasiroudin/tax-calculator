using System.Threading.Tasks;

namespace TaxCalculator.Api.User.Infrastructure.Persistence.SqlServer.Interfaces;

public interface IUserStore
{
    Task InsertAsync(User user);
    Task<User> GetByUsernameAsync(string username);
}