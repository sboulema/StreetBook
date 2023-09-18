using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;
using StreetBook.Models.ViewModels;

namespace StreetBook.Services;

public interface IAccountService
{
    ClaimsPrincipal? Login(LoginViewModel model);
}

public class AccountService : IAccountService
{
    private readonly string? _password;

    public AccountService(IConfiguration configuration)
    {
        _password = configuration["PASSWORD"];
    }

    public ClaimsPrincipal? Login(LoginViewModel model)
    {
        if (model.Password != _password)
        {
            return null;
        }
        
        var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

        var claimsPrincipal = new ClaimsPrincipal(identity);

        return claimsPrincipal;
    }
}
