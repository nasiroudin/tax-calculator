using Refit;
using TaxCalculator.WebApp.Infrastructure.ApiService.Models;

namespace TaxCalculator.WebApp.Infrastructure.ApiService;

public interface IUserApiService
{
    [Post("/api/user/login")]
    Task<ApiResponse<TokenResponse>> LoginAsync(LoginRequest loginRequest);
}