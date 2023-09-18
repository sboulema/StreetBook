﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using StreetBook.Services;
using StreetBook.Models.ViewModels;

namespace StreetBook.Controllers;

[Route("[controller]")]
public class AccountController : Controller
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpGet("[action]")]
    public IActionResult Login() => View(new LoginViewModel());

    [HttpPost("[action]")]
    public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = "")
    {
        var claimsPrincipal = _accountService.Login(model);

        if (claimsPrincipal == null)
        {
            TempData["notificationMessage"] = "Password is incorrect.";
            TempData["notificationType"] = "danger";

            return RedirectToAction("Login");
        }

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

        return RedirectToLocal(returnUrl);
    }

    [Authorize]
    [HttpGet("[action]")]
    public async Task Logout() => await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

    private IActionResult RedirectToLocal(string returnUrl)
    {
        if (Url.IsLocalUrl(returnUrl))
        {
            return Redirect(returnUrl);
        }
        else
        {
            return RedirectToAction("Index", "Home");
        }
    }
}