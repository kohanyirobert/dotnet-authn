using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiBasicWithoutIdentity.Controllers;

[ApiController]
[Route("[controller]")]
public class HomeController : ControllerBase
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpGet("Index")]
    public string Index()
    {
        return "Everybody can see this without login.";
    }

    [HttpGet("UserSecret")]
    [Authorize]
    public string UserSecret()
    {
        return "Users and Admins can see this after login.";
    }

    [HttpGet("AdminSecret")]
    [Authorize(Roles = "Admin")]
    public string AdminSecret()
    {
        return "Only Admins can see this after login.";
    }
}
