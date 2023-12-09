using System.Net;
using System.Text.Json;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Refit;
using TaxCalculator.Shared.Models.User;
using TaxCalculator.WebApp.Infrastructure.ApiService;

namespace TaxCalculator.WebApp.Components.Pages.User;

public partial class Login
{
    [Inject] private IUserApiService UserApiService { get; set; }
    [Inject] private ILocalStorageService LocalStorageService { get; set; }
    [Inject] private NavigationManager NavigationManager { get; set; }
    
    private readonly LoginModel _model = new LoginModel();
    private bool _loading = false;
    private string _error;

    protected override async Task OnInitializedAsync()
    {
        if (await LocalStorageService.ContainKeyAsync("TokenResponse"))
        {
            NavigationManager.NavigateTo("/");
        }
    }

    public async Task HandleSubmit()
    {
        _loading = true;

        var apiResponse = await UserApiService.LoginAsync(new LoginRequest
        {
            Username = _model.Username,
            Password = _model.Password
        });
        
        switch (apiResponse.StatusCode)
        {
            case HttpStatusCode.OK:
                _error = null;
                await LocalStorageService.ClearAsync();
                await LocalStorageService.SetItemAsync("TokenResponse", apiResponse.Content);
                NavigationManager.NavigateTo("/");
                break;
            case HttpStatusCode.BadRequest:
                var problemDetails = await apiResponse.Error?.GetContentAsAsync<ProblemDetails>()!;
                _error = JsonSerializer.Serialize(problemDetails?.Errors).Replace("\\u0027", "");
                break;
            case HttpStatusCode.InternalServerError:
                _error = "Internal Server Error";
                break;
        }
        
        _loading = false;
    }
}

public class LoginModel
{
    public string Username { get; set; }
    public string Password { get; set; }
}