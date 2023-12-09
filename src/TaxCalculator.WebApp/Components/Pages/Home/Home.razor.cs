using System.Net;
using System.Text.Json;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Refit;
using TaxCalculator.Shared.Models.Calculate;
using TaxCalculator.WebApp.Infrastructure.ApiService;

namespace TaxCalculator.WebApp.Components.Pages.Home;

public partial class Home
{
    [Inject] private ITaxApiService TaxApiService { get; set; }
    [Inject] private ILocalStorageService LocalStorageService { get; set; }
    [Inject] private NavigationManager NavigationManager { get; set; }

    private List<TaxDetailTableData> _taxDetailList = [];
    private TaxInput _taxInput = new TaxInput();
    private bool _loading = false;
    private string _calculatedTax;
    private string _error;

    protected override async Task OnInitializedAsync()
    {
        if (await LocalStorageService.ContainKeyAsync("TokenResponse"))
        {
            var tokenResponse = await LocalStorageService.GetItemAsync<TokenResponse>("TokenResponse");
            var apiResponseDetails = await TaxApiService.GetTaxDetailsAsync(tokenResponse.AccessToken);
            if (apiResponseDetails.StatusCode == HttpStatusCode.OK)
            {
                _taxDetailList = apiResponseDetails.Content.TaxDetailList.Select(x => new TaxDetailTableData
                {
                    AnnualIncome = x.AnnualIncome,
                    CalculatedTax = x.CalculatedTax,
                    TaxCalculationType = x.TaxCalculationType,
                    PostalCode = x.PostalCode,
                    CreatedOn = x.CreatedOn
                }).ToList();
            }
            else
            {
                _error = "Internal Server Error: Unable to fetch data to populate tax details table";
            }
        }
        else
        {
            NavigationManager.NavigateTo("/login", true);
        }
    }

    private async Task OnFinish(EditContext editContext)
    {
        _loading = true;

        var tokenResponse = await LocalStorageService.GetItemAsync<TokenResponse>("TokenResponse");
        var apiResponse = await TaxApiService.CalculateTaxAsync(new CalculateTaxRequest
        {
            PostalCode = _taxInput.PostalCode,
            AnnualIncome = _taxInput.AnnualIncome
        }, tokenResponse.AccessToken);

        switch (apiResponse.StatusCode)
        {
            case HttpStatusCode.OK:
                _calculatedTax = $"Calculated Tax is {apiResponse.Content?.TaxAmount.ToString()}";
                _error = null;

                var apiResponseDetails = await TaxApiService.GetTaxDetailsAsync(tokenResponse.AccessToken);
                if (apiResponseDetails.StatusCode == HttpStatusCode.OK)
                {
                    _taxDetailList = apiResponseDetails.Content.TaxDetailList.Select(x => new TaxDetailTableData
                    {
                        AnnualIncome = x.AnnualIncome,
                        CalculatedTax = x.CalculatedTax,
                        TaxCalculationType = x.TaxCalculationType,
                        PostalCode = x.PostalCode,
                        CreatedOn = x.CreatedOn
                    }).ToList();
                }
                else
                {
                    _error = "Internal Server Error";
                }
                
                break;
            case HttpStatusCode.BadRequest:
                var problemDetails = await apiResponse.Error?.GetContentAsAsync<ProblemDetails>()!;
                _error = JsonSerializer.Serialize(problemDetails?.Errors).Replace("\\u0027", "");
                _calculatedTax = null;
                break;
            case HttpStatusCode.InternalServerError:
                _error = "Internal Server Error";
                _calculatedTax = null;
                break;
        }
        
        _loading = false;
    }
}