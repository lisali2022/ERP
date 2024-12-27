using System;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Security.Claims;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Blazor.Shared.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Yi.Framework.Rbac.Domain.Shared.Dtos;
namespace BlazorAuto.Controllers;

[Route("api/auth")]
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
    public async Task<IActionResult> SignInAsync([FromForm]UserDto model,[FromQuery]string? returnUrl)
    {
        try
        {
            string url = "http://localhost:19001/api/app/account/login";
            var response = await _httpClient.PostAsJsonAsync(url, model);           
            //HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var tokenStr = await response.Content.ReadAsStringAsync();
            if (!string.IsNullOrWhiteSpace(tokenStr))
            {
                JObject jsonObject = JObject.Parse(tokenStr);
                if (jsonObject.ContainsKey("token"))
                {
                    string token = jsonObject["token"].ToString();      
                    msg = token;
                    var identity = new ClaimsIdentity(authenticationType: "Demo");
                    identity.AddClaim(new(ClaimTypes.Name, model.UserName));
                    identity.AddClaim(new(ClaimTypes.Role, "管理员"));
                    identity.AddClaim(new(ClaimTypes.DateOfBirth, "1990-1-1"));
                    await HttpContext.SignInAsync(new(identity));
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

    [HttpGet("sign-out")]
    public async Task<IActionResult> SignOutAsync()
    {
        await HttpContext.SignOutAsync();
        return Redirect("/login");
    }
}
