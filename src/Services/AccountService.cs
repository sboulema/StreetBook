using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;
using StreetBook.Models.ViewModels;

namespace StreetBook.Services;

public interface IAccountService
{
    ClaimsPrincipal? Login(LoginViewModel model);
}

public class AccountService(IConfiguration configuration) : IAccountService
{
    public ClaimsPrincipal? Login(LoginViewModel model)
    {
        if (model.Password != configuration["PASSWORD"])
        {
            return null;
        }
        
        var identity = new ClaimsIdentity(
            new[] { new Claim(ClaimTypes.Name, "Bewoner") },
            CookieAuthenticationDefaults.AuthenticationScheme);

        var claimsPrincipal = new ClaimsPrincipal(identity);

        return claimsPrincipal;
    }
}
