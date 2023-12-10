using System;
using System.Linq;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Serilog;
using TaxCalculator.Api.Tax.Calculate;
using TaxCalculator.Api.Tax.Details;
using TaxCalculator.Api.Tax.Infrastructure.Persistence.SqlServer.Interfaces;

namespace TaxCalculator.Api.Tax;

public class TaxModule(ILogger logger, ITaxDetailStore taxDetailStore): ICarterModule
{
    private readonly ILogger _logger = logger.ForContext<TaxModule>();
    
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/tax/calculate",
                async (CalculateTaxRequest request, IMediator mediator) => await mediator.Send(request))
            .RequireAuthorization();
        
        app.MapGet("api/tax/details", async () =>
            {
                try
                {
                    var taxDetailList = await taxDetailStore.GetTaxDetailListAsync();
                    var taxDetailsResponse = new TaxDetailsResponse
                    {
                        TaxDetailList = taxDetailList.OrderByDescending(x => x.CreatedOn).Select(x => new TaxDetail
                        {
                            PostalCode = x.PostalCode,
                            AnnualIncome = x.AnnualIncome,
                            CalculatedTax = x.CalculatedTax,
                            TaxCalculationType = x.TaxCalculationType,
                            CreatedOn = x.CreatedOn
                        }).ToList()
                    };
                    
                    return Results.Ok(taxDetailsResponse);
                }
                catch (Exception e)
                {
                    _logger
                        .Error(e, "Error occurred while retrieving tax details: {ErrorMessage}", e.Message);
            
                    return Results.StatusCode(StatusCodes.Status500InternalServerError);
                }
            })
            .RequireAuthorization();
    }
}