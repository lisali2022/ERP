using Microsoft.AspNetCore.Components.Authorization;

namespace OneAuth.Blazor.Client;

public class WasmAuthenticationStateProvider : AuthenticationStateProvider
{
    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        throw new NotImplementedException();
    }
}
