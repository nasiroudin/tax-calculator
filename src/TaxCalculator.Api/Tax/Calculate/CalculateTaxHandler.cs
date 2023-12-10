using System;
using System.Threading;
using System.Threading.Tasks;
using Carter.ModelBinding;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Serilog;
using TaxCalculator.Api.Tax.Domain.Enums;
using TaxCalculator.Api.Tax.Domain.Interfaces;
using TaxCalculator.Api.Tax.Infrastructure.Persistence.SqlServer;
using TaxCalculator.Api.Tax.Infrastructure.Persistence.SqlServer.Interfaces;

namespace TaxCalculator.Api.Tax.Calculate;

public class CalculateTaxHandler(
    IValidator<CalculateTaxRequest> validator, 
    ITaxCalculatorFactory taxCalculatorFactory,
    ITaxConfigurationStore taxConfigurationStore,
    ITaxDetailStore taxDetailStore,
    ILogger logger): IRequestHandler<CalculateTaxRequest, IResult>
{
    private readonly ILogger _logger = logger.ForContext<CalculateTaxHandler>();
    
    public async Task<IResult> Handle(CalculateTaxRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Results.ValidationProblem(validationResult.GetValidationProblems());

            var taxConfiguration = await taxConfigurationStore.GetTaxConfigurationAsync(request.PostalCode);
            if (taxConfiguration == null)
            {
                return Results.Problem(
                    title: "Business Error",
                    detail: "Invalid PostalCode",
                    statusCode: StatusCodes.Status400BadRequest
                );
            }

            var taxCalculationType = Enum.Parse<TaxCalculationType>(taxConfiguration.TaxCalculationType, true);
            var taxCalculator = taxCalculatorFactory.GetTaxCalculator(taxCalculationType);
            var taxAmount = taxCalculator.Calculate(request.AnnualIncome);

            await taxDetailStore.InsertAsync(new TaxDetail
            {
                PostalCode = request.PostalCode,
                AnnualIncome = request.AnnualIncome,
                CalculatedTax = taxAmount,
                TaxCalculationType = taxConfiguration.TaxCalculationType,
                CreatedOn = DateTime.Now
            });

            return Results.Ok(new CalculateTaxResponse
            {
                TaxAmount = taxAmount
            });
        }
        catch (Exception e)
        {
            _logger
                .ForContext("CalculateTaxRequest", request, true)
                .Error(e, "Error occurred while calculating tax: {ErrorMessage}", e.Message);
            
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}