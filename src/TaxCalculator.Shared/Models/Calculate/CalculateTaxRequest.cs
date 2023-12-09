using MediatR;
using Microsoft.AspNetCore.Http;

namespace TaxCalculator.Shared.Models.Calculate;

public class CalculateTaxRequest : IRequest<IResult>
{
    public string PostalCode { get; set; }
    public decimal AnnualIncome { get; set; }
}