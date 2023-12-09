using Refit;
using TaxCalculator.WebApp.Infrastructure.ApiService.Models;

namespace TaxCalculator.WebApp.Infrastructure.ApiService;

public interface ITaxApiService
{
    [Post("/api/tax/calculate")]
    [Headers("Authorization: Bearer")]
    Task<ApiResponse<CalculateTaxResponse>> CalculateTaxAsync(CalculateTaxRequest calculateTaxRequest, [Authorize("Bearer")] string token);
    
    [Get("/api/tax/details")]
    Task<ApiResponse<TaxDetailsResponse>> GetTaxDetailsAsync([Authorize("Bearer")] string token);
}