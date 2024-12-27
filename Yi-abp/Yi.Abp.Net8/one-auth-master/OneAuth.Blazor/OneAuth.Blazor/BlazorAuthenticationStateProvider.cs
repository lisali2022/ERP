using Microsoft.AspNetCore.Components.Authorization;

namespace OneAuth.Blazor;

public class BlazorAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly IHttpContextAccessor _accessor;

    public BlazorAuthenticationStateProvider(IHttpContextAccessor accessor)
    {
        this._accessor = accessor;
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        return Task.FromResult(new AuthenticationState(_accessor?.HttpContext?.User??new()));
    }
}
