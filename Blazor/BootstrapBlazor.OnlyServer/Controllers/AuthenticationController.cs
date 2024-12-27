using System;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Security.Claims;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Blazor.Shared.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Runtime;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Yi.Framework.Rbac.Domain.Shared.Dtos;
using Blazor.Shared.Core.NewtonsoftJson;
using BootstrapBlazor.Components;
using Yi.Framework.Rbac.Application.Contracts.Dtos.Account;

namespace BootstrapBlazor.OnlyServer.Controllers;

[Route("api/auth")]//api/auth/sign-in
[ApiController]
public class AuthenticationController : ControllerBase
{
    string msg { get; set; } = "";
    private readonly HttpClient _httpClient;
    // 构造函数注入HttpClient  
    public AuthenticationController(HttpClient httpClient)
    {
        _httpClient = httpClient;       
    }
    [HttpPost("sign-in")]
    public async Task<IActionResult> SignInAsync([FromForm] LoginInputVo model,[FromQuery]string? returnUrl)
    {
        try
        {
            var jsonObject = await new ApiClient().PostAsync<JObject, LoginInputVo>("account/login", model);
            if (jsonObject != null)
            {                
                string? token = jsonObject.ContainsKey("Token") ? jsonObject["Token"].ToString() : "";                
                if (token != "")
                {
                    AppsettingsUtility.InitialApi(token);  //初始化API请求调用的参数及实体 

                    var identity = new ClaimsIdentity(authenticationType: "JWT");
                    identity.AddClaim(new(ClaimTypes.Authentication, token));
                    identity.AddClaim(new(ClaimTypes.Name, model.UserName));
                   
                    if (AppsettingsUtility.UserRoleMenuDto != null)                    {
                        identity.AddClaim(new("UserId", AppsettingsUtility.UserRoleMenuDto.User.Id.ToString()));
                        identity.AddClaim(new("DisplayName", AppsettingsUtility.UserRoleMenuDto.User.Name));//
                        identity.AddClaim(new(ClaimTypes.StreetAddress, AppsettingsUtility.UserRoleMenuDto.User.Address));
                        identity.AddClaim(new("Nick", AppsettingsUtility.UserRoleMenuDto.User.Nick));
                        identity.AddClaim(new(ClaimTypes.MobilePhone, AppsettingsUtility.UserRoleMenuDto.User.Phone.ToString()));
                        identity.AddClaim(new(ClaimTypes.Role, JsonConvert.SerializeObject(AppsettingsUtility.UserRoleMenuDto.Roles)));                        
                    }
                    identity.AddClaim(new(ClaimTypes.Name, model.UserName));
                    identity.AddClaim(new(ClaimTypes.Role, "管理员"));
                    identity.AddClaim(new(ClaimTypes.DateOfBirth, "1990-1-1"));
                    await HttpContext.SignInAsync(new(identity));  //签入
                    return Redirect(returnUrl ?? "/");
                }
                else
                {
                    msg = "帐户或者密码不正确!";
                    return Ok(msg);
                }
            }
        }
        catch (HttpRequestException e)
        {
            // Console.WriteLine("\nException Caught!");
            // Console.WriteLine();
            msg = e.Message;
            return Ok(msg);
        }
        return Ok(msg);
    }

    [HttpGet("sign-out")]//api/auth/sign-out
    public async Task<IActionResult> SignOutAsync()
    {
        await HttpContext.SignOutAsync();
        return Redirect("/login");
    }
}
