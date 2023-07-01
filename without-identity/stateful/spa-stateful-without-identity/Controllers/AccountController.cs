using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace SpaStatefulWithoutIdentity.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly ILogger<AccountController> _logger;

    public AccountController(ILogger<AccountController> logger)
    {
        _logger = logger;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromForm]string email, [FromForm]string password)
    {
        if (email != "admin@admin" && password != "admin" && email != "user@user" && password != "user")
        {
            return Unauthorized();
        }

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(new ClaimsIdentity(
                new[]
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.Role, email == "admin@admin" ? "Admin" : "User"),
                }
                , CookieAuthenticationDefaults.AuthenticationScheme
            )),
            new AuthenticationProperties()
        );
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return Ok();
    }
}
