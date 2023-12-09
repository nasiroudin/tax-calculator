using MediatR;
using Microsoft.AspNetCore.Http;

namespace TaxCalculator.Shared.Models.User;

public class LoginRequest : IRequest<IResult>
{
    public string Username { get; set; }
    public string Password { get; set; }
}