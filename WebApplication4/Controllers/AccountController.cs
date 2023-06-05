using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication4.Models;

namespace WebApplication4.Controllers;

public class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;

    public AccountController(ILogger<AccountController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ActionName("Login")]
    public async Task<IActionResult> PostLogin(LoginViewModel model)
    {
        if (model is not ({Email: "admin@admin", Password: "admin"} or {Email: "user@user", Password: "user"}))
        {
            ModelState.AddModelError("InvalidLogin", "Invalid login credentials");
            return View();
        }

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(new ClaimsIdentity(
                new[]
                {
                    new Claim(ClaimTypes.Email, model.Email),
                    new Claim(ClaimTypes.Role, model.Email == "admin@admin" ? "Admin" : "User"),
                }
                , CookieAuthenticationDefaults.AuthenticationScheme
            )),
            new AuthenticationProperties()
        );
        return Redirect("/");
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return Redirect("/");
    }
}
