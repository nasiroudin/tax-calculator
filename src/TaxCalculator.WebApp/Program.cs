using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Components;
using AntDesign.ProLayout;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Refit;
using Serilog;
using TaxCalculator.WebApp.Infrastructure.ApiService;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc) => lc
    .Enrich.FromLogContext()
    .ReadFrom.Configuration(ctx.Configuration)
);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddAntDesign();
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(sp.GetService<NavigationManager>()!.BaseUri)
});
builder.Services.Configure<ProSettings>(builder.Configuration.GetSection("ProSettings"));
builder.Services.AddBlazoredLocalStorage();
builder.Services.Configure<RazorPagesOptions>(options => options.RootDirectory = "/Components/Pages");

builder.Services
    .AddRefitClient<ITaxApiService>(new RefitSettings
    {
        ContentSerializer = new SystemTextJsonContentSerializer(new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
            Converters = { new JsonStringEnumConverter() }
        })
    })
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration["TaxCalculatorApiEndpoint"]!));

builder.Services
    .AddRefitClient<IUserApiService>(new RefitSettings
    {
        ContentSerializer = new SystemTextJsonContentSerializer(new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
            Converters = { new JsonStringEnumConverter() }
        })
    })
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration["TaxCalculatorApiEndpoint"]!));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();