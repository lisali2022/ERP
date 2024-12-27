using System.Security.Claims;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

using OneAuth.BlazorServerDemo.Models;

namespace OneAuth.BlazorServerDemo.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    [HttpPost("sign-in")]
    public async Task<IActionResult> SignInAsync([FromForm]LoginViewModel model,[FromQuery]string? returnUrl)
    {
        var identity = new ClaimsIdentity(authenticationType: "Demo");
        identity.AddClaim(new(ClaimTypes.Name, model.UserName));
        identity.AddClaim(new(ClaimTypes.Role, "管理员"));
        identity.AddClaim(new(ClaimTypes.DateOfBirth, "1990-1-1"));

        await HttpContext.SignInAsync(new(identity));

        return Redirect(returnUrl ?? "/");
    }

    [HttpGet("sign-out")]
    public async Task<IActionResult> SignOutAsync()
    {
        await HttpContext.SignOutAsync();
        return Redirect("/login");
    }
}
