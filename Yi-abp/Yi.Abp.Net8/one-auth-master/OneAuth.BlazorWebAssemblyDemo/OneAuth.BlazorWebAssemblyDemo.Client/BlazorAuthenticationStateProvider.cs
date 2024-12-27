using System.Net.Http.Json;
using System.Security.Claims;

using Blazored.LocalStorage;

using Microsoft.AspNetCore.Components.Authorization;

using OneAuth.BlazorWebAssemblyDemo.Client.Models;

namespace OneAuth.BlazorWebAssemblyDemo.Client;

public class BlazorAuthenticationStateProvider(IServiceProvider serviceProvider) 
    : AuthenticationStateProvider
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        //2. token -> api -> user -> local storage
        if (!OperatingSystem.IsBrowser())
        {
            return new AuthenticationState(new());
        }

        var client = _serviceProvider.GetRequiredService<HttpClient>();
        var localStorage = _serviceProvider.GetRequiredService<ILocalStorageService>();

        var userInfo = await localStorage.GetItemAsync<UserInfo?>("USER_INFO");

        if (userInfo is null)
        {
            var token = await localStorage.GetItemAsStringAsync("TOKEN");
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new("Bearer", token);
                var user = await client.GetFromJsonAsync<UserInfo>("api/auth/profile");

                await localStorage.SetItemAsync("USER_INFO", user);

                userInfo = user;
            }
        }

        var identity = CreateClaims(userInfo);

        return new AuthenticationState(new(identity));
    }

    private static ClaimsIdentity CreateClaims(UserInfo? userInfo)
    {
        if (userInfo is null)
        {
            return new();
        }

        var identity = new ClaimsIdentity(authenticationType: "JWT");

        identity.AddClaim(new(ClaimTypes.Name, userInfo.UserName));

        foreach (var role in userInfo.Roles)
        {
            identity.AddClaim(new(ClaimTypes.Role, role));
        }

        if (userInfo.Birthday.HasValue)
        {
            identity.AddClaim(new(ClaimTypes.DateOfBirth, userInfo.Birthday.Value.ToString()));
        }
        return identity;
    }
}
