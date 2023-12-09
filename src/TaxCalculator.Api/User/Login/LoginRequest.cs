using MediatR;
using Microsoft.AspNetCore.Http;

namespace TaxCalculator.Api.User.Login;

public class LoginRequest : IRequest<IResult>
{
    public string Username { get; set; }
    public string Password { get; set; }
}