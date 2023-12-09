using Refit;
using TaxCalculator.Shared.Models.User;

namespace TaxCalculator.WebApp.Infrastructure.ApiService;

public interface IUserApiService
{
    [Post("/api/user/login")]
    Task<ApiResponse<TokenResponse>> LoginAsync(LoginRequest loginRequest);
}