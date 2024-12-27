using Blazor.Shared.Models;
using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using System.Security.Claims;
using Volo.Abp.Application.Dtos;
using Yi.Framework.Rbac.Application.Contracts.Dtos.Menu;
using Yi.Framework.Rbac.Domain.Shared.Enums;
using Yi.Framework.Rbac.Domain.Shared.Dtos;

namespace BootstrapBlazor.OnlyServer1.Components.Shared
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class MainLayout
    {

        public UserDto? CurrUser { get; set; } = new();
        private string LogOutUrl { get; set; } = "api/auth/sign-out";
        private bool UseTabSet { get; set; } = true;
        private string Theme { get; set; } = "";
        private bool IsOpen { get; set; }
        private bool IsFixedHeader { get; set; } = true;
        private bool IsFixedFooter { get; set; } = true;
        private bool IsFullSide { get; set; } = true;
        private bool ShowFooter { get; set; } = true;
        private List<MenuItem>? Menus { get; set; }
        PagedResultDto<MenuGetListOutputDto>? PagedResultDto { get; set; }
        private List<MenuItem>? menuItems { get; set; }
        private IEnumerable<MenuItem>? MenuItems { get; set; }

        //路由中的AuthorizeRouteView组件参数
        [CascadingParameter]
        Task<AuthenticationState> StateTask { get; set; }
        protected override async Task OnInitializedAsync()
        {
            //menuItems = new List<MenuItem>();
            //MenuItems=new List<MenuItem>();
            //await AddTestMenuItemsAsync();
            //MenuItems = menuItems;

    
            await base.OnInitializedAsync();
            var state = await StateTask;
            if (!state.User.Identity.IsAuthenticated)
            {
                Navigation.NavigateTo("/login", true);
            }
            else
            {
                List<string> userClaims = new List<string>();
                var user = state.User;
                string token = user.FindFirstValue(ClaimTypes.Authentication);
                await AppsettingsUtility.InitialApi(token);

                CurrUser = AppsettingsUtility.UserRoleMenuDto.User;
                PagedResultDto = await AppsettingsUtility.ApiClient.GetAsync<PagedResultDto<MenuGetListOutputDto>>("menu?visible=true");//获取数据库的菜单
                IReadOnlyList<MenuGetListOutputDto> lst = PagedResultDto.Items
                    .Where(x => x.MenuType != MenuTypeEnum.Component && x.ParentId == Guid.Empty)
                    .OrderBy(x => x.OrderNum)
                    .ToList();

                GetCascadeMenuItems(lst, null); //获取添加测试菜单到menuItems
                await AddTestMenuItemsAsync(); //添加测试菜单到menuItems

                List<MenuItem>? MenuTree = new List<MenuItem>();
                var parentNode = menuItems.Where(p => p.ParentId == Guid.Empty.ToString()).ToList();
                foreach (var menu in parentNode)
                {
                    menu.Items = new List<MenuItem> { };
                    GetNewNodes(menuItems, menu);
                    MenuTree.Add(menu);
                }
                MenuItems = MenuTree;
            }
        }

        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            return base.OnAfterRenderAsync(firstRender);
        }
        private async Task AddTestMenuItemsAsync()
        {
            //不声明Parent=menuDemo 只声明 ParentId="Demo"也是可以产生层级关系的
            MenuItem menuDemo = new MenuItem() { Id = "Demo", Text = "Demo", Icon = "fa-solid fa-fw fa-flag", Match = NavLinkMatch.All };
            menuItems.Add(new MenuItem() { Id = "TablesTree", ParentId = "Demo", Text = "树型表格", Icon = "fa-solid fa-fw fa-users", Url = "/table/tree" });
            menuItems.Add(new MenuItem() { Id = "Demo", Text = "Demo", Icon = "fa-solid fa-fw fa-flag", Match = NavLinkMatch.All });
            menuItems.Add(new MenuItem() { Id = "TkFonticon", ParentId = "Demo", Parent = menuDemo, Text = "fonticon", Icon = "fa-solid fa-fw fa-check-square", Url = "/TkFonticon" });
            menuItems.Add(new MenuItem() { Id = "tkgrid", ParentId = "Demo", Text = "tkgrid", Icon = "fa-solid fa-fw fa-table", Url = "/tkgrid" });
            menuItems.Add(new MenuItem() { Id = "counter", ParentId = "Demo", Text = "计数器", Icon = "fa-solid fa-fw fa-check-square", Url = "/counter" });
            menuItems.Add(new MenuItem() { Id = "weather", ParentId = "Demo", Text = "天气", Icon = "fa-solid fa-fw fa-database", Url = "/weather" });
            menuItems.Add(new MenuItem() { Id = "table", ParentId = "Demo", Text = "BT网格", Icon = "fa-solid fa-fw fa-table", Url = "/table" });
            menuItems.Add(new MenuItem() { Id = "roster", ParentId = "Demo", Text = "花名册", Icon = "fa-solid fa-fw fa-users", Url = "/roster" });
        }
        static void GetNewNodes(List<MenuItem> all, MenuItem curItem)
        {
            curItem.Items = all.Where(c => c.ParentId == curItem.Id).ToList();
            foreach (var subItem in curItem.Items)
            {
                GetNewNodes(all, subItem);
            }
        }
        /// <summary>
        /// /获取扁平,有层次关系的菜单
        /// </summary>
        /// <param name="items"></param>
        /// <param name="parentMenu"></param>
        public void GetCascadeMenuItems(IReadOnlyList<MenuGetListOutputDto> items, MenuItem parentMenu)
        {
            foreach (var menuGetListOutputDto in items)
            {
                MenuItem menuItem = new MenuItem()
                {
                    Id = menuGetListOutputDto.Id.ToString(),
                    ParentId = menuGetListOutputDto.ParentId.ToString(),
                    Parent = parentMenu,
                    Text = menuGetListOutputDto.MenuName,
                    Url = menuGetListOutputDto.Url == null ? menuGetListOutputDto.Url : menuGetListOutputDto.Url.Replace("~/", ""),
                    Icon = menuGetListOutputDto.Icon
                };
                //menuItem.Indent = parentMenu == null ? 0 : parentMenu.Indent + 1;
                IReadOnlyList<MenuGetListOutputDto> childrenItems = PagedResultDto.Items
                    .Where(x => x.MenuType != MenuTypeEnum.Component && x.ParentId == menuGetListOutputDto.Id)
                    .OrderBy(x => x.OrderNum)
                    .ToList();
                if (childrenItems != null && childrenItems.Count() > 0)
                {
                    menuItem.IsCollapsed = true;
                    GetCascadeMenuItems(childrenItems, menuItem);
                }
                else
                {
                    menuItem.IsCollapsed = false;
                }

                if (menuItems == null)
                {
                    menuItems = new List<MenuItem>();
                    menuItems.Insert(0, menuItem);
                }
                else
                {
                    menuItems.Add(menuItem);
                }
            }
        }

    }
}


