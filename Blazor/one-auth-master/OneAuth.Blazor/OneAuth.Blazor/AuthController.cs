using System.Security.Claims;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace OneAuth.Blazor;

[ApiController]
[Route("[controller]")]
public class AuthController:ControllerBase
{
    [HttpPost("{userName}")]
    public async Task<IActionResult> Post([FromRoute]string userName)
    {
        var identity = new ClaimsIdentity(authenticationType: "cookie");
        identity.AddClaim(new(ClaimTypes.Name, userName));

        await HttpContext.SignInAsync(new(identity));
        return Redirect("/");
    }
}
