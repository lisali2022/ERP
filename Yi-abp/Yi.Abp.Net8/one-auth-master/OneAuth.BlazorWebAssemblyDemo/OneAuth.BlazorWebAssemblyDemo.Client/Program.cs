using System.Security.Claims;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using OneAuth.BlazorWebAssemblyDemo.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new(builder.HostEnvironment.BaseAddress) });
builder.Services.AddCascadingAuthenticationState();  //级联： CascadingAuthenticationState
builder.Services.AddBlazoredLocalStorage();  //本地持久化
builder.Services.AddScoped<AuthenticationStateProvider, BlazorAuthenticationStateProvider>();  //注入策略
builder.Services.AddAuthorizationCore(options =>  //授权方式
{
    options.AddPolicy("Above21", policy =>
    {
        policy.RequireClaim(ClaimTypes.DateOfBirth)
        .RequireAssertion(context =>
        {
            var birthDay = context.User.FindFirst(ClaimTypes.DateOfBirth);
            return (DateOnly.TryParse(birthDay.Value, out var birth) && DateTime.Now.Date.Year - birth.Year > 21);
        });
    });
});

await builder.Build().RunAsync();
