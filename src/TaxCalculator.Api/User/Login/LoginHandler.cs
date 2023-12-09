using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Carter.ModelBinding;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Http;
using Serilog;
using TaxCalculator.Api.User.Infrastructure.Persistence.SqlServer.Interfaces;

namespace TaxCalculator.Api.User.Login;

public class LoginHandler(
    IValidator<LoginRequest> validator, 
    ILogger logger, 
    IUserStore userStore): IRequestHandler<LoginRequest, IResult>
{
    private readonly ILogger _logger = logger.ForContext<LoginHandler>();
    
    public async Task<IResult> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Results.ValidationProblem(validationResult.GetValidationProblems());
            var user = await userStore.GetByUsernameAsync(request.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return Results.Problem(
                    title: "Business Error",
                    detail: "Invalid credentials",
                    statusCode: StatusCodes.Status400BadRequest
                );
            }
            
            var claimsPrincipal = new ClaimsPrincipal(
                new ClaimsIdentity(
                    new[]
                    {
                        new Claim(ClaimTypes.Name, request.Username)
                    },
                    BearerTokenDefaults.AuthenticationScheme
                )
            );

            return Results.SignIn(claimsPrincipal);
        }
        catch (Exception e)
        {
            _logger
                .Error("Error occurred while retrieving tax details: {ErrorMessage}", e.Message);
            
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}