using MediatR;
using Microsoft.AspNetCore.Http;

namespace TaxCalculator.Api.Tax.Calculate;

public class CalculateTaxRequest : IRequest<IResult>
{
    public string PostalCode { get; set; }
    public decimal AnnualIncome { get; set; }
}