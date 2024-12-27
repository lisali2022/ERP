using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

using OneAuth.BlazorWebAssemblyDemo.Client.Models;

namespace OneAuth.BlazorWebAssemblyDemo.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    public const string KEY = "oi2q3jrfou09q23hfoqiuhwciquhefc08qhf";

    [HttpPost]
    public IActionResult SignInAsync(LoginViewModel model)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
        var sign = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var header = new JwtHeader(sign);

        var payload = new JwtPayload();
        payload.AddClaim(new(ClaimTypes.Name, model.UserName));
        payload.AddClaim(new(ClaimTypes.Role, "管理员"));
        payload.AddClaim(new(ClaimTypes.DateOfBirth, "2010-1-1"));

        var handler = new JwtSecurityTokenHandler();

        var token = new JwtSecurityToken(issuer: "STS",
                                         audience: "test",
                                         payload.Claims,
                                         notBefore: DateTime.Now,
                                         expires: DateTime.Now.AddMinutes(10),
                                         signingCredentials: header.SigningCredentials);

        var jwt = handler.WriteToken(token);

        return Ok(jwt);
    }

    [HttpGet("profile")]
    [Authorize]
    public IActionResult GetUserProfile()
    {
        UserInfo userInfo = new UserInfo
        {
            UserName = User.Identity?.Name,
            Roles = User.FindAll(ClaimTypes.Role).Select(m => m.Value),
            Birthday = DateTime.Parse(User.FindFirstValue(ClaimTypes.DateOfBirth) ?? "")
        };
        return Ok(userInfo);
    }
}
