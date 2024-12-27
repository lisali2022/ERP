using System.Net.Http;
using System.Security.Claims;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components;

namespace OneAuth.Blazor.Components.Pages;

partial class Login
{
    string? UserName { get; set; }

    [Inject] IHttpContextAccessor HttpContextAccessor { get; set; }

    [Inject] NavigationManager NavigationManager { get; set; }

    async Task SubmitAsync()
    {
        var identity = new ClaimsIdentity(authenticationType: "cookie");
        identity.AddClaim(new(ClaimTypes.Name, UserName));

        await HttpContextAccessor.HttpContext.SignInAsync(new(identity));

        NavigationManager.NavigateTo("/", true);
    }
}
