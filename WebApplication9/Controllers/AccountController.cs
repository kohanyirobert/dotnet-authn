using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApplication9.Models;

namespace WebApplication9.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly ILogger<AccountController> _logger;
    private readonly IConfiguration _configuration;
    private readonly JwtSecurityTokenHandler _handler;

    public AccountController(ILogger<AccountController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
        _handler = new JwtSecurityTokenHandler();
    }

    [HttpPost("Login")]
    public Task<IActionResult> Login([FromBody] LoginDTO login)
    {
        var email = login.Email;
        var password = login.Password;
        if (email != "admin@admin" && password != "admin" && email != "user@user" && password != "user")
        {
            return Task.FromResult<IActionResult>(Unauthorized());
        }

        var tokenObject = _handler.CreateToken(new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
                new[]
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.Role, email == "admin@admin" ? "Admin" : "User"),
                }
            ),
            Expires = DateTime.Now.AddMinutes(1),
            Issuer = _configuration["Authentication:Schemes:Bearer:ValidIssuer"],
            Audience = _configuration["Authentication:Schemes:Bearer:ValidAudiences:0"],
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey("SigningKeysSigningKeysSigningKeysSigningKeys"u8.ToArray()),
                SecurityAlgorithms.HmacSha256
            )
        });
        var tokenString = _handler.WriteToken(tokenObject);
        return Task.FromResult<IActionResult>(Ok(new {token = tokenString}));
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return Ok();
    }
}
