using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace WebApplication10.Services;

public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private SignInManager<IdentityUser> _signInManager;

    public BasicAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        SignInManager<IdentityUser> signInManager) : base(options, logger, encoder, clock)
    {
        _signInManager = signInManager;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!new [] { "/Home/UserSecret", "/Home/AdminSecret" }.Contains(Request.Path.Value)) 
        {
            return await Task.FromResult(AuthenticateResult.NoResult());
        }

        var authHeader = Request.Headers["Authorization"].ToString();
        if (authHeader.StartsWith("Basic"))
        {
            var token = authHeader["Basic ".Length..].Trim();
            var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(token)).Split(':');
            var username = credentials[0];
            var password = credentials[1];

            var user = await _signInManager.UserManager.FindByEmailAsync(username);
            var result = await _signInManager.CheckPasswordSignInAsync(user!, password, false);
            if (result.Succeeded)
            {
                var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(user);
                return await Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name)));
            }
        }

        Response.StatusCode = 401;
        Response.Headers.Add("WWW-Authenticate", "Basic realm=\"www\"");
        return await Task.FromResult(AuthenticateResult.Fail("Invalid Authorization Header"));
    }
}
