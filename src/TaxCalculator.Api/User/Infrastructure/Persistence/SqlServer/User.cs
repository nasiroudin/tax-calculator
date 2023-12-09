using System;

namespace TaxCalculator.Api.User.Infrastructure.Persistence.SqlServer;

public class User
{
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public DateTime CreatedOn { get; set; }
}