using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using TaxCalculator.Shared.Models.User;

namespace TaxCalculator.Api.User;

public class UserModule: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/user/login", async (LoginRequest request, IMediator mediator) => await mediator.Send(request));
    }
}