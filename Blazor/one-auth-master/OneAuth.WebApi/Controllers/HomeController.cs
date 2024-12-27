using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace OneAuth.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = "管理员")]
public class HomeController : ControllerBase
{
    [HttpGet]
    public IActionResult Index() => Ok($"ok:{User.Identity.Name}");

    [HttpGet("auth")]
    //[AllowAnonymous]
    public async Task<IActionResult> Auth()
    {

        var identity = new ClaimsIdentity(authenticationType: "cookie");

        identity.AddClaim(new(ClaimTypes.Name, "张三"));

        await HttpContext.SignInAsync(new(identity));

        return Ok(User.Identity.Name);
    }
    [HttpGet("sign-out")]
    [AllowAnonymous]
    public async Task<IActionResult> SignOut()
    {
        await HttpContext.SignOutAsync();
        return Ok("sign out");
    }

    [HttpGet("/access")]
    public IActionResult GetAccess()
    {
        if (User.IsInRole("销售部"))
        {
            return Ok("具备市场部内容");
        }

        return Ok("正常访问");
    }


    [Authorize(Roles = "市场部,销售部")]
    [HttpGet("/market-access")]
    public IActionResult GetMarketAccess()
    {
        return Ok("市场/销售部-正常访问");
    }

    [Authorize(Policy ="HasEmailPolicy")]
    [HttpGet("has-email")]

    public IActionResult HasEmailCanAccess()
    {
        return Ok("你有邮箱，可以进来");
    }

    [Authorize(Policy = "HasEmployeNo")]
    [HttpGet("has-employee-no")]

    public IActionResult AllowEmployeeNo()
    {
        var no = User.FindFirstValue("EmployeeNo");
        if (no=="5")
        {
            return StatusCode(403, "5其实也不能进入");
        }

        return Ok("你的员工编号允许访问");
    }

    [Authorize(Policy ="Above18")]
    [HttpGet("above-18")]
    public IActionResult Above18Access()
    {
        return Ok("你满18岁了");
    }
}
