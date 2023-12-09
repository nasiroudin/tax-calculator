using System.Text.Json;
using System.Text.Json.Serialization;
using Carter;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using TaxCalculator.Api.Tax.Domain;
using TaxCalculator.Api.Tax.Domain.Interfaces;
using TaxCalculator.Api.Tax.Infrastructure.Persistence.SqlServer;
using TaxCalculator.Api.Tax.Infrastructure.Persistence.SqlServer.Interfaces;
using TaxCalculator.Api.User.Infrastructure.Persistence.SqlServer;
using TaxCalculator.Api.User.Infrastructure.Persistence.SqlServer.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc) => lc
    .Enrich.FromLogContext()
    .ReadFrom.Configuration(ctx.Configuration)
);

var assembly = typeof(Program).Assembly;

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication().AddBearerToken();
builder.Services.AddAuthorization();

builder.Services.AddCarter(); // Use for automatic Minimal API Endpoint registration
builder.Services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(assembly)); // Register Handlers
builder.Services.AddValidatorsFromAssembly(assembly);;
builder.Services.AddMemoryCache();
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
});

builder.Services.AddTransient<ProgressiveTaxCalculator>();
builder.Services.AddTransient<FlatValueTaxCalculator>();
builder.Services.AddTransient<FlatRateTaxCalculator>();
builder.Services.AddTransient<ITaxCalculatorFactory, TaxCalculatorFactory>();
builder.Services.AddTransient<ITaxConfigurationStore, TaxConfigurationStore>();
builder.Services.AddTransient<ITaxDetailStore, TaxDetailStore>();
builder.Services.AddTransient<IUserStore, UserStore>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapCarter();


await app.RunAsync();