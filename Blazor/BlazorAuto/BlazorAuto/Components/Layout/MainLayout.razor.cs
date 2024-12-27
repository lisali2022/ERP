using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;

namespace BlazorAuto.Components.Layout
{
    public partial class MainLayout
    {
        //路由中的AuthorizeRouteView组件参数
        [CascadingParameter]
        Task<AuthenticationState> StateTask { get; set; }
        protected override async Task OnInitializedAsync()
        {
            var state = await StateTask;
            if (!state.User.Identity.IsAuthenticated)
            {
                Manager.NavigateTo("/login", true);
            }
        }
    }
}
