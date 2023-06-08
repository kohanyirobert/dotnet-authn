using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.HttpResults;
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
    public IResult Login([FromBody] LoginDTO login)
    {
        HttpContext.AuthenticateAsync();
        var email = login.Email;
        var password = login.Password;
        if (email != "admin@admin" && password != "admin" && email != "user@user" && password != "user")
        {
            return Results.Unauthorized();
        }

        var validIssuer = _configuration["Authentication:Schemes:Bearer:ValidIssuer"];
        var signingKey = _configuration
            .GetSection("Authentication:Schemes:Bearer:SigningKeys")
            .GetChildren()
            .Single(x => x["Issuer"] == validIssuer);
        var bytes = Convert.FromBase64String(signingKey["Value"]!);
        var key = new SymmetricSecurityKey(bytes);

        var claims = new List<Claim>();
        claims.AddRange(_configuration
            .GetSection("Authentication:Schemes:Bearer:ValidAudiences")
            .Get<string[]>()!
            .Select(x => new Claim(JwtRegisteredClaimNames.Aud, x))
        );
        claims.Add(new Claim(ClaimTypes.Email, email));
        claims.Add(new Claim(ClaimTypes.Role, email == "admin@admin" ? "Admin" : "User"));

        var tokenObject = _handler.CreateToken(new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddMinutes(1),
            Issuer = validIssuer,
            SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
        });
        var tokenString = _handler.WriteToken(tokenObject);
        
        HttpContext.Response.Cookies.Append("jwt", tokenString, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
        });
        return Results.Ok();
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return Ok();
    }
}
