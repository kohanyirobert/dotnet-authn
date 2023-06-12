using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace MvcBasicWithoutIdentity.Services;

public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public BasicAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock) : base(options, logger, encoder, clock)
    {
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!new [] { "/Home/UserSecret", "/Home/AdminSecret" }.Contains(Request.Path.Value)) 
        {
            return Task.FromResult(AuthenticateResult.NoResult());
        }

        var authHeader = Request.Headers["Authorization"].ToString();
        if (authHeader.StartsWith("Basic"))
        {
            var token = authHeader["Basic ".Length..].Trim();
            var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(token)).Split(':');
            var username = credentials[0];
            var password = credentials[1];
            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, username == "user" ? "User" : "Admin"),
            }, Scheme.Name));
            return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name)));
        }

        Response.StatusCode = 401;
        Response.Headers.Add("WWW-Authenticate", "Basic realm=\"www\"");
        return Task.FromResult(AuthenticateResult.Fail("Invalid Authorization Header"));
    }
}
