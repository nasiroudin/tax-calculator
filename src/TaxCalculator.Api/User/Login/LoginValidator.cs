﻿using FluentValidation;
using TaxCalculator.Shared.Models.User;

namespace TaxCalculator.Api.User.Login;

public class LoginValidator: AbstractValidator<LoginRequest>
{
    public LoginValidator()
    {
        RuleFor(x => x.Username)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Password)
            .NotNull()
            .NotEmpty();
    }
}